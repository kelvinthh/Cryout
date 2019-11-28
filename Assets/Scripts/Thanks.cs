using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thanks : MonoBehaviour
{
    [SerializeField]
    GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowReturn", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }

    void ShowReturn()
    {
        text.SetActive(true);
        text.GetComponent<Animation>().Play();
    }
}
