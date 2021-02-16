using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Variable Declaration
    private Vector2 position;
    private Vector2 dpos;
    int dx; // Step change on x-axis
    int dy; // Step change on y-axis

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position; // Initialize starting position of the player
        //dpos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //dx = 0;
        //dy = 0;

        //position = transform.position; // Initialize starting position of the player

        // Input to Movement Map
        // TODO: Convert to mappable keys for accessibility
        if ( Input.GetKey(KeyCode.W) ) 
        {
            //dy -= 4;
            position += 5f * Time.deltaTime * Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position += 5f * Time.deltaTime * Vector2.down;
            //dy += 4;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position += 5f * Time.deltaTime * Vector2.left;
            //dx -= 4;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position += 5f * Time.deltaTime * Vector2.right;
            //dx += 4;
        }

        //position = position*5f*Time.deltaTime*;

        transform.position = position;
    }
}
