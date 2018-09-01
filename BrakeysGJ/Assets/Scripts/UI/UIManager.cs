using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

#region Instance
	public static UIManager Instance { private set; get; }

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}
#endregion

	[SerializeField] private List<MenuPage> gamePages;
	private MenuPage currentPage;

	private void Start() {
		OpenPage(MenuPages.MainPage);
	}

	public void OpenPage( MenuPages pageType ) {
		if(currentPage != null) {
			currentPage.Hide();
			Destroy(currentPage.gameObject);
		}
		currentPage = GetPage(pageType);
		currentPage.Show();
	}

	private MenuPage GetPage(MenuPages pageType) {
		MenuPage tmpPage = Instantiate(gamePages[(int)pageType]);
		tmpPage.transform.SetParent(gameObject.transform, false);
		tmpPage.transform.SetAsLastSibling();
		return tmpPage;
	}

}

public enum MenuPages
{
	MainPage,
	InGamePage
}
