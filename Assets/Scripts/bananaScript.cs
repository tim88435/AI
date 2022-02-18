using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaScript : MonoBehaviour
{
    public Transform player;
    public float Speed = 1.0f;
    public GameObject position0;
    public float minGoalDIstance = 0.01f;
    public float chaseDistance = 5f;
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position)<chaseDistance)
            {
            AIMovementTowards(player);
            }
        //Vector3.MoveTowards(transform.position, position0.transform.position, Time.deltaTime * Speed);
    }
    private void AIMovementTowards(Transform goal)
    {
        if (Vector2.Distance(transform.position, goal.position) > minGoalDIstance)
        {
            Vector2 directionToPos0 = goal.position-transform.position;
                    directionToPos0.Normalize();
                    transform.position += (Vector3)directionToPos0*Time.deltaTime*Speed;
        }
    }
}
