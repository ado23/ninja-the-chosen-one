using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour, ITakeDamage, IPlayerRespawnListener {

	public float Speed;
	public int PointsToGivePlayer;

	public int curHealth;
	public int maxHealth;
	public GameObject KilledEffect;

	private CharacterController2D _controller;
	private Vector2 _direction;
	private Vector2 _startPosition;
	private float _canFireIn;

	public AudioClip KilledEnemy;

	public void Start()
	{
		_controller = GetComponent<CharacterController2D> ();
		_direction = new Vector2 (-1, 0);
		_startPosition = transform.position;

		curHealth = maxHealth;
	}

	public void Update()
	{
		_controller.SetHorizontalForce (_direction.x * Speed);

		if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight)) 
		{
			_direction = -_direction;
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}

		var raycast = Physics2D.Raycast (transform.position, _direction, 10, 1 << LayerMask.NameToLayer ("Player"));
		if (!raycast) {
			if (curHealth <= 0) {
				Debug.Log (curHealth);
				AudioSource.PlayClipAtPoint (KilledEnemy, transform.position);
				gameObject.SetActive (false);
				Instantiate (KilledEffect, transform.position, transform.rotation);
			}
			return;
		}
		if (raycast) {
			if (curHealth <= 0) {
				Debug.Log (curHealth);
				AudioSource.PlayClipAtPoint (KilledEnemy, transform.position);
				gameObject.SetActive (false);
				Instantiate (KilledEffect, transform.position, transform.rotation);
			}
		}


	}

	public void TakeDamage (int damage, GameObject instigator)
	{
		if (PointsToGivePlayer != 0) {

			var projectile = instigator.GetComponent<Projectile> ();
			if (projectile.Owner != null && projectile.Owner.GetComponent<Player> () != null) {

				GameManager.Instance.AddPoints (PointsToGivePlayer);
				FloatingText.Show (string.Format ("+{0}!", PointsToGivePlayer), "PointStarText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1.5f, 50));
			}
		}
			
		gameObject.SetActive (false);
	}

	public void OnPlayerRespawnInThisCheckpoint (Checkpoint checkpoint, Player player)
	{
		_direction = new Vector2 (-1, 0);
		transform.localScale = new Vector3 (1, 1, 1);
		transform.position = _startPosition;
		gameObject.SetActive (true);
		curHealth = maxHealth;
	}


	public void Damage (int damage)
	{
		curHealth -= damage;
		Debug.Log ("Damage kod zombie");
		//gameObject.GetComponent<Animation> ().Play ("Player_RedFlash");
	}

}
