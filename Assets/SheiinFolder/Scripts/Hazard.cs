using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage = 10; // Amount of damage the hazard deals

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        Debug.Log("Collision");
        if (playerHealth != null)
        {
            Debug.Log("Collision with player");
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            playerHealth.TakeDamage(damage, hitDirection);
        }
    }
}
