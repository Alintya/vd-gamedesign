using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image HealthSprite;


    private float _maxHealth = 100f;

    public void SetMaxHealth(float health)
    {
        _maxHealth = health;
    }
    
    public void UpdateHealth(float health)
    {
        HealthSprite.fillAmount = health / _maxHealth;
    }
}
