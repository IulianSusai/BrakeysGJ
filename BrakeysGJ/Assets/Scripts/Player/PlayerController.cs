using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

#region Instance
	public static PlayerController Instance { private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			ActionsManager.Instance.onGameStart += OnGameStart;
			ActionsManager.Instance.onGameEnd += OnGameEnd;
			ActionsManager.Instance.onTalkStatusChanged += OnTalkStatusChanged;
		} else {
			Destroy(gameObject);
		}
	}
#endregion

	public PlayerState state;
	[SerializeField] private CameraFollow cameraFollow;
	[SerializeField] private List<CharacterBase> characters;
	[SerializeField] private Vector2 fireflyEndPos;

	private int currentIndex = 1;
	private CharacterBase currentCharacter;
	public bool hasKey;


	private void OnTalkStatusChanged(ToughtState tstate) {
		state = tstate == ToughtState.End ? PlayerState.Moving : PlayerState.Idle;
	}

	public void OnGameStart() {
		hasKey = false;
		characters[0].gameObject.transform.position = new Vector2(-0.5f, 9.5f);
		characters[1].gameObject.transform.position = new Vector2(1f, 7.5f);
		currentCharacter = characters[1];
		cameraFollow.SnapToPos(currentCharacter.transform);
		state = PlayerState.Idle;
	}

	public void OnGameEnd() {
		characters[0].gameObject.transform.position = fireflyEndPos;
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
		if (Input.GetKeyDown(KeyCode.Escape)) {
			ActionsManager.Instance.ClearReferences();
			SceneManager.LoadScene(0);
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
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
