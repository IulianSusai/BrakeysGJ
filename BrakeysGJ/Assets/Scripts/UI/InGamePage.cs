using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGamePage : MenuPage {

	[SerializeField] private Text speakText;
	[SerializeField] private Image speakerImage;
	[SerializeField] private GameObject speakContent;
	[SerializeField] private Image dieTransImg;
	[SerializeField] private Text motivationText;

	[SerializeField] private float transitionTime;
	[SerializeField] private AnimationCurve transitionCurve;
	[SerializeField] private AnimationCurve textCurve;
	[SerializeField] private List<string> dialogs;

	private float transitionStartTime;
	private bool inTransition;

	public override void Show() {
		base.Show();
		speakContent.SetActive(false);
		StartDieTransition(true);
		ActionsManager.Instance.onTalkText += OnCharacterTalk;
		ActionsManager.Instance.onTalkStatusChanged += OnStatusChanged;
		ActionsManager.Instance.onPlayerDeath += OnPlayerDeath;
		ActionsManager.Instance.onFinish += OnFinish;
		ActionsManager.Instance.SendOnGameStart();
	}

	public override void Hide() {
		base.Hide();
		ActionsManager.Instance.onTalkText -= OnCharacterTalk;
		ActionsManager.Instance.onTalkStatusChanged -= OnStatusChanged;
		ActionsManager.Instance.onPlayerDeath -= OnPlayerDeath;
		ActionsManager.Instance.onFinish -= OnFinish;
	}

	private void OnFinish() {
		dieTransImg.color = Color.black;
		Invoke("RestartGame", 1.5f);
	}

	void RestartGame() {
		ActionsManager.Instance.ClearReferences();
		SceneManager.LoadScene(0);
	}


	private void OnCharacterTalk(Sprite ch, string text) {
		speakText.text = text;
		speakerImage.sprite = ch;
	}

	private void OnStatusChanged(ToughtState state) {
		speakContent.SetActive(state == ToughtState.Start);
	}

	private void OnPlayerDeath() {
		StartDieTransition(false);
	}

	private void Update() {
		if (inTransition) {
			Transition();
		}
	}

	private void StartDieTransition(bool fromMainPage) {
		transitionStartTime = Time.time;
		inTransition = true;
		motivationText.gameObject.SetActive(true);
		motivationText.text = fromMainPage ? "" : dialogs[Random.Range(0, dialogs.Count)];
	}

	private void Transition() {
		float timeSinceStart = Time.time - transitionStartTime;
		float percentage = timeSinceStart / transitionTime;
		dieTransImg.color = new Color(dieTransImg.color.r, dieTransImg.color.g, dieTransImg.color.b, Mathf.Lerp(0f, 1f, transitionCurve.Evaluate(percentage)));
		motivationText.color = new Color(motivationText.color.r, motivationText.color.g, motivationText.color.b, Mathf.Lerp(0f, 1f, textCurve.Evaluate(percentage)));
		if (percentage >= 1.0f) {
			inTransition = false;
			motivationText.gameObject.SetActive(false);
		}
	}

}
