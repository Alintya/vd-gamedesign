using UnityEngine;


public class RoomInterior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0f, 1f)> 0.5f)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
