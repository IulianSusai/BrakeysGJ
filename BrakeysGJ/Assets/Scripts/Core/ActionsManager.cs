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
}
