using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour {

	[HideInInspector]
	public List <Item> item;

	public GameObject cellContainer;
	public KeyCode showInventory;
	public KeyCode takeButton;

	public FirstPersonController player;
	public GameObject point;

	[Header ("Massage")]
	public GameObject massageManager;
	public GameObject massage;


	// Use this for initialization
	void Start () {
		item = new List<Item>();

		cellContainer.SetActive (false);

		for (int i = 0; i < cellContainer.transform.childCount; i++) {
			item.Add(new Item());

			cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ToggleInventory();

		if ( Input.GetKeyDown(takeButton)) {
			Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
			RaycastHit hit;

			Debug.Log("1 - ");

			if (Physics.Raycast(ray, out hit, 6f)) {

				if (hit.collider.GetComponent<Item>()) {

					Item currentItem = hit.collider.GetComponent<Item>();
					Massage(currentItem);
					AddItem(hit.collider.GetComponent<Item>());

				}
			}
		}
	}

	void Massage(Item currentItem) {
		GameObject msgObj = Instantiate(massage);
		msgObj.transform.SetParent(massageManager.transform);
		Image msgImg = msgObj.transform.GetChild(0).GetComponent<Image>();
		msgImg.sprite = Resources.Load<Sprite>(currentItem.pathIcon);

		Text msgTxt = msgObj.transform.GetChild(1).GetComponent<Text>();
		msgTxt.text = currentItem.nameItem;
	}

	void AddItem(Item currentItem) {
		if (currentItem.isStackable) {
			AddStackableItem(currentItem);
		} else {
			AddUnstackableItem(currentItem);
		}
	}

	void AddUnstackableItem(Item currentItem) {
		for (int i = 0; i < item.Count; i++) {
			if (item[i].id == 0) {
				item[i] = currentItem;
				item[i].countItem = 1;
				DisplayItems();
				Destroy(currentItem.gameObject);
				break;
			}
		}
	}

	void AddStackableItem(Item currentItem) {
		for (int i=0; i < item.Count; i++) {
			if (item[i].id == currentItem.id) {
				item[i].countItem ++;
				DisplayItems();
				Destroy(currentItem.gameObject);
				return;
			}
		}

		AddUnstackableItem(currentItem);
	}

	void ToggleInventory() {	
		if (Input.GetKeyDown(showInventory)) {
			if (cellContainer.activeSelf) {
				cellContainer.SetActive (false);
				player.enabled = true;
				point.SetActive(true);
				// Time.timeScale = 1;
			} else {
				cellContainer.SetActive (true);
				player.enabled = false;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				point.SetActive(false);
				// Time.timeScale = 0;
			}
		}
	}

	public void DisplayItems() {
		for (int i = 0; i < item.Count; i++) {
			Transform cell = cellContainer.transform.GetChild(i);
			Transform icon = cell.GetChild(0);
			Transform count = icon.GetChild(0);
			Text txt = count.GetComponent<Text>();

			Image img = icon.GetComponent<Image>();

			if (item[i].id != 0) {

				img.enabled = true;
				img.sprite = Resources.Load<Sprite>(item[i].pathIcon);

				if (item[i].countItem > 1)
					txt.text = item[i].countItem.ToString(); 
				else 
					txt.text = null;

			} else {
				img.enabled = false;
				img.sprite = null;
				txt.text = null;
			}

		}


	}

}
