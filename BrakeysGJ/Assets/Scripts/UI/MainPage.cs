using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPage : MenuPage {

	public override void Show() {
		base.Show();
	}

	public override void Hide() {
		base.Hide();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			UIManager.Instance.OpenPage(MenuPages.InGamePage);
		}
	}

}
