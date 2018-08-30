using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePage : MenuPage {

	[SerializeField] private Text speakText;
	[SerializeField] private Image speakerImage;
	[SerializeField] private GameObject speakContent;

	public override void Show() {
		base.Show();
		speakContent.SetActive(false);
		ActionsManager.Instance.onTalkText += OnCharacterTalk;
		ActionsManager.Instance.onTalkStatusChanged += OnStatusChanged;
	}

	public override void Hide() {
		base.Hide();
		ActionsManager.Instance.onTalkText -= OnCharacterTalk;
		ActionsManager.Instance.onTalkStatusChanged -= OnStatusChanged;
	}

	private void OnCharacterTalk(Sprite ch, string text) {
		speakText.text = text;
		speakerImage.sprite = ch;
	}

	private void OnStatusChanged(ToughtState state) {
		speakContent.SetActive(state == ToughtState.Start);
	}
}
