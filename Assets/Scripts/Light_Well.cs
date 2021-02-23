using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Well : MonoBehaviour
{

    private Field_Of_View fov;
    private float fov_radius;
    private float fov_arc;

    private float transfer_speed; // Light Transfered Per Second
    private float delay; // Time In Seconds Before Light Recharges
    private float delay_countdown;
    private bool charging;
    private float charge_speed; // Light Returned Per Second After Delay
    private float current;
    private float capacity;

    // Start is called before the first frame update
    void Start()
    {
        fov = transform.GetComponentInChildren<Field_Of_View>();
        fov_radius = 3f;
        fov_arc = 360f;

        fov.SetRadius(fov_radius);
        fov.SetArc(fov_arc);

        transfer_speed = 5f;
        delay = 5f;
        delay_countdown = 0f;
        charging = true;
        charge_speed = 1f;
        capacity = 100f;
        current = capacity;        
    }

    // Update is called once per frame
    void Update()
    {
        if (charging && current < capacity)
        {
            current += charge_speed*Time.deltaTime;
            if(current > capacity)
            {
                current = capacity;
            }
        }
        else if(delay_countdown > 0f)
        {
            delay_countdown -= Time.deltaTime;
            if(delay_countdown <= 0f)
            {
                charging = true;
            }
        }

        fov.SetRadius(current / capacity * fov_radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {

            charging = false;
            collision.gameObject.GetComponent<Player_Manager>().SetLightCharging(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            if (current > 0f)
            {
                collision.gameObject.GetComponent<Player_Manager>().AddLight(transfer_speed * Time.deltaTime);
                current -= transfer_speed * Time.deltaTime;

                if(current < 0f)
                {
                    current = 0f;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        {

            delay_countdown = delay;
            collision.gameObject.GetComponent<Player_Manager>().SetLightCharging(false);
        }
    }
}
