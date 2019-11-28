using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float health;

    public float maxHealth;
    public float startHealth;
    public int healingRate;

    private bool isRegenerating;
    private bool isDead;

    private AudioSource bloodaudio1;
    private AudioSource bloodaudio2;

    private AudioClip deathsound;
    private AudioClip slowbeat;
    private AudioClip fastbeat;
    private AudioClip hurt1;
    private AudioClip hurt2;

    private Color RedPanel;
	
	private GameObject blood1;
	private GameObject blood2;

    private TextMeshProUGUI objective;
    private AudioClip objectiveSound;

    // Start is called before the first frame update
    void Start()
    {
        isRegenerating = true;
        isDead = false;

        bloodaudio1 = GameObject.Find("/Health System/Health HUD/Blood 1").GetComponent<AudioSource>();
        bloodaudio2 = GameObject.Find("/Health System/Health HUD/Blood 2").GetComponent<AudioSource>();

        deathsound = Resources.Load<AudioClip>("Sounds/396798__scorpion67890__male-death-1");
        slowbeat = Resources.Load<AudioClip>("Sounds/376464__user391915396__normal-heartbeats");
        fastbeat = Resources.Load<AudioClip>("Sounds/332808__loudernoises__heartbeat-100bpm");
        hurt1 = Resources.Load<AudioClip>("Sounds/437651__dersuperanton__damage-hit-voice-vocal");
        hurt2 = Resources.Load<AudioClip>("Sounds/437650__dersuperanton__getting-hit-damage-scream");

        RedPanel = GameObject.Find("/Health System/Health HUD/Red Panel").GetComponent<Image>().color;
        health = startHealth;
		
		blood1 = GameObject.Find("/Health System/Health HUD/Blood 1");
		blood2= GameObject.Find("/Health System/Health HUD/Blood 2");

        objective = GameObject.Find("/Health System/Health HUD/Objective").GetComponent<TextMeshProUGUI>();
        objectiveSound = Resources.Load<AudioClip>("Sounds/270468__littlerobotsoundfactory__jingle-achievement-00");
    }

    // Update is called once per frame
    void Update()
    {
        RestrictHealth();
        HealthGUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }
    void FixedUpdate()
    {
        RegenerateHealth();
    }

    private void RestrictHealth()
    {
        // Prevent health drops below zero and handle death
        if (health <= 0)
        {
            health = 0;
            Death();  //If health hits zero or lower, execute Death();
        }

        //Prevent health goes beyond its maximum number
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    private void HealthGUI()
    {
        if(health <= maxHealth * 0.65) // If health is equal to lower than 65%
        {
            blood1.GetComponent<Animator>().SetBool("isHalfHealth", true);
            if (!bloodaudio1.isPlaying && health > maxHealth * 0.3)
            {
                bloodaudio1.PlayOneShot(slowbeat);
            }else if(health <= maxHealth * 0.3)
            {
                bloodaudio1.Stop();
            }
        }
        else
        {
            blood1.GetComponent<Animator>().SetBool("isHalfHealth", false);
            bloodaudio1.Stop();
        }

        if (health <= maxHealth * 0.3) // If health is equal to lower than 30%
        {
            blood2.GetComponent<Animator>().SetBool("is30PercentHealth", true);
            if (!bloodaudio2.isPlaying)
            {
                bloodaudio2.PlayOneShot(fastbeat);
            }
        }
        else
        {
            blood2.GetComponent<Animator>().SetBool("is30PercentHealth", false);
            bloodaudio2.Stop();
        }
    }

    private void RegenerateHealth()
    {
        // Slowly regenerating health
        if(isRegenerating && health != maxHealth)
        {
            health += Time.deltaTime * healingRate;
        }
    }

    public void ChangeHealth(float addition)
    {
        if (!isDead)
        {
            if (addition > 0)
            {
                // Action if it is a health buff
            }

            if (addition < 0)
            {
                // Action if it is a damage
                if (Random.Range(0, 10) > 4 && health + addition > 0)
                {
                    GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(hurt1);
                }
                else
                {
                    GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(hurt2);
                }

                // Play the red screen animation
                GameObject.Find("Red Panel").GetComponent<Animation>().Play();

            }
            health += addition;
        }
    }

    public void Death()
    {
        if (!isDead)
        {
            isRegenerating = false;  // Disable health regeneration
            gameObject.GetComponent<FirstPersonController>().enabled = false; // Disable player movement

            // Destroy heartbeat sound effect and play the death growth
            bloodaudio1.enabled = false;
            bloodaudio2.enabled = false;

            GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(deathsound);

            GameObject.Find("/Health System/Health HUD/Red Panel").GetComponent<Animation>().Stop(); // Stop all blood spatter animations

            //Show the red screen and the sprite
            RedPanel.a = 0.7f;
            GameObject.Find("/Health System/Health HUD/Red Panel").GetComponent<Image>().color = RedPanel;
            GameObject.Find("/Health System/Health HUD/DeadSprite").GetComponent<Animation>().Play();

            isDead = true;
            Invoke("Restart", 5f);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleHealthRegeneration(bool toggle)
    {
        isRegenerating = toggle;
    }

    public bool IsAlive()
    {
        return !isDead;
    }

    public void SetObjective(string text)
    {
        objective.SetText("Objective: " + text);
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(objectiveSound,0.3f);
    }
}