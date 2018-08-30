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

	public Action onLevelFinished;
	public Action<Vector2, Vector2> onLevelStart;
	public Action<Sprite, string> onTalkText;
	public Action<ToughtState> onTalkStatusChanged;

	public void SendOnLevelFinished() {
		if(onLevelFinished != null) {
			onLevelFinished();
		}
	}

	public void SendOnLevelStart(Vector2 fireflyPos, Vector2 humanPos) {
		if(onLevelStart != null) {
			onLevelStart(fireflyPos, humanPos);
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

}
