using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFloorController : MonoBehaviour
{
    public GameObject NextFloor;
    public GameObject boss;
    public Material material;
    /*public bool isActive;
    public Animator animator;

    public void ActivateNextFloor()
    {
        if(!isActive)
        {isActive = true;}
        animator.SetBool("isActive", isActive);
    }*/
    // Start is called before the first frame update
    void Start()
    {
        NextFloor.GetComponent<SwitchFloor>().enabled = false;
        material.DisableKeyword ("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.GetComponent<Health>().MaxHealth == 0)
        {
            NextFloor.GetComponent<SwitchFloor>().enabled = true;
            material.EnableKeyword ("_EMISSION");
        }
    }
}
