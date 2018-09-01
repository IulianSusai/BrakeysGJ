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
			ActionsManager.Instance.onGameStart += OnGameStart;
			ActionsManager.Instance.onGameEnd += OnGameEnded;
#if UNITY_STANDALONE
			Cursor.visible = false;
#endif
		} else {
			Destroy(gameObject);
		}
	}
#endregion

	public DesignSettings design;
	public PlayerInputSettings inputSettings;

	[SerializeField] private List<string> startDialog;
	[SerializeField] private List<string> endDialog;
	[SerializeField] private Sprite talkerSprite;
	private int currentIndex;
	private GameState state;

	private void Start() {
		state = GameState.Ui;
	}

	private void OnGameStart() {
		PlayerController.Instance.state = PlayerState.Idle;
		state = GameState.Start;
		currentIndex = 0;
		ActionsManager.Instance.SendTalkStatusChanged(ToughtState.Start);
		ActionsManager.Instance.SendOnTalkText(talkerSprite, startDialog[currentIndex]);
		currentIndex++;
	}

	private void OnGameEnded() {
		PlayerController.Instance.state = PlayerState.Idle;
		state = GameState.End;
		currentIndex = 0;
		ActionsManager.Instance.SendTalkStatusChanged(ToughtState.Start);
		ActionsManager.Instance.SendOnTalkText(talkerSprite, endDialog[currentIndex]);
		currentIndex++;
	}


	private void Update() {
		KeyCode actionKey = inputSettings.actionKey;
		if (Input.GetKeyDown(actionKey)) {
			if(state == GameState.Start) {
				if(currentIndex == startDialog.Count) {
					ActionsManager.Instance.SendTalkStatusChanged(ToughtState.End);
					state = GameState.Play;
					PlayerController.Instance.state = PlayerState.Moving;
				} else {
					ActionsManager.Instance.SendOnTalkText(talkerSprite, startDialog[currentIndex]);
				}
				currentIndex++;
			} else if( state == GameState.End) {
				if (currentIndex == endDialog.Count) {
					ActionsManager.Instance.SendTalkStatusChanged(ToughtState.End);
					state = GameState.Ui;
				} else {
					ActionsManager.Instance.SendOnTalkText(talkerSprite, endDialog[currentIndex]);
				}
				currentIndex++;
			}
		}
	}
}

[Serializable]
public struct DesignSettings
{
	public float fireflyMoveSpeed;
	public float fireflyLightIntensity;
	public float chargeTime;
	public AnimationCurve fireflyChargeCurve;
	public float unchargeTime;
	public AnimationCurve fireflyUnchargeCurve;
	public float humanMaxSpeed;
	public float humanMoveForce;
	public float humanJumpForce;
	public float lightIntensity;
	public float torchLightTime;
	public AnimationCurve torchLightCurve;
}

[Serializable]
public class PlayerInputSettings
{
	public KeyCode swapKey;
	public KeyCode actionKey;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode moveUp;
	public KeyCode moveDown;

	public bool IsMoveKey(KeyCode key) {
		return key == moveLeft || key == moveRight || key == moveDown || key == moveUp;
	}

}

public enum GameState
{
	Start,
	Play,
	End,
	Ui
}

