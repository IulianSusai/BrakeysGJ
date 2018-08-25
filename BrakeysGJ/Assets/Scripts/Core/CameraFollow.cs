using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


	[SerializeField] private float characterFollowSpeed;
	[SerializeField] private float boundsSize;
	[SerializeField] private float swapTime;
	[SerializeField] private AnimationCurve swapCurve;
	

	private Vector3 startPostion;
	private Vector3 endPosition;
	private float swapStartTime;
	private bool isMoving;

	Transform target;
	Vector3 refVel;

	public void SetTarget(Transform tar) {
		target = tar;
		PlayerController.Instance.state = PlayerState.Idle;
		StartSwap();
	}

	public void SnapToPos(Transform tar) {
		target = tar;
		transform.position = new Vector3(tar.position.x, tar.position.y, transform.position.z);
	}

	private void Update() {
		if (isMoving) {
			MoveToTarget();
		}
	}

	private void LateUpdate() {
		if (!isMoving) {
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), ref refVel, characterFollowSpeed * Time.deltaTime);
		}
	}

	private void StartSwap() {
		startPostion = transform.position;
		endPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
		swapStartTime = Time.time;
		isMoving = true;
	}


	private void MoveToTarget() {
		float timeSinceMoving = Time.time - swapStartTime;
		float percentage = timeSinceMoving / swapTime;
		transform.position = Vector3.Lerp(startPostion, endPosition, swapCurve.Evaluate(percentage));
		if(percentage >= 1.0f) {
			isMoving = false;
			PlayerController.Instance.state = PlayerState.Moving;
		}
	}


}
