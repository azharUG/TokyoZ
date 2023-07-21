using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health value for the player
    public int currentHealth; // Current health value for the player

    public Image healthBar; // Reference to the UI image representing the health bar

    private void Start()
    {
        currentHealth = maxHealth; // Set the player's current health to the maximum health at the start
        UpdateHealthUI(); // Update the UI health bar
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Subtract the damage amount from the player's current health

        if (currentHealth <= 0)
        {
            Die(); // If the player's health reaches zero or below, call the Die() function
        }

        UpdateHealthUI(); // Update the UI health bar
    }

    private void Die()
    {
        // Perform actions for player death (e.g., game over, restart level, etc.)
        Debug.Log("Player has died!");
    }

    private void UpdateHealthUI()
    {
        // Calculate the health percentage
        float healthPercentage = (float)currentHealth / maxHealth;

        // Update the fill amount of the health bar image
        healthBar.fillAmount = healthPercentage;
    }
}
