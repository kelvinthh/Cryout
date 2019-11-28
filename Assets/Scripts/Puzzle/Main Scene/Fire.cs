using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    GameObject player;
    bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isHurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isHurt)
        {
            player.GetComponent<HealthSystem>().ChangeHealth(-45);
            isHurt = true;
            Invoke("DamageDelay", 0.4f);
        }
    }

    private void DamageDelay()
    {
        isHurt = false;
    }
}
