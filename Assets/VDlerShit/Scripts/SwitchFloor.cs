using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchFloor : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player Character")       //"PlayerTrigger"
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            myMaterial.DisableKeyword ("_EMISSION");
            enabled = false;
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "NextFloor")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }*/
    void Update()
    {}
}
