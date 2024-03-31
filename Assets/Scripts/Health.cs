using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public HealthUI healthUI;

    void Start()
    {
        maxHealth = GetComponent<Unit>().unitSO.health;
        currentHealth = maxHealth;
        healthUI.SetHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void UpdateHealthUI()
    {
        healthUI.UpdateHealthUI(currentHealth, maxHealth);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}