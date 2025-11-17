using UnityEngine;
using UnityEngine.InputSystem;
using MGProject.Entities;

namespace MGProject.Player
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private InputActionAsset playerControlsMap;
		[SerializeField] private Movement playerMovement;
		[SerializeField] private Attacker playerAttack; 

		private InputAction moveAction;
		private InputAction jumpAction;

		private InputAction attackAction;

		private void Start()
		{
			moveAction = playerControlsMap.FindAction("Move");
			jumpAction = playerControlsMap.FindAction("Jump");
			attackAction = playerControlsMap.FindAction("Attack");

			moveAction.performed += (ctx) => playerMovement.Move(ctx.ReadValue<Vector2>().x);
			moveAction.canceled += (ctx) => playerMovement.Move(0);

			jumpAction.started += (ctx) => playerMovement.Jump();

			attackAction.started += (ctx) => playerAttack.ForwardAttack();
		}
	}
}