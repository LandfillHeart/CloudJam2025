using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
	[SerializeField] private float walkSpeed;

	private Rigidbody2D rb;

	private float directionHorizontal;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		// move position will override the vertical movement from gravity
		rb.AddForceX(walkSpeed * directionHorizontal, ForceMode2D.Impulse);
		if(Mathf.Abs(rb.linearVelocityX) > walkSpeed)
		{
			rb.AddForceX((Mathf.Abs(rb.linearVelocityX) - walkSpeed) * -directionHorizontal, ForceMode2D.Impulse);
		}
	}

	public void Move(float direction)
	{
		direction = Mathf.Clamp01(direction);
		this.directionHorizontal = direction;
		if (Mathf.Approximately(direction, 0f)) 
		{
			rb.AddForceX(-rb.linearVelocityX, ForceMode2D.Impulse);
		}
	}


	public void Jump()
	{

	}

}
