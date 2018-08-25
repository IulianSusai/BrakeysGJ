using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

#region Instance
	public static PlayerController Instance { private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}
	#endregion

	public PlayerState state;
	[SerializeField] private CameraFollow cameraFollow;
	[SerializeField] private List<CharacterBase> characters;
	private int currentIndex = 0;
	private CharacterBase currentCharacter;

	private void Start() {
		StartGame();
	}

	private void StartGame() {
		currentCharacter = characters[currentIndex];
		cameraFollow.SetTarget(currentCharacter.transform);
	}

	private void Update() {
		if (state == PlayerState.Moving) {
			CheckInput();
		}
	}

	private void CheckInput() {
		if (Input.GetKeyDown(GameManager.Instance.inputSettings.swapKey)) {
			SwapCharacter();
		}
		currentCharacter.CheckMoveInput();
		currentCharacter.CheckActionInput();
	}

	private void SwapCharacter() {
		currentCharacter.StopMoving();
		currentIndex = (int)Mathf.Repeat(++currentIndex, characters.Count);
		currentCharacter = characters[currentIndex];
		cameraFollow.SetTarget(currentCharacter.transform);
	}

}

public enum PlayerState
{
	Idle,
	Moving
}
