using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float StartHealth = 100f;
    public float DamageFactor = 1f;
    public UnityEvent OnDeath;
    public Healthbar Healthbar;
    
    public bool IsDead => _currentHealth <= 0;

    [SerializeField]
    private float _currentHealth;

    void Awake()
    {
        _currentHealth = StartHealth;
        if (Healthbar == null)
            Healthbar = GetComponentInChildren<Healthbar>();
        Healthbar?.SetMaxHealth(MaxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount * DamageFactor;

        if (Healthbar != null)
            Healthbar.UpdateHealth(_currentHealth);

        if (_currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        _currentHealth += amount;

        if (_currentHealth > MaxHealth)
            _currentHealth = MaxHealth;

        if (Healthbar != null)
            Healthbar.UpdateHealth(_currentHealth);
    }

    public void SetMaxHealth(float health)
    {
        MaxHealth = health;

        if (Healthbar != null)
            Healthbar.SetMaxHealth(health);
    }

    public void IncreaseMaxHealth(float amount)
    {
        SetMaxHealth(MaxHealth + amount);
        Heal(amount);
    }

    private void Die()
    {
        // TODO: add animation etc.
        OnDeath?.Invoke();
        
        if (!gameObject.CompareTag("Player"))
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
