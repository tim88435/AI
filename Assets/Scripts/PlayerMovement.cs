using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private void movement() 
        {
            Vector2 moveDirection = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection.y ++;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection.x--;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection.y--;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection.x++;
            }
            moveDirection.Normalize();
            transform.position += (Vector3)moveDirection *speed*Time.deltaTime;
        }    
    
    void Update()
    {
        movement();
    }
    
}
