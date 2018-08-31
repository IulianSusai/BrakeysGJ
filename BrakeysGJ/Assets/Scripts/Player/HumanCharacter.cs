﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCharacter : CharacterBase {

	[SerializeField] private JumpTrigger jumpTrigger;
	private Vector2 spawnPosition;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

#region Movement

	public override void CheckMoveInput() {
		KeyCode up = GameManager.Instance.inputSettings.moveUp;
		KeyCode left = GameManager.Instance.inputSettings.moveLeft;
		KeyCode right = GameManager.Instance.inputSettings.moveRight;
		float moveSpeed = GameManager.Instance.design.humanMoveForce;

		if (Input.GetKey(right)) {
			rb.AddForce(Vector2.right * moveSpeed);
		}

		if (Input.GetKey(left)) {
			rb.AddForce(Vector2.left * moveSpeed);
		}

		if (Input.GetKeyDown(up) && jumpTrigger.canJump) {
			rb.AddForce(Vector2.up * GameManager.Instance.design.humanJumpForce, ForceMode2D.Impulse);
		}

	}

	private void FixedUpdate() {
		float moveMaxSpeed = GameManager.Instance.design.humanMaxSpeed;
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -moveMaxSpeed, moveMaxSpeed), rb.velocity.y);
	}

	public override void CheckActionInput() {
		KeyCode actionKey = GameManager.Instance.inputSettings.actionKey;		
	}

	#endregion

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Finish")) {
			Debug.LogError("Level Finished");
			ActionsManager.Instance.SendOnLevelFinished();
		} else if (collision.CompareTag("Checkpoint")) {
			spawnPosition = collision.transform.position;
		} else if (collision.CompareTag("Enemy")) {
			rb.velocity = Vector2.zero;
			transform.position = spawnPosition;
		}
	}
}
