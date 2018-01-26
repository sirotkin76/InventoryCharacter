using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoKeyManager : MonoBehaviour {

	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit();
	}

}
