using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public Slider beaconBar;
    public Image fillImage;



    public void TakeDamage(int damage)
    {
        health -= damage;
        // Debug.Log("Taken " + damage + " damage. Remaining health: " + health);
        if (gameObject.CompareTag("Player"))
        {
            UpdatePlayerHealth();
        }
        else if (!gameObject.CompareTag("Player") &&health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerHealth()
    {
        beaconBar.value = health;
        AudioManager.Instance.PlayerHurt();
        float percent = (float)health / beaconBar.maxValue;
        if (percent < 0.3f)
        {
            fillImage.color = Color.red;
        }
        else
        {
            fillImage.color = Color.green;
        }

        if (health <= 0)
        {
            // Debug.Log("Player has died!");
            AudioManager.Instance.PlayerDied();
            RescueTimer.Instance.YouLose("PLayer has died!");
        }
        
    }
}
