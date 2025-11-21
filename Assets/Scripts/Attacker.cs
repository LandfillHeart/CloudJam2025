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
	[SerializeField] public InstantKillFromBehind instantKill;

	private bool CanAttack => remainingCooldown <= 0f;

	private void Awake()
	{
		enemyLayer = LayerMask.GetMask("NPC");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log("pressed e");
			instantKill.TryInstantKill();    //null reference qui devo risolverla
		}

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

		Debug.Log(hitCache == true); //sta displayando sempre false, giusto o sbagliato? risolto ------ non avevo capito il layermask da mettere
		if (!hitCache) return;

		enemyCache = hitCache.transform.GetComponent<Health>();
		enemyCache.Damage(attackDamage);
	}
}
