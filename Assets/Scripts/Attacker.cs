using MGProject.Entities;
using UnityEngine;

public class Attacker : MonoBehaviour
{
	[SerializeField] private float attackRange;
	[SerializeField] private float attackDamage;
	[SerializeField] private float attackCooldown;

	private float remainingCooldown;

	private LayerMask enemyLayer;
	
	private RaycastHit2D hitCache;
	private Health enemyCache;

	private bool CanAttack => remainingCooldown <= 0f;

	private void Awake()
	{
		enemyLayer = LayerMask.GetMask("NPC");
	}

	private void Update()
	{
		if (CanAttack) return;
		remainingCooldown -= Time.deltaTime;
	}

	public void ForwardAttack()
	{
		if (!CanAttack) return;
		Debug.Log("Attacking...");
		remainingCooldown = attackCooldown;
		
		Debug.DrawRay(transform.position, transform.right * attackRange, Color.red, 3f);

		hitCache = Physics2D.Raycast(transform.position, transform.right, attackRange, enemyLayer);

		Debug.Log(hitCache == true);
		if (!hitCache) return;

		enemyCache = hitCache.transform.GetComponent<Health>();
		enemyCache.Damage(attackDamage);
	}
}
