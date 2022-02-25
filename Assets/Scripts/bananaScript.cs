using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaScript : MonoBehaviour
{
    public Transform player;
    public float Speed = 1f;
    public float minGoalDistance = 0.01f;
    public float chaseDistance = 5;
    public Transform[] waypoints;
    public int waypointIndex;
    public bool isChasing = false;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            isChasing = true;
            AIMovementTowards(player);
        }
        else
        {
            if (isChasing == true)
            {
                isChasing = false;
                for (int i = 0; i < 3; i++)
                {
                    if (Vector2.Distance(transform.position, waypoints[i].position) < Vector2.Distance(transform.position, waypoints[waypointIndex].position))
                    {
                        waypointIndex = i;
                    }
                }
            }
            else
            {
                WaypointUpdate();
            }
            
            AIMovementTowards(waypoints[waypointIndex]);
        }
        //Vector3.MoveTowards(transform.position, position0.transform.position, Time.deltaTime * Speed);
    }
    private void WaypointUpdate()
    {
        Vector2 AIPosition = transform.position;
        if (Vector2.Distance(AIPosition, waypoints[waypointIndex].position) < minGoalDistance)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
    private void AIMovementTowards(Transform goal)
    {
        if (Vector2.Distance(transform.position, goal.position) > minGoalDistance)
        {
            Vector2 directionToPos0 = goal.position-transform.position;
                    directionToPos0.Normalize();
                    transform.position += (Vector3)directionToPos0*Time.deltaTime*Speed;
        }
    }
}
