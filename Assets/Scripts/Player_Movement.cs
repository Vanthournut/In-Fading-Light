using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Variable Declaration
    private Vector3 position;
    private Vector3 direction_vector;
    private Vector3 last_move;

    private bool stunned;
    private float stun_duration;
    private Vector3 knockback_direction;
    private float knockback_speed;
    private float knockback_distance;

    [SerializeField] private LayerMask ray_mask;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position; // Initialize starting position of the player
        last_move = Vector3.zero;

        stunned = false;
        stun_duration = 0f;
        knockback_direction = Vector3.zero;
        knockback_speed = 0f;
        knockback_distance = 0f;
}

    // Update is called once per frame
    void Update()
    {
        direction_vector = Vector2.zero;
        position = transform.position; // Initialize starting position of the player

        if (stunned) // When Stunned prevent player input
        {
            stun_duration -= Time.deltaTime;
            if (stun_duration <= 0f)
            {
                stunned = false;
            }
        }
        else
        {
            // Input to Movement Map
            // TODO: Convert to mappable keys for accessibility
            if (Input.GetKey(KeyCode.W))
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
        }

        direction_vector = direction_vector * 5f * Time.deltaTime;

        knockback_distance -= knockback_speed * Time.deltaTime;

        if(knockback_distance > 0f)
        {
            direction_vector += knockback_direction * knockback_speed * Time.deltaTime;
            knockback_distance -= knockback_speed * Time.deltaTime;
        }

        RaycastHit2D raycast_hit = Physics2D.Raycast(transform.position,  direction_vector, direction_vector.magnitude, ray_mask);

        if (raycast_hit.collider == null)
        {
            transform.position += direction_vector;
            last_move = direction_vector;
        }
        else if(transform.position.Equals(raycast_hit.point))
        {
            transform.position -= last_move;
            transform.position += direction_vector;
            last_move = direction_vector;
        }

    }

    public void SetKnockback(Vector3 direction, float speed, float distance)
    {
        if (direction.Equals(Vector2.zero))
        {
            direction = last_move*-1;
        }
        knockback_direction = direction.normalized;
        knockback_distance = distance;
        knockback_speed = speed;
    }

    public void SetStun(float duration)
    {
        stunned = true;
        stun_duration = duration;
    }
}
