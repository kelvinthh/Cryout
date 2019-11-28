using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointControl : MonoBehaviour
{
    GameObject player;
    EnemyController enemyController;
    [SerializeField]
    GameObject[] patrolPoints;
    GameObject previousLoadedPoint;
    bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= 30f)
        {
            SwitchPoints(1);
        }
        else
        {
            SwitchPoints(0);
        }
    }

    void SwitchPoints(int number)
    {
        if (previousLoadedPoint != patrolPoints[number])
        {
            enemyController.patrolPointObject = patrolPoints[number];
            enemyController.LoadPatrolPoints();
            enemyController.NextPatrolPoint();
            previousLoadedPoint = patrolPoints[number];
            Debug.Log("Changed to the point set '" + number + "'");
        }
    }
}
