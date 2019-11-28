using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerEnterHandler
{
    private AudioClip click;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        click = Resources.Load<AudioClip>("Sounds/271140__strange-dragoon__menu-navigation");
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(click);
        Debug.Log("Cyka");
    }
}
