using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool IsPickUp = true;
    public bool DestroyOnInteract = true;
    public UnityEvent OnInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(!IsPickUp)
            return;
        PlayerController player = other.GetComponent<PlayerController>();
        if(!player)
            return;
        player.InteractWith(this);
    }

    public  virtual bool Interact(Character character)
    {
        if(!CanInteract(character))
            return false;
        OnInteract?.Invoke();
        if(DestroyOnInteract)
            Destroy(gameObject);
        return true;
    }

    public virtual bool CanInteract(Character character)
    {
        return true;
    }
}
