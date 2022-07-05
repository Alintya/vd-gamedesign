using UnityEngine;


public class AddRoom : MonoBehaviour
{
    private ProceduralGenerator templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<ProceduralGenerator>();
        templates.Rooms.Add(this.gameObject);
    } 
}
