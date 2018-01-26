using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMassages : MonoBehaviour {

	public float time = 0.3f;

	// Update is called once per frame
	void Update () {
		Destroy(this.gameObject, time);
	}
}
