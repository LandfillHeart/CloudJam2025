using UnityEngine;

public class FieldOfViewCheck : MonoBehaviour
{
	[SerializeField] private float maxRange;
	[SerializeField] private float degreesOfVision;

	// there are two correct ways to handle this, in theory
	// 1. shoot a cone of rays, with the two extremes at the edges of the fov
	// 2. make a circlecast, then see if the player is within the fov.
	// i'll try option 2 first

	private Collider2D hitCache = new();
	private LayerMask playerLayer;

	private float fovCheck;

	private void Start()
	{
		playerLayer = LayerMask.GetMask("Player");
		// angle to half angle
		fovCheck = degreesOfVision / 2;
		// angle euler to radians
		fovCheck *= Mathf.PI / 180;
		// angle to cos of angle
		//fovCheck = Mathf.Cos(fovCheck);
		Debug.Log(fovCheck);

		// preemptive check on right angle
		// float angle = Mathf.Acos(Vector2.Dot((transform.right - transform.position).normalized, transform.right));
		// Debug.Log(angle);
	}

	private void FixedUpdate()
	{
		hitCache = Physics2D.OverlapCircle(transform.position, maxRange, playerLayer);
		
		if (hitCache == null) return;
		// dot prod = cos of angle between the two points
		float angle = Mathf.Acos(Vector2.Dot((hitCache.transform.position - transform.position).normalized, transform.right));

		// greater or equal than BECAUSE the tighter the angle between the two, the higher the dotprod
		if (angle <= fovCheck)
		{
			Debug.Log("Player in FOV");
			Debug.Log(angle);
			Debug.DrawRay(transform.position, (hitCache.transform.position - transform.position), Color.yellow, 3f);
		} 
	}
}
