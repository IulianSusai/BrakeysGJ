using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {
	
	public virtual void OnKeyPressed(KeyCode key) {	}
	public virtual void OnPlayerDeath() { }

}
