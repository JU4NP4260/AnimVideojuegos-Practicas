using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar; // Reference to the health bar UI
    private Animator animator;
    private Collider playerCollider;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();

        // Ensure the player has a Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Set the Rigidbody to be kinematic for collision detection without physical reactions
        rb.isKinematic = true;
    }

    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        Debug.Log("TakeDamage called with damage: " + damage + " at position: " + hitPosition);

        currentHealth -= damage;
        healthBar.value = currentHealth;

        // Get the center position of the player
        Vector3 playerCenter = playerCollider.bounds.center;

        // Calculate the hit direction
        float hitDirX = Mathf.Clamp((hitPosition.x - playerCenter.x) / playerCollider.bounds.size.x, -1f, 1f);
        float hitDirY = Mathf.Clamp((hitPosition.y - playerCenter.y) / playerCollider.bounds.size.y, -1f, 1f);

        // Ensure HitDirY is positive when the hit is above the center
        if (hitPosition.y > playerCenter.y)
        {
            hitDirY = Mathf.Abs(hitDirY);
        }

        animator.SetFloat("HitDirX", hitDirX);
        animator.SetFloat("HitDirY", hitDirY);
        animator.SetTrigger("GetHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        // Add any additional logic for when the player dies
    }
}
