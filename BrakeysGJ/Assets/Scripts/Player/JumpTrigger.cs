﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {

	public bool canJump { private set; get; }

	private void OnTriggerStay2D(Collider2D collision) {
		if(collision.CompareTag("Platform") && !canJump) {
			canJump = true;
		} else if (collision.CompareTag("MovingPlatform")) {
			canJump = true;
			gameObject.transform.parent.transform.SetParent(collision.gameObject.transform);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Platform")) {
			canJump = false;
		} else if (collision.CompareTag("MovingPlatform")) {
			canJump = false;
			gameObject.transform.parent.transform.SetParent(PlayerController.Instance.gameObject.transform);
		}
	}

}
