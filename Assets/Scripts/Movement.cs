using UnityEngine;

namespace MGProject.Entities
{

	[RequireComponent(typeof(Rigidbody2D))]
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float walkSpeed;
		[SerializeField] private float jumpStrength;

		private Rigidbody2D rb;

		private float directionHorizontal;
		private LayerMask groundLayer;

		private float groundCheckLength;

		private bool IsGrounded => Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, groundLayer);



		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			groundLayer = LayerMask.GetMask("Ground");
			groundCheckLength = transform.localScale.y + transform.localScale.y / 10;
		}

		private void FixedUpdate()
		{
			// move position will override the vertical movement from gravity
			

			if(Mathf.Approximately(Mathf.Abs(rb.linearVelocityX), 0f)) 
			{
				rb.AddForceX(-rb.linearVelocityX, ForceMode2D.Impulse);
			}

			rb.AddForceX(walkSpeed * directionHorizontal, ForceMode2D.Impulse);
			if (Mathf.Abs(rb.linearVelocityX) > walkSpeed)
			{
				rb.AddForceX((Mathf.Abs(rb.linearVelocityX) - walkSpeed) * -directionHorizontal, ForceMode2D.Impulse);
			}

			
		}

		public void Move(float direction)
		{
			direction = Mathf.Clamp(direction, -1, 1);
			this.directionHorizontal = direction;
			if (!Mathf.Approximately(direction, 0f))
			{
				float facing = direction < 0f ? 180 : 0;
				transform.rotation = Quaternion.Euler(0, facing, 0);
			}

			if (Mathf.Approximately(direction, 0f))
			{
				rb.AddForceX(-rb.linearVelocityX, ForceMode2D.Impulse);
			}
		}


		public void Jump()
		{
			Debug.DrawRay(transform.position, Vector2.down * groundCheckLength, Color.yellow, 3f);
			if (!IsGrounded) { return; }
			rb.AddForceY(jumpStrength, ForceMode2D.Impulse);
		}

	}
}

