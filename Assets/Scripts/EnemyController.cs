using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;

    //Checker
    private bool isTouchingPlayer;
    private bool isAttacked;
    private bool isSeeingThePlayer;

    private float originalSpeed;
    [SerializeField]
    private float speedBuff;

    [SerializeField]
    private float damageAmount;
    [SerializeField]
    private float damageDelayTime;
    [SerializeField]
    private float pursuitDelay;

    //Vairables for SphereCast
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private GameObject hitGameObject;

    private float sphereRadius;
    [SerializeField]
    private float sphereRadiusInPatrol;
    [SerializeField]
    private float sphereRaidusInPursuit;
    private LayerMask layerMask;
    private float currentHitDistance;
    [SerializeField]
    float outOfSightTimer;

    //Patrol point stuff
    public GameObject patrolPointObject;
    [SerializeField]
    private List<GameObject> patrolPoints = new List<GameObject>();

    //Audio stuff
    private AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip[] footstep;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        originalSpeed = agent.speed;
        sphereRadius = sphereRadiusInPatrol;
        isTouchingPlayer = false;
        isAttacked = false;
        isSeeingThePlayer = false;
        outOfSightTimer = pursuitDelay;
        audioSource = GetComponent<AudioSource>();
        animator.SetInteger("state", 1);
        layerMask = ~0;

        //Initialize patrol points
        LoadPatrolPoints();
    }

    public void LoadPatrolPoints()
    {
        if(patrolPoints.Count > 0)
        {
            patrolPoints.Clear();
        }

        foreach (Transform child in patrolPointObject.transform)
        {
            patrolPoints.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScanPlayer();
        AIMovement();
        if (player != null)
        {
            if (player.GetComponent<HealthSystem>().IsAlive())
            {
                DamagePlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
            isSeeingThePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    private void ScanPlayer()
    {
        float tempDistance;

        if(player != null) // For monster in Start Menu to work
        {
            if (player.GetComponent<FlashlightSystem>().isOn)
            {
                tempDistance = maxDistance;
            }
            else
            {
                tempDistance = maxDistance * 0.3f;
            }
        }
        else
        {
            tempDistance = maxDistance; //So the monster would still move in the Start Menu scene
        }

        if (Physics.SphereCast(transform.GetChild(1).position, sphereRadius, transform.GetChild(1).forward, out RaycastHit hit, tempDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            hitGameObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            if (hitGameObject == player)
            {
                isSeeingThePlayer = true;
            }
            else if (!isTouchingPlayer)
            {
                isSeeingThePlayer = false;
            }

        }
        else //If the ray touches nothing
        {
            currentHitDistance = tempDistance;
            hitGameObject = null;
            if (!isTouchingPlayer)
            {
                isSeeingThePlayer = false;
            }
        }
    }

    private void AIMovement()
    {
        if (isSeeingThePlayer)
        {
            // Reset the pursuit delay
            outOfSightTimer = 0;
            Pursuit();
        }
        else
        {
            // Keep chasing the player for a short amount of time even out of sight
            outOfSightTimer += Time.fixedDeltaTime;
            if (outOfSightTimer > pursuitDelay)
            {
                Patrol();
            }
            else
            {
                Pursuit();
            }
        }
    }

    private void Pursuit()
    {
        // Keep looking torwards the player
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 100f * Time.deltaTime);

        // To prevent any stuck
        if (isTouchingPlayer)
        {
            agent.isStopped = true;
        }
        else if (player.GetComponent<HealthSystem>().IsAlive()) // Keep moving if not touching the player and the player is alive
        {
            agent.isStopped = false;
            agent.speed = originalSpeed * speedBuff;
            sphereRadius = sphereRaidusInPursuit;
            agent.SetDestination(player.transform.position);
            animator.SetInteger("state", 2);
            if (!IsInvoking("PlayFootStep"))
            {
                Invoke("PlayFootStep", 0.4f);
            }
        }
    }


    private void Patrol()
    {
        agent.speed = originalSpeed;
        sphereRadius = sphereRadiusInPatrol;
        animator.SetInteger("state", 1);
        if (!IsInvoking("PlayFootStep"))
        {
            Invoke("PlayFootStep", 0.8f);
        }
        if (agent.remainingDistance <= 0 || !IsMoving())
        {
            NextPatrolPoint();
        }

    }

    public void NextPatrolPoint()
    {
        agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Count - 1)].transform.position);
    }

    private void DamagePlayer()
    {
        if (isTouchingPlayer && !isAttacked)
        {
            player.GetComponent<HealthSystem>().ChangeHealth(-damageAmount);
            animator.SetInteger("state", 3);
            audioSource.PlayOneShot(attackSound);
            isAttacked = true;
            Invoke("DamageDelay", damageDelayTime); // To prevent damaging player every frame
        }
    }

    private void DamageDelay()
    {
        isAttacked = false;
    }

    private void PlayFootStep()
    {
        int n = Random.Range(0, footstep.Length - 1);
        audioSource.PlayOneShot(footstep[n]);
    }

    public bool IsMoving()
    {
        if (agent.velocity.x != 0 && agent.velocity.z != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.GetChild(1).position, transform.GetChild(1).position + transform.GetChild(1).forward * currentHitDistance);
        Gizmos.DrawWireSphere(transform.GetChild(1).position + transform.GetChild(1).forward * currentHitDistance, sphereRadius);
    }

}