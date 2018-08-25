using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyCharacter : CharacterBase {

	private bool isCharging;
	private float currentCharge;
	private float lerpStartTime;

	private bool isUpdatingCharge;
	private float startCharge;
	private float endCharge;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

#region Movement

	public override void CheckMoveInput() {
		KeyCode up = GameManager.Instance.inputSettings.moveUp;
		KeyCode down = GameManager.Instance.inputSettings.moveDown;
		KeyCode left = GameManager.Instance.inputSettings.moveLeft;
		KeyCode right = GameManager.Instance.inputSettings.moveRight;
		float moveSpeed = GameManager.Instance.design.fireflyMoveSpeed;

		//up
		if (Input.GetKey(up)) {
			rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
		} else if (Input.GetKeyUp(up)) {
			rb.velocity = new Vector2(rb.velocity.x, 0f);
		}

		//down
		if (Input.GetKey(down)) {
			rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
		} else if (Input.GetKeyUp(down)) {
			rb.velocity = new Vector2(rb.velocity.x, 0f);
		}

		//right
		if (Input.GetKey(right)) {
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		} else if (Input.GetKeyUp(right)) {
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}

		//left
		if (Input.GetKey(left)) {
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
		} else if (Input.GetKeyUp(left)) {
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}
	}
	#endregion

	private void Update() {
		if (isUpdatingCharge) {
			UpdateCharge();
		}
	}

	private void StartCharge() {
		isCharging = true;
		isUpdatingCharge = true;
		lerpStartTime = Time.time;
		startCharge = currentCharge;
		endCharge = 1f;
	}

	private void StopCharge() {
		isCharging = false;
		isUpdatingCharge = true;
		lerpStartTime = Time.time;
		startCharge = currentCharge;
		endCharge = 0f;
	}

	private void UpdateCharge() {
		float chargeTime = Time.time - lerpStartTime;
		float percentage = chargeTime / (isCharging ? GameManager.Instance.design.chargeTime : GameManager.Instance.design.unchargeTime);
		AnimationCurve curve = isCharging ? GameManager.Instance.design.fireflyChargeCurve : GameManager.Instance.design.fireflyUnchargeCurve;
		currentCharge = Mathf.Lerp(startCharge, endCharge, curve.Evaluate(percentage));
		if(percentage >= 1.0f) {
			isUpdatingCharge = false;
			if (!isCharging) {
				Debug.LogError("PLAYER DEATH");
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Torch")) {
			Torch t = collision.gameObject.GetComponent<Torch>();
			if (!t.isActive) {
				t.ActivateTorch();
			}
			StartCharge();
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Torch")) {
			StopCharge();
		}
	}

}
