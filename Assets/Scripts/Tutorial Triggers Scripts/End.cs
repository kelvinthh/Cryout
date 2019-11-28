using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    GameObject blackpanel;
    // Start is called before the first frame update
    void Start()
    {
        blackpanel = GameObject.Find("/Inventory System/Inventory HUDv2/Black Panel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blackpanel.GetComponent<Animation>().Play();
            Invoke("ChangeScene", 2);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}
