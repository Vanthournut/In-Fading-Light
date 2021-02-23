using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    private Vector3 player_entry_position;
    private BoxCollider2D pit_collider;

    // Start is called before the first frame update
    void Start()
    {
        pit_collider = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player_entry_position = collision.transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            if(pit_collider.bounds.Contains(collision.collider.bounds.min) && pit_collider.bounds.Contains(collision.collider.bounds.max))
            {
                collision.gameObject.GetComponent<Player_Manager>().ModifyHealth(-20);
                collision.gameObject.GetComponent<Player_Movement>().SetKnockback(Vector2.zero, 6f, 1f);
                collision.gameObject.GetComponent<Player_Movement>().SetStun(0.25f);
                collision.gameObject.transform.position = player_entry_position;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        /*if (collision.gameObject.name == "Player")
        {
        }*/
    }
}
