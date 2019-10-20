using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

	public float velX = 5f; 	// contains X velocity, can be modified in inspector
	float velY = 0f; 			// contains Y velocity which is 0
	Rigidbody2D rb;				// rigidBody2D component reference
	public float lifeTime = 5f;	// life time of a rocket, can be modified in inspector

	void Awake()
	{
		// reverse rocket sprite if rocket flies in negative X direction

		Vector3 localScale = transform.localScale;
		if (velX < 0) {
			localScale.x *= -1;
			transform.localScale = localScale;
		}
	}

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> (); // initiate rigidBody component
		
	}
	
	// Update is called once per frame
	void Update () {

		rb.velocity = new Vector2 (velX, velY); // set velocity to the rocket
		Invoke ("destroyGameobject", lifeTime); // invoke destroy rocket function after lifeTime ran out
		
	}

	void destroyGameobject ()
	{
		foreach (Transform child in transform) {			//
			child.GetComponent<ParticleSystem> ().Stop ();	// stop children to emit particles and destroy them in 3 seconds
			Destroy (child.gameObject, 3f);					//
		}
		transform.DetachChildren (); 	// detach children to make particles be visible after rocket is destroyed
		Destroy (gameObject);			// destroy rocket
	}
}
