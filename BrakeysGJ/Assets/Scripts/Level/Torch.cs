using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	[SerializeField] private Light pointLight;
	[SerializeField] private float maxIntensity;

	private bool isLightingUp;
	private float activateStartTime;
	private float startLightIntensity;
	private float endLightIntensity;

	public bool isActive { private set; get; }

	private void Start() {
		isActive = false;
		pointLight.intensity = 0f;
		pointLight.gameObject.SetActive(false);
	}

	public void ActivateTorch() {
		isActive = true;
		isLightingUp = true;
		activateStartTime = Time.time;
		startLightIntensity = 0f;
		endLightIntensity = maxIntensity != 0f ? maxIntensity : GameManager.Instance.design.lightIntensity;
		pointLight.gameObject.SetActive(true);
	}

	private void Update() {
		if (isLightingUp) {
			LightUp();
		}
	}

	private void LightUp() {
		float timeSinceStarted = Time.time - activateStartTime;
		float percentage = timeSinceStarted / GameManager.Instance.design.torchLightTime;

		pointLight.intensity = Mathf.Lerp(startLightIntensity, endLightIntensity, GameManager.Instance.design.torchLightCurve.Evaluate(percentage));
		if(percentage >= 1.0f) {
			isLightingUp = false;
		}
	}


}
