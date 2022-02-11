using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaScript : MonoBehaviour
{
    public GameObject position0;
    public GameObject position1;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < position0.transform.position.x)
        {

            Vector2 AIPosition = transform.position;
            AIPosition.x +=Time.deltaTime;
            transform.position = AIPosition;

        }
    }
}
