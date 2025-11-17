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

		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			groundLayer = LayerMask.GetMask("Ground");
		}

		private void FixedUpdate()
		{
			// move position will override the vertical movement from gravity
			rb.AddForceX(walkSpeed * directionHorizontal, ForceMode2D.Impulse);
			if (Mathf.Abs(rb.linearVelocityX) > walkSpeed)
			{
				rb.AddForceX((Mathf.Abs(rb.linearVelocityX) - walkSpeed) * -directionHorizontal, ForceMode2D.Impulse);
			}

			if (rb.linearVelocityY < 0f)
			{
				rb.AddForceY(Physics2D.gravity.y, ForceMode2D.Force);
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
			Debug.DrawRay(transform.position, Vector2.down * 1.15f, Color.yellow, 3f);
			if (!Physics2D.Raycast(transform.position, Vector2.down, 1.15f, groundLayer)) { return; }
			rb.AddForceY(jumpStrength, ForceMode2D.Impulse);
		}

	}
}

