using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadableItemSystem : MonoBehaviour
{
    GameObject player;
    GameObject panel;
    GameObject proceedText;

    [SerializeField]
    private AudioClip[] paperturn;

    [SerializeField]
    private GameObject instansiateObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        panel = gameObject.transform.GetChild(0).gameObject;
        proceedText = gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && panel.activeSelf)
        {
            HideText();
        }
    }

    public void DisplayText(string content)
    {
        player.GetComponent<AudioSource>().PlayOneShot(paperturn[0]);
        panel.SetActive(true);
        proceedText.SetActive(true);
        GameObject tempGameObject = Instantiate(instansiateObject, transform.GetChild(1));
        tempGameObject.GetComponent<TextMeshProUGUI>().SetText(content);
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }

    private void HideText()
    {
        player.GetComponent<AudioSource>().PlayOneShot(paperturn[1]);
        panel.SetActive(false);
        proceedText.SetActive(false);
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            Destroy(transform.GetChild(1).GetChild(i).gameObject);
        }
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }
}
