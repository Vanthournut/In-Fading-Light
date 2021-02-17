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
    private float current_light;
    private float light_capacity;

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
        current_light = light_capacity;
        light_decay = 1f;

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
        if (current_light < 0)
        {
            current_light = 0;
        }
    }
}