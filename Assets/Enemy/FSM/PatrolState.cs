using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    bool isMoving;
    Vector3 destinationPoint;

    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Patrol");

        isMoving = false;

        enemy.animator.SetTrigger("Patrol");
    }

    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Patrolling");

        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.chaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        if (!isMoving)
        {
            isMoving = true;
            int index = UnityEngine.Random.Range(0, enemy.waypoints.Count);
            destinationPoint = enemy.waypoints[index].position;
            enemy.navMeshAgent.destination = destinationPoint;
        }
        else
        {
            if((Vector3.Distance(destinationPoint, enemy.transform.position)) <= 0.3f)
            {
                isMoving = false;
                //Debug.Log("ARRIVED");
            }

            //Debug.Log($"DISTANCE: {Vector3.Distance(destinationPoint, enemy.transform.position)}");
        }

    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}