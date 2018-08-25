using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

#region Instance
	public static GameManager Instance {private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}
#endregion

	public DesignSettings design;
	public PlayerInputSettings inputSettings;
}

[Serializable]
public struct DesignSettings
{
	public float fireflyMoveSpeed;
	public float chargeTime;
	public AnimationCurve fireflyChargeCurve;
	public float unchargeTime;
	public AnimationCurve fireflyUnchargeCurve;

}

[Serializable]
public class PlayerInputSettings
{
	public KeyCode swapKey;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode moveUp;
	public KeyCode moveDown;

	public bool IsMoveKey(KeyCode key) {
		return key == moveLeft || key == moveRight || key == moveDown || key == moveUp;
	}

}

