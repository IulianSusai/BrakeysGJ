using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

#region Instance
	public static PlayerController Instance { private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			ActionsManager.Instance.onLevelStart += StartLevel;
			ActionsManager.Instance.onLevelFinished += OnLevelFinished;
		} else {
			Destroy(gameObject);
		}
	}
#endregion

	public PlayerState state;
	[SerializeField] private CameraFollow cameraFollow;
	[SerializeField] private List<CharacterBase> characters;
	private int currentIndex = 1;
	private CharacterBase currentCharacter;
	
	//Iulian ToDo -> Use Actions when adding an intermediate layer between LevelFinish and LevelStart
	public void StartLevel(Vector2 fireflyPos, Vector2 humanPos ) {
		characters[0].transform.position = fireflyPos;
		characters[1].transform.position = humanPos;
		currentCharacter = characters[1];
		cameraFollow.SnapToPos(currentCharacter.transform);
		state = PlayerState.Moving;
		//ActivateCharacters(true);
	}

	private void OnLevelFinished() {
		//ActivateCharacters(false);
		//state = PlayerState.Idle;
	}

	private void ActivateCharacters(bool _active) {
		for(int i = 0; i < characters.Count; i++) { 
			characters[i].gameObject.SetActive(_active);
		}
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
