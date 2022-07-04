using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image HealthSprite;


    private float _maxHealth = 100f;

    public void SetMaxHealth(int health)
    {
        _maxHealth = health;
    }
    
    public void UpdateHealth(int health)
    {
        HealthSprite.fillAmount = health / _maxHealth;
    }
}
