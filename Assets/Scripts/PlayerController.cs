using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private InputActionAsset playerControlsMap;
	[SerializeField] private Movement playerMovement;

	private InputAction moveAction;

	private void Start()
	{
		moveAction = playerControlsMap.FindAction("Move");

		moveAction.performed += (ctx) => playerMovement.Move(ctx.ReadValue<Vector2>().x);
		moveAction.canceled += (ctx) => playerMovement.Move(0);
	}

}
