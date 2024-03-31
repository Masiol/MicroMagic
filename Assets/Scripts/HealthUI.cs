using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBar;

    public void SetHealth(int maxHealth)
    {
        healthBar.fillAmount = 1f; // Ustawienie pocz¹tkowej pe³noœci na maksimum (1)
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        float normalizedHealth = (float)currentHealth / (float)maxHealth; // Normalizacja zdrowia do zakresu od 0 do 1
        healthBar.fillAmount = normalizedHealth; // Ustawienie fillAmount na znormalizowane zdrowie
    }
}
