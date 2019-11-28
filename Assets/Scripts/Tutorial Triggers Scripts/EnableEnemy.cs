using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemy : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    TutorialHUD tutorial;
    AudioSource audioSource;
    AudioClip danger;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.Find("/Enemy");
        tutorial = GameObject.Find("/Tutorial HUD").GetComponent<TutorialHUD>();
        enemy.SetActive(false);
        audioSource = GameObject.Find("/Level Stuff").GetComponent<AudioSource>();
        danger = Resources.Load<AudioClip>("Sounds/464311__nightwolfcfm__bunnyman-chase");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.SetActive(true);
            player.transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
            tutorial.ShowTutoral(4);
            player.GetComponent<HealthSystem>().SetObjective("Run back to the brick house! Press 'Tab' and right-click on the key to use it");
            audioSource.Stop();
            audioSource.clip = danger;
            audioSource.volume = 0.3f;
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
