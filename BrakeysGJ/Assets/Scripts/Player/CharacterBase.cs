using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {

	protected Rigidbody2D rb;

	public virtual void CheckMoveInput() { }
	public virtual void CheckActionInput() { }
	public virtual void StopMoving() {
		rb.velocity = Vector2.zero;
	}

}
