using UnityEngine;

public class PathedProjectileSpawner : MonoBehaviour {

	public Transform Destination;
	public PathedProjectile Projectile;

	public float Speed;
	public float FireRate;

	private float _nextShotInSeconds;

	public AudioClip ProjectileShot;
	public float distance;
	public Transform target;
	public float wakeRange;
	public float volume = 0.5f;

	public void Start()
	{
		_nextShotInSeconds = FireRate;
	}

	public void Update()
	{
		if ((_nextShotInSeconds -= Time.deltaTime) > 0)
			return;

		RangeCheck ();
		_nextShotInSeconds = FireRate;
		var projectile = (PathedProjectile)Instantiate (Projectile, transform.position, transform.rotation);
		projectile.Initalize (Destination, Speed);
	}

	public void OnDrawGizmos()
	{
		if (Destination == null)
			return;

		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, Destination.position);
	}

	void RangeCheck()
	{
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance < wakeRange) {
			AudioSource.PlayClipAtPoint (ProjectileShot, transform.position, volume);
		}
	}

}
