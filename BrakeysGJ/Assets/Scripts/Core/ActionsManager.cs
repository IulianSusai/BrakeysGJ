using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager {

#region Instance
	private static ActionsManager instance;

	public static ActionsManager Instance {
		get {
			if(instance == null) {
				instance = new ActionsManager();
			}
			return instance;
		}
	}

	private ActionsManager() { }
	#endregion

	public Action onGameStart;
	public Action onGameEnd;
	public Action<Sprite, string> onTalkText;
	public Action<ToughtState> onTalkStatusChanged;
	public Action<bool> onTimeWaitTrigger;
	public Action onPlayerDeath;
	public Action onFinish;

	public void SendOnFinish() {
		if(onFinish!= null) {
			onFinish();
		}
	}

	public void SendOnPlayerDeath() {
		if(onPlayerDeath != null) {
			onPlayerDeath();
		}
	}

	public void SendOnGameStart() {
		if(onGameStart != null) {
			onGameStart();
		}
	}

	public void SendOnGameEnd() {
		if(onGameEnd != null) {
			onGameEnd();
		}
	}

	public void SendOnTalkText(Sprite chSprite, string talkText) {
		if(onTalkText != null) {
			onTalkText(chSprite, talkText);
		}
	}

	public void SendTalkStatusChanged(ToughtState state) {
		if(onTalkStatusChanged != null) {
			onTalkStatusChanged(state);
		}
	}

	public void SendOnTimeWaitTriger(bool enter) {
		if(onTimeWaitTrigger != null) {
			onTimeWaitTrigger(enter);
		}
	}

	public void ClearReferences() {
		onGameEnd = null;
		onGameStart = null;
		onPlayerDeath = null;
		onTalkText = null;
		onTalkStatusChanged = null;
		onTimeWaitTrigger = null;
	}

}
