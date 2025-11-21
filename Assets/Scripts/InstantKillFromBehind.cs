using MGProject.Entities;
using UnityEngine;

public class InstantKillFromBehind : MonoBehaviour
{
    [SerializeField] private float killRange = 1.5f; // distanza massima
    [SerializeField] private float maxBackAngle = 60f; // angolo entro il quale sei "alle spalle"
    [SerializeField] private LayerMask enemyLayer;

    public void TryInstantKill()
    {
        Debug.Log("sto testando tryInstantKill");
        // Raycast frontale per trovare un bersaglio davanti al player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, killRange, enemyLayer);
        Debug.DrawRay(transform.position, transform.right * killRange, Color.red, 3f);
        if (!hit)
        {
            Debug.Log("Nessun bersaglio davanti.");
            return;
        }

        Transform enemy = hit.transform;

        if (IsBehind(enemy))
        {
            Debug.Log("Uccisione istantanea!");
            Health health = enemy.GetComponent<Health>();
            
            if (health != null)
                health.Damage(9999); // “istakill”
        }
        else
        {
            Debug.Log("Non sei dietro il nemico!");
        }
    }

    private bool IsBehind(Transform enemy)
    {
        // direzione nemico -> player
        Vector2 dirToPlayer = (transform.position - enemy.position).normalized;

        // forward del nemico 
        Vector2 enemyForward = enemy.right;

        // calcola angolo
        float angle = Vector2.Angle(enemyForward, dirToPlayer);

        // sei dietro se l'angolo è piccolo
        return angle < maxBackAngle;
    }    
}
