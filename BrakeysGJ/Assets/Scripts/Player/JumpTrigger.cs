using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {

	public bool canJump { private set; get; }

	private void OnTriggerStay2D(Collider2D collision) {
		if(collision.CompareTag("Platform") && !canJump) {
			canJump = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Platform")) {
			canJump = false;
		}
	}

}
