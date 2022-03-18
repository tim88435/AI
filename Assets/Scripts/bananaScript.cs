using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaScript : MonoBehaviour
{


    [Header("Enemy Attributes")]
    [Tooltip("Speed of enemy")]
    public float Speed = 1f;
    [Tooltip("Minimum distance to consume a banana")]
    public float minGoalDistance = 0.01f;
    [Tooltip("Minimum distance to engage the player")]
    public float chaseDistance = 5;
    [Tooltip("Minimum distance to flee from the player")]
    public float runDistance = 5;
    [Header("Current Banana Waypoint")]
    [Tooltip("Current Banana Waypoint based on the \"waypoints\" element list below")]
    public int waypointIndex;
    [Header("Banana Spawn Settings")]
    [Tooltip("Seconds since the last time a banana has spawned")]
    public float timeOfLastSpawn;
    [Tooltip("Time it takes to spawn a banana while the enemy is chasing the player")]
    public int attackingBananaSpawnTime;
    [Tooltip("Time it takes to spawn a banana while the enemy is defending")]
    public int defendingBananaSpawnTime;
    [Tooltip("Time it takes to spawn a banana while the enemy is fleeing the player")]
    public int fleeingBananaSpawnTime;
    [Tooltip("Time it takes to spawn a banana while the enemy is picking bananas")]
    public int pickingBananaSpawnTime;
    [Tooltip("Amount of bananas for the enemy to start picking bananas again")]
    public int requiredBanana;
    [Tooltip("Amount of bananas that spawn at the start of the game")]
    public int startingBanana;
    [Header("Elements")]
    [Tooltip("Player Object")]
    public Transform player;
    [Tooltip("Prefabricated object to use as the bananas")]
    public GameObject wayPointPrefab;
    [Tooltip("List of current waypoints")]
    public List<GameObject> waypoints;


    // Update is called once per frame

    //void Update()
    //{
    //    if (Vector2.Distance(transform.position, player.position) < chaseDistance)
    //    {
    //        isChasing = true;
    //        AIMovementTowards(player);
    //    }
    //    else
    //    {
    //        if (isChasing == true)
    //        {
    //            isChasing = false;
    //            for (int i = 0; i < waypoints.Length; i++)
    //            {
    //                if (Vector2.Distance(transform.position, waypoints[i].position) < Vector2.Distance(transform.position, waypoints[waypointIndex].position))
    //                {
    //                    waypointIndex = i;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            WaypointUpdate();
    //        }

    //        AIMovementTowards(waypoints[waypointIndex]);
    //    }
    //    //Vector3.MoveTowards(transform.position, position0.transform.position, Time.deltaTime * Speed);
    //}

    private void Start()
    {
        while (waypoints.Count < startingBanana)
        {
            NewWaypoint();
        }
    }


    public void RemoveCurrentWaypoint()
    {
        GameObject current = waypoints[waypointIndex];
        waypoints.Remove(current);
        Destroy(current);
        if (waypointIndex == waypoints.Count & waypointIndex !=0)
        {
            waypointIndex = 0;
        }
    }



    public void NewWaypoint()
    {
        float x = Random.Range(-9, 9);
        float y = Random.Range(-5, 5);
        GameObject newPoint = Instantiate(wayPointPrefab, new Vector2(x,y), Quaternion.identity);
        waypoints.Add(newPoint);
        WaypointUpdate();
    }




    public void WaypointUpdate()
    {
        Vector2 AIPosition = transform.position;
        if (Vector2.Distance(AIPosition, waypoints[waypointIndex].transform.position) < minGoalDistance)
        {
            RemoveCurrentWaypoint();
            if (waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }

        }
    }
    public void AIMovementTowards(Transform goal)
    {
        if (Vector2.Distance(transform.position, goal.position) > minGoalDistance)
        {
            Vector2 directionToPos0 = goal.position-transform.position;
                    directionToPos0.Normalize();
                    transform.position += (Vector3)directionToPos0*Time.deltaTime*Speed;
        }
    }

    public void AIMovementAway(Transform goal)
    {
    Vector2 directionToPos0 = goal.position - transform.position;
    directionToPos0.Normalize();
    transform.position -= (Vector3)directionToPos0 * Time.deltaTime * Speed;
    }

}
