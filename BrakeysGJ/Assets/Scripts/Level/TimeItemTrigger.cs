using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItemTrigger : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			ActionsManager.Instance.SendOnTimeWaitTriger(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			ActionsManager.Instance.SendOnTimeWaitTriger(false);
		}
	}
}
