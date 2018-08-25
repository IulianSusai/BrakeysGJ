using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	public bool isActive { private set; get; }

	private void Start() {
		isActive = false;
	}

	public void ActivateTorch() {
		isActive = true;
	}

}
