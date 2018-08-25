using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCharacter : CharacterBase {

	[SerializeField] private JumpTrigger jumpTrigger;
	Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

#region Movement

	public override void CheckMoveInput() {
		KeyCode left = GameManager.Instance.inputSettings.moveLeft;
		KeyCode right = GameManager.Instance.inputSettings.moveRight;
		float moveSpeed = GameManager.Instance.design.humanMoveForce;

		if (Input.GetKey(right)) {
			rb.AddForce(Vector2.right * moveSpeed);
		}

		if (Input.GetKey(left)) {
			rb.AddForce(Vector2.left * moveSpeed);
		}
				
	}

	private void FixedUpdate() {
		float moveMaxSpeed = GameManager.Instance.design.humanMaxSpeed;
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -moveMaxSpeed, moveMaxSpeed), rb.velocity.y);
		Debug.Log(rb.velocity);
	}

	public override void CheckActionInput() {
		KeyCode actionKey = GameManager.Instance.inputSettings.actionKey;
		if(Input.GetKeyDown(actionKey) && jumpTrigger.canJump) {
			rb.AddForce(Vector2.up * GameManager.Instance.design.humanJumpForce, ForceMode2D.Impulse);
		}
	}

	#endregion

}
