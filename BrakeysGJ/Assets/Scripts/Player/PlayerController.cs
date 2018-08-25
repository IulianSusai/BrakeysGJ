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

	[SerializeField] private List<CharacterBase> characters;
	private int currentIndex = 0;
	private CharacterBase currentCharacter;

	private void Update() {
		if (Input.GetKeyDown(GameManager.Instance.inputSettings.swapKey)) {
			SwapCharacter();
		}
	}

	private void SwapCharacter() {
		currentIndex = (int)Mathf.Repeat(currentIndex++, characters.Count - 1);
		currentCharacter = characters[currentIndex];
	}

}
