using System;
using UnityEngine;

namespace MGProject.Entities
{
	public class Health : MonoBehaviour
	{
		[SerializeField] private float maxHealth;

		private float currentHealth;

		public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

		public float CurrentHealth
		{
			get => currentHealth;
			set
			{
				currentHealth = Mathf.Clamp(value, 0f, maxHealth);
				OnHealthChanged?.Invoke(currentHealth);
				if(currentHealth <= 0f)
				{
					Die();
				}

			} 
		}

		public event Action<float> OnHealthChanged;

		private void Awake()
		{
			currentHealth = maxHealth;
			
		}

		private void Die()
		{
			// create event, disable behaviours, etc etc
			Debug.Log($"Entity Killed: {gameObject.name}");

			Destroy(gameObject);
		}

		public void Damage(float damage) //here is not joining in any test maybe i should look into attacker to see it there's something wrong there
		{
			CurrentHealth -= damage;
			Debug.Log($"Taking damage, current health: {CurrentHealth}");
		}

	}
}

