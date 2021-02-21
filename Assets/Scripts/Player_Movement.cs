using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Variable Declaration
    private Vector3 position;
    private Vector3 direction_vector;
    private Vector3 last_move;
    [SerializeField] private LayerMask ray_mask;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position; // Initialize starting position of the player
        last_move = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        direction_vector = Vector2.zero;
        position = transform.position; // Initialize starting position of the player

        // Input to Movement Map
        // TODO: Convert to mappable keys for accessibility
        if ( Input.GetKey(KeyCode.W) ) 
        {
            direction_vector += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction_vector += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction_vector += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction_vector += Vector3.right;
        }

        RaycastHit2D raycast_hit = Physics2D.Raycast(transform.position,  direction_vector, 5f*Time.deltaTime, ray_mask);

        if (raycast_hit.collider == null)
        {
            transform.position += direction_vector * 5f * Time.deltaTime;
            last_move = direction_vector * 5f * Time.deltaTime;
        }
        else if(transform.position.Equals(raycast_hit.point))
        {
            transform.position -= last_move;
            transform.position += direction_vector * 5f * Time.deltaTime;
            last_move = direction_vector * 5f * Time.deltaTime;
        }



        //body.MovePosition(position);
    }
}
