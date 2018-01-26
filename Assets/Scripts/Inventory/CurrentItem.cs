using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentItem : MonoBehaviour, IPointerClickHandler {

	[HideInInspector]
	public int index;

	GameObject invetoryObj;
	Inventory inventory;

    // Use this for initialization
    void Start () {
		invetoryObj = GameObject.FindGameObjectWithTag("InventoryManager");
		inventory = invetoryObj.GetComponent<Inventory>();
	}
	
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right) {
			if (inventory.item[index].id == 0) return;

			GameObject dropedObj = Instantiate(Resources.Load<GameObject>(inventory.item[index].pathPrefab));
			dropedObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward;

			if (inventory.item[index].countItem > 1)
				inventory.item[index].countItem --; 
			else 
				inventory.item[index] = new Item();
				
			inventory.DisplayItems();
		}
    }

}
