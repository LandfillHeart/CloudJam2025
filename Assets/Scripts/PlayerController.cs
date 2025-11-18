using UnityEngine;
using UnityEngine.InputSystem;
using MGProject.Entities;
using System;

namespace MGProject.Player
{
	public class PlayerController : MonoBehaviour
	{
		#region Singleton
		private static PlayerController instance;
		public static PlayerController Instance => instance;

		private void Awake()
		{
			if(instance == null)
			{
				instance = this;
				return;
			}
			Destroy(gameObject);
		}
		#endregion

		[SerializeField] private InputActionAsset playerControlsMap;
		[SerializeField] private Movement playerMovement;
		[SerializeField] private Attacker playerAttack;

		[NonSerialized] public SimpleInteractable interactableInRange;

		private InputAction moveAction;
		private InputAction jumpAction;

		private InputAction attackAction;

		private InputAction interactAction;

		private void Start()
		{
			moveAction = playerControlsMap.FindAction("Move");
			jumpAction = playerControlsMap.FindAction("Jump");
			attackAction = playerControlsMap.FindAction("Attack");
			interactAction = playerControlsMap.FindAction("Interact");

			moveAction.performed += (ctx) => playerMovement.Move(ctx.ReadValue<Vector2>().x);
			moveAction.canceled += (ctx) => playerMovement.Move(0);

			jumpAction.started += (ctx) => playerMovement.Jump();

			attackAction.started += (ctx) => playerAttack.ForwardAttack();

			interactAction.started += (ctx) => Interact();
		}

		private void Interact()
		{
			if (interactableInRange == null) return;
			interactableInRange.Interact();
		}
	}
}