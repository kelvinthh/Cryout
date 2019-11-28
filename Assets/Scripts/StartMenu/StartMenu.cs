using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private GameObject logo;
    private GameObject buttons;
    // Start is called before the first frame update
    void Start()
    {
        logo = transform.GetChild(0).gameObject;
        buttons = transform.GetChild(1).gameObject;
        buttons.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!logo.GetComponent<Animation>().isPlaying)
        {
            buttons.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(3);
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene(5);
        }
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuiteGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game.");
    }
}
