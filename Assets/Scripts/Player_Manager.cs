using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    // Variable Declaration
    private Camera cam;

    private Field_Of_View fov;
    private float fov_radius;
    private float fov_arc;

    private float light_decay; // Light Lost Per Second
    private bool light_charging;
    private float current_light;
    private float light_capacity;

    private int max_health;
    private int current_health;

    private float direction;
    private Vector3 dir_vector;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        fov = GetComponentInChildren<Field_Of_View>();
        fov_radius = 3f;
        fov_arc = 360f;

        light_capacity = 100f;
        light_charging = false;
        current_light = light_capacity;
        light_decay = 1f;

        max_health = 100;
        current_health = max_health;

        direction = 0f;
        dir_vector = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        dir_vector = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);

        direction = Mathf.Atan2(dir_vector.x, -dir_vector.y) * Mathf.Rad2Deg + 180f;

        if(direction < 0)
        {
            direction += 360f;
        }

        fov.SetDirection(direction);
        fov.SetRadius(current_light / light_capacity * fov_radius);

        current_light = current_light - light_decay * Time.deltaTime;
        if (current_light < 0 && !light_charging)
        {
            current_light = 0;
        }
    }

    public void SetLightCharging(bool charging)
    {
        this.light_charging = charging;
    }

    public void AddLight(float amount)
    {
        current_light += amount;
        if(current_light > light_capacity)
        {
            current_light = light_capacity;
        }
        else if (current_light < 0f)
        {
            current_light = 0f;
        }
    }

    public void ModifyHealth(int amount)
    {
        current_health += amount;
        if(current_health <= 0)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
