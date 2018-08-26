using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[SerializeField] private List<Level> levelsPrefabs;
	private int currentLevelIndex;

	private GameObject currentLevel;

	private void Awake() {
		ActionsManager.Instance.onLevelFinished += OnLevelFinished;
	}

	private void Start() {
		ChangeLevel(0);
	}

	private void ChangeLevel(int index) {
		if (index < levelsPrefabs.Count) {
			if (currentLevel != null) {
				Destroy(currentLevel);
			}
			currentLevelIndex = index;
			//currentLevel = Instantiate(levelsPrefabs[currentLevelIndex].levelPrefab, transform);
			ActionsManager.Instance.SendOnLevelStart(levelsPrefabs[currentLevelIndex].fireflyPosition, levelsPrefabs[currentLevelIndex].humanPosition);
		}
		Debug.Log("here");
	}

	private void OnLevelFinished() {
		ChangeLevel(++currentLevelIndex);
	}


}

[Serializable]
public struct Level
{
	public GameObject levelPrefab;
	public Vector2 fireflyPosition;
	public Vector2 humanPosition;
}
