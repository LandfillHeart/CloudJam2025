using MGProject.Player;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SimpleInteractable : MonoBehaviour
{
	[SerializeField] private bool singleUse = true;

	private bool hasInteracted;

	public event Action OnPlayerInteract;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			PlayerController.Instance.interactableInRange = this;
			Debug.Log("Entered interaction range");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			PlayerController.Instance.interactableInRange = null;
			Debug.Log("Exited interaction range");
		}
	}

	public void Interact()
	{
		if (singleUse && hasInteracted) return;
		hasInteracted = true;

		Debug.Log("Interacted with object");
		OnPlayerInteract?.Invoke();
	}
}
