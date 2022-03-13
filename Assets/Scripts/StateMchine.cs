using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMchine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Defence,
        RunAway,
        BerryPicking
    }
    public State currentState;

    public bananaScript aiMovement;

    private void Start()
    {
        aiMovement = GetComponent<bananaScript>();
        NextState();
    }


    private void NextState()
    {
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
        }
    }

    private IEnumerator AttackState()
    {
        Debug.Log("Attack Enter");

        while (currentState == State.Attack)
        {
            if (aiMovement.timeOfLastSpawn + aiMovement.attackingBananaSpawnTime < Time.time)
            {
                Debug.Log("New waypoint at" + aiMovement.timeOfLastSpawn);
                aiMovement.NewWaypoint();
                aiMovement.timeOfLastSpawn = Time.time;
            }
                if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance)
            {
                aiMovement.AIMovementTowards(aiMovement.player);
                Debug.Log("Currently Attacking");
                yield return null;
            }
            else
            {
                currentState = State.BerryPicking;
            }
        }
        Debug.Log("Attack Exit");
        NextState();
    }
    private IEnumerator DefenceState()
    {
        Debug.Log("Defence Enter");
        aiMovement.timeOfLastSpawn = Time.time;
        while (currentState == State.Defence)
        {
            Debug.Log("Currently Defending");
            if (aiMovement.timeOfLastSpawn + aiMovement.defendingBananaSpawnTime < Time.time)
            {
                Debug.Log("New waypoint at"+aiMovement.timeOfLastSpawn);
                aiMovement.NewWaypoint();
                aiMovement.timeOfLastSpawn = Time.time;
            }
            if (aiMovement.waypoints.Count >= aiMovement.requiredBanana)
            {
                currentState = State.Attack;
            }
            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.runDistance)
            {
                currentState = State.RunAway;
            }
            yield return null;
        }
        Debug.Log("Defence Exit");
        NextState();
    }
    private IEnumerator RunAwayState()
    {
        Debug.Log("RunAway Enter");
        aiMovement.timeOfLastSpawn = Time.time;
        while (currentState == State.RunAway)
        {
            Debug.Log("Currently Running Away");

            if (aiMovement.timeOfLastSpawn + aiMovement.fleeingBananaSpawnTime < Time.time)
            {
                aiMovement.NewWaypoint();
                aiMovement.timeOfLastSpawn = Time.time;
            }
            if (aiMovement.waypoints.Count >= aiMovement.requiredBanana)
            {
                currentState = State.Attack;
            }
            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.runDistance)
            {
                aiMovement.AIMovementAway(aiMovement.player);
            }
            else
            {
                currentState = State.Defence;
            }
            yield return null;

        }
        Debug.Log("RunAway Exit");
        NextState();
    }
    private IEnumerator BerryPickingState()
    {
        Debug.Log("BerryPicking Enter");
        for (int i = 0; i < aiMovement.waypoints.Count; i++)
                {
                    if (Vector2.Distance(transform.position, aiMovement.waypoints[i].transform.position) < Vector2.Distance(transform.position, aiMovement.waypoints[aiMovement.waypointIndex].transform.position))
                    {
                        aiMovement.waypointIndex = i;
                    }
                }
        aiMovement.WaypointUpdate();
        aiMovement.AIMovementTowards(aiMovement.waypoints[aiMovement.waypointIndex].transform);
        while (currentState == State.BerryPicking)
        {
            Debug.Log("Currently picking Bananas");
            if (aiMovement.timeOfLastSpawn + aiMovement.pickingBananaSpawnTime < Time.time)
            {
                Debug.Log("New waypoint at" + aiMovement.timeOfLastSpawn);
                aiMovement.NewWaypoint();
                aiMovement.timeOfLastSpawn = Time.time;
            }
            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance)
            {
                currentState = State.Attack;
            }
            else
            {
                aiMovement.WaypointUpdate();
                if (aiMovement.waypoints.Count == 0)
                {
                    currentState = State.Defence;
                    break;
                }
                aiMovement.AIMovementTowards(aiMovement.waypoints[aiMovement.waypointIndex].transform);
            }
            yield return null;
        }
        Debug.Log("BerryPicking Exit");
        NextState();
    }






}
