using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : MonoBehaviour {
	
	[SerializeField] float secondsToWait;

	private bool countTime;
	private float currentTime;
	private Rigidbody2D rb;
	private BoxCollider2D col;

	private void Start() {
		ActionsManager.Instance.onTimeWaitTrigger += OnTimeWaitTrigger;
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
		currentTime = 0;
	}

	private void OnTimeWaitTrigger(bool count) {
		countTime = count;
	}

	private void Update() {
		if (countTime) {
			currentTime += Time.deltaTime;	
			if(currentTime >= secondsToWait) {
				Debug.Log("Drop Item");
				rb.isKinematic = false;
				col.isTrigger = false;
				countTime = false;
				ActionsManager.Instance.onTimeWaitTrigger -= OnTimeWaitTrigger;
			}
		}
	}

}
