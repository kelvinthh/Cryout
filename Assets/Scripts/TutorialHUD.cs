using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHUD : MonoBehaviour
{
    GameObject player;
    GameObject panel;
    GameObject proceedText;
    [SerializeField]
    private AudioClip[] paperturn;
    private List<GameObject> tutorials = new List<GameObject>();
    private int shownTutorial;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        panel = gameObject.transform.GetChild(0).gameObject;
        proceedText = gameObject.transform.GetChild(2).gameObject;

        //Put every child under the 'Tutorials' GameObject into the list
        foreach (Transform child in gameObject.transform.GetChild(1))
        {
            tutorials.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeScale();
    }

    private void TimeScale()
    {
        TimePause();
        if (Input.GetKeyDown(KeyCode.Mouse0) && panel.activeSelf)
        {
            HideTutorial(shownTutorial);
        }
    }
    private void TimePause()
    {
        if (panel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void HideTutorial(int i)
    {
        player.GetComponent<AudioSource>().PlayOneShot(paperturn[1]);
        panel.SetActive(false);
        proceedText.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
    }

    public void ShowTutoral(int i)
    {
        HideTutorial(shownTutorial);
        player.GetComponent<AudioSource>().PlayOneShot(paperturn[0]);
        panel.SetActive(true);
        proceedText.SetActive(true);
        gameObject.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
        shownTutorial = i;
    }
}
