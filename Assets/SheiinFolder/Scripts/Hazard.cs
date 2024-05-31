using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage = 10; // Amount of damage the hazard deals

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            playerHealth.TakeDamage(damage, contactPoint);
        }
    }
}
