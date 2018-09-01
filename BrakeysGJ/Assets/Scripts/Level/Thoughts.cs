using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thoughts : MonoBehaviour {

	[SerializeField] protected List<string> phrases;
	private int currentIndex;
	private bool isPlayerNear;

	private void Start() {
		currentIndex = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			isPlayerNear = true;
			if(currentIndex < phrases.Count) {
				SendTalk();
			} else {
				currentIndex = phrases.Count - 1;
				SendTalk();
			}
			ActionsManager.Instance.SendTalkStatusChanged(ToughtState.Start);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			ActionsManager.Instance.SendTalkStatusChanged(ToughtState.End);
			isPlayerNear = false;
		}
	}

	private void Update() {
		if (isPlayerNear) {
			KeyCode actionKey = GameManager.Instance.inputSettings.actionKey;
			if (Input.GetKeyDown(actionKey)) {
				if (currentIndex == phrases.Count) {
					ActionsManager.Instance.SendTalkStatusChanged(ToughtState.End);
					return;
				}
				SendTalk();
				currentIndex++;
			}
		}
	}

	private void SendTalk() {
		Sprite chSprite = GetComponent<SpriteRenderer>().sprite;
		ActionsManager.Instance.SendOnTalkText(chSprite, phrases[currentIndex]);
	}

}
public enum ToughtState
{
	Start,
	End
}