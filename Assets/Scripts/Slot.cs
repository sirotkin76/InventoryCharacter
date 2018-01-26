using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Sprite activeCell;
	Sprite cell;

	Image img;

	void OnDisable() {
		img.sprite = cell;
	}
	
	// Update is called once per frame
	void Start () {
		img = GetComponent<Image>();
		cell = img.sprite;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = activeCell;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = cell;
    }

}
