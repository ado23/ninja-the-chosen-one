﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour, IPlayerRespawnListener {

	public int curHealth;
	public int maxHealth;

	private Vector2 _startPosition;

	public float distance;
	public float wakeRange;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;

	public bool awake = false;
	public bool lookingRight = true;

	public GameObject bullet;
	public Transform target;
	public Animator anim;
	public Transform shootPointLeft;
	public Transform shootPointRight;

	public GameObject KilledEffect;
	public AudioClip DestroyedTurret;
	public AudioClip TurretShot;

	void Awake () 
	{
		anim = gameObject.GetComponent<Animator> ();
	}

	void Start()
	{
		curHealth = maxHealth;
		_startPosition = transform.position;
	}

	void Update()
	{
		anim.SetBool ("Awake", awake);
		anim.SetBool ("LookingRight", lookingRight);

		RangeCheck ();

		if (target.transform.position.x > transform.position.x) {
			lookingRight = true;
		}

		if (target.transform.position.x < transform.position.x) {
			lookingRight = false;
		}

		if (curHealth <= 0) {
			AudioSource.PlayClipAtPoint (DestroyedTurret, transform.position);
			gameObject.SetActive (false);
			Instantiate (KilledEffect, transform.position, transform.rotation);
		}
	}

	void RangeCheck()
	{
		distance = Vector3.Distance (transform.position, target.transform.position);

		if (distance < wakeRange) {
			
			awake = true;
		}

		if (distance > wakeRange) {

			awake = false;
		}
	}

	public void Attack(bool attackingRight)
	{
		bulletTimer += Time.deltaTime;

		if (bulletTimer >= shootInterval) {

			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize ();

			if (!attackingRight) {
				AudioSource.PlayClipAtPoint (TurretShot, transform.position);
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;

				bulletTimer = 0;
			}

			if (attackingRight) {
				AudioSource.PlayClipAtPoint (TurretShot, transform.position);
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;

				bulletTimer = 0;
			}

		}
	}

	public void Damage (int damage)
	{
		curHealth -= damage;

		gameObject.GetComponent<Animation> ().Play ("Player_RedFlash");
	}

	public void OnPlayerRespawnInThisCheckpoint (Checkpoint checkpoint, Player player)
	{
		transform.position = _startPosition;
		gameObject.SetActive (true);
		curHealth = maxHealth;
		Debug.Log ("Udes li ikako u respawn turreta?");
	}
}
