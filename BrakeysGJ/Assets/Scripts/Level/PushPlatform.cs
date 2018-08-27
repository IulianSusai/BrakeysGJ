using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlatform : MonoBehaviour {

	[SerializeField] private float pushForce;


	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
			playerRb.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
		}
	}
}
