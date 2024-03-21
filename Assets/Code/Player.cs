using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ITakeDamage {

	private bool _isFacingRight;
	private CharacterController2D _controller;
	private float _normalizedHorizontalSpeed;

	public Animator anim;

	public float MaxSpeed = 8;
	public float SpeedAccelerationOnGround = 10f;
	public float SpeedAccelerationInAir = 5f;
	public int MaxHealth = 100;
	public GameObject OuchEffect;

	public AudioClip PlayerHitSound;
	public AudioClip PlayerHealthSound;
	public AudioClip PlayerJump;

	public Projectile Projectile;
	public float FireRate;
	public Transform ProjectileFireLocation;


	public int Health { get; private set;}
	public bool isDead { get; private set;}

	private float _canFireIn;


	public void Awake(){

		_controller = GetComponent<CharacterController2D> ();
		_isFacingRight = transform.localScale.x > 0;
		Health = MaxHealth;
	}

	public void Update(){

		_canFireIn -= Time.deltaTime;

		if(!isDead)
			HandleInput ();

		var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

		if (isDead)
			_controller.SetHorizontalForce (0);
		else
			_controller.SetHorizontalForce (Mathf.Lerp (_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

		anim.SetBool ("Grounded", _controller.State.IsGrounded);
		anim.SetFloat ("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);

	}

	public void FinishLevel()
	{
		enabled = false;
		_controller.enabled = false;
	}
		

	public void Kill()
	{
		_controller.HandleCollisions = false;
		GetComponent<Collider2D>().enabled = false;
		isDead = true;
		Health = 0;

		_controller.SetForce (new Vector2(0,5));
	}

	public void EndGame(){
		Debug.Log ("GAME OVER");
	}

	public void RespawnAt(Transform spawnPoint)
	{
		if (GameControl.lifes <= 0) {
			EndGame ();
		} 
		else {
			if (!_isFacingRight)
				Flip ();

			isDead = false;
			GetComponent<Collider2D>().enabled = true;
			_controller.HandleCollisions = true;
			Health = MaxHealth;

			transform.position = spawnPoint.position;
		}
	}

	public void TakeDamage(int damage, GameObject instigator)
	{
		AudioSource.PlayClipAtPoint (PlayerHitSound, transform.position);
		gameObject.GetComponent<Animation> ().Play ("Player_RedFlash");
		FloatingText.Show (string.Format ("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 2f, 50));

		Instantiate (OuchEffect, transform.position, transform.rotation);
		Health -= damage;

		if (Health <= 0)
			LevelManager.Instance.KillPlayer ();
	}

	public void GiveHealth(int health, GameObject instagator)
	{
		AudioSource.PlayClipAtPoint (PlayerHealthSound, transform.position);
		FloatingText.Show(string.Format("+{0}", health), "PlayerGotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60f));
		Health = Mathf.Min (Health + health, MaxHealth);
	}

	private void HandleInput(){

		if (Input.GetKey (KeyCode.D)) {

			_normalizedHorizontalSpeed = 1;
			if (!_isFacingRight)
				Flip ();
		} else if (Input.GetKey (KeyCode.A)) {

			_normalizedHorizontalSpeed = -1;
			if (_isFacingRight)
				Flip ();

		} else {

			_normalizedHorizontalSpeed = 0;
		}

		if (_controller.CanJump && Input.GetKeyDown (KeyCode.Space)) {
			AudioSource.PlayClipAtPoint (PlayerJump, transform.position);
			_controller.Jump ();
		}

		//if (Input.GetMouseButtonDown (0))
		//	FireProjectile ();
		
	}

	private void FireProjectile()
	{
		if (_canFireIn > 0)
			return;
		var direction = _isFacingRight ? Vector2.right : -Vector2.right;

		var projectile = (Projectile)Instantiate (Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
		projectile.Initialize (gameObject, direction, _controller.Velocity);

		//projectile.transform.localScale = new Vector3 (_isFacingRight ? 1 : -1, 1, 1);

		_canFireIn = FireRate;
	}

	private void Flip(){

		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		_isFacingRight = transform.localScale.x > 0;
	}

}