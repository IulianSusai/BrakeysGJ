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
		} else {
			Destroy(gameObject);
		}
	}
#endregion

	public DesignSettings design;
	public PlayerInputSettings inputSettings;
}

[Serializable]
public struct DesignSettings
{
		
}

[Serializable]
public struct PlayerInputSettings
{
	public KeyCode swapKey;
}

