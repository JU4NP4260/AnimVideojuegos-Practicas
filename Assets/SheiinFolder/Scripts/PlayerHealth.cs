using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar; // Reference to the health bar UI
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        float hitDirX = Mathf.Clamp(hitDirection.z, -1f, 1f);
        float hitDirY = Mathf.Clamp(hitDirection.y, -1f, 1f);

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
