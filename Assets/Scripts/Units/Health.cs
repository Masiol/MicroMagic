using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public HealthUI healthUI;

    private bool isDeath;

    void Start()
    {
        maxHealth = GetComponent<Unit>().unitSO.health;
        currentHealth = maxHealth;
        healthUI.SetHealth(maxHealth);
    }

    public void TakeDamage(int damage, bool _specjalAttack)
    {
        if (_specjalAttack)
        {
            StartCoroutine(DoDelayedDamage(damage));
        }
        else
        {
            currentHealth -= damage;
            UpdateHealthUI();
        }

        if (currentHealth <= 0 && !isDeath)
        {
            isDeath = true;
            if (GetComponent<Unit>().isEnemyUnit)
            {
                GameManager.instance.UnregisterUnit(true, GetComponent<Unit>().unitSO.commandPoints);
            }
            else
            {
                GameManager.instance.UnregisterUnit(false, GetComponent<Unit>().unitSO.commandPoints);
            }
            Die();
        }
    }

    private IEnumerator DoDelayedDamage(int damage)
    {
        yield return new WaitForSeconds(1f);
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDeath)
        {
            isDeath = true;
            if (GetComponent<Unit>().isEnemyUnit)
            {
                GameManager.instance.UnregisterUnit(true, GetComponent<Unit>().unitSO.commandPoints);
            }
            else
            {
                GameManager.instance.UnregisterUnit(false, GetComponent<Unit>().unitSO.commandPoints);
            }
            Die();
        }
        UpdateHealthUI();
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