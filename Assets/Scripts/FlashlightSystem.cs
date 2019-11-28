using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class FlashlightSystem : MonoBehaviour
{
    private GameObject spotlight;

    [SerializeField]
    private float energy;
    public float maxEnergy;
    public float powerUseRate;
    public int regeneratingRate;
    public int tempBlockTime;
    [HideInInspector]
    public bool isOn;
    private bool isRegenerating;
    private bool tempDisable;

    private AudioClip toggleSound;
    private AudioClip reject;
    private AudioClip nopower;
    private AudioClip recharged;
    private AudioSource audioSource;

    private GameObject NormalPanel;
    private GameObject RedPanel;
    private Image[] batteries = new Image[3];

    // Start is called before the first frame update
    void Start()
    {
        spotlight = transform.GetChild(0).GetChild(0).gameObject;
        toggleSound = Resources.Load<AudioClip>("Sounds/435845__dersuperanton__flashlight-click");
        reject = Resources.Load<AudioClip>("Sounds/415764__thebuilder15__wrong");
        nopower = Resources.Load<AudioClip>("Sounds/55823__sergenious__dischrge");
        recharged = Resources.Load<AudioClip>("Sounds/2014__edwin-p-manchester__flash");
        audioSource = GetComponent<AudioSource>();

        batteries[0] = GameObject.Find("/Flashlight System/Flashlight Canvas/Flashlight Panel/First Battery").GetComponent<Image>();
        batteries[1] = GameObject.Find("/Flashlight System/Flashlight Canvas/Flashlight Panel/Second Battery").GetComponent<Image>();
        batteries[2] = GameObject.Find("/Flashlight System/Flashlight Canvas/Flashlight Panel/Third Battery").GetComponent<Image>();

        NormalPanel = GameObject.Find("/Flashlight System/Flashlight Canvas/Flashlight Panel");
        RedPanel = GameObject.Find("/Flashlight System/Flashlight Canvas/Flashlight Red Panel");

        energy = maxEnergy;
        isOn = false;
        isRegenerating = true;
        tempDisable = false;
        RedPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ToggleFlashlight();
        RestrictEnergy();
        FlashlightGUI();
    }

    void FixedUpdate()
    {
        RegenerateEnergy();
    }

    public void ToggleFlashlight()
    {
        if (isOn && !spotlight.activeSelf)
        {
            spotlight.SetActive(true);
        }
        else if(!isOn && spotlight.activeSelf)
        {
            spotlight.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && GetComponent<HealthSystem>().IsAlive())
        {
            if (isOn)
            {
                audioSource.PlayOneShot(toggleSound);
                isOn = false;
            }
            else
            {
                if (energy > 0 && !tempDisable)
                {
                    audioSource.PlayOneShot(toggleSound);
                    isOn = true;
                }
                else
                {
                    audioSource.PlayOneShot(reject);
                    Debug.Log("No energy left in flashlight");
                }
            }
        }
    }

    private void RegenerateEnergy()
    {
        if (isOn)
        {
            energy -= Time.deltaTime * powerUseRate;
        }
        if (!isOn && isRegenerating && energy != maxEnergy)
        {
            energy += Time.deltaTime * regeneratingRate;
        }
    }

    private void RestrictEnergy()
    {
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        if (energy <=0)
        {
            isOn = false;
            audioSource.PlayOneShot(nopower);
            Debug.Log("Our of battery");
            energy = 0;
            tempDisable = true;
            Invoke("DisableFlashlightLock", tempBlockTime);
        }
    }

	public void DisableFlashlightLock()
    {
        tempDisable = false;
        audioSource.PlayOneShot(recharged);
    }
	
    private void FlashlightGUI()
    {
        if(energy <= maxEnergy / 3 * 2)
        {
            batteries[2].enabled = false;
        }
        else
        {
            batteries[2].enabled = true;
        }

        if (energy <= maxEnergy / 3)
        {
            batteries[1].enabled = false;
        }
        else
        {
            batteries[1].enabled = true;
        }

        if (energy <= 0)
        {
            batteries[0].enabled = false;
        }
        else
        {
            batteries[0].enabled = true;
        }

        if (tempDisable)
        {
            NormalPanel.SetActive(false);
            RedPanel.SetActive(true);
        }
        else
        {
            NormalPanel.SetActive(true);
            RedPanel.SetActive(false);
        }
    }
}
