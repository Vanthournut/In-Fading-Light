using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Of_View : MonoBehaviour
{

    // Variable Declaration

    Mesh mesh;
    [SerializeField] private LayerMask ray_mask;

    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;

    Vector3 root;

    float fov;
    float direction;
    float radius;
    int rays;
    float delta_angle;
    float bleed;
    private float cur_direction;

    // Start is called before the first frame update
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        direction = 0f;
        radius = 3f;
        fov = 90f;
        rays = 200;
        delta_angle = fov / rays;
        bleed = 0.2f;

    }

    // Update is called once per frame
    private void LateUpdate()
    {

        root = transform.position;

        cur_direction = direction;


        vertices = new Vector3[rays + 2];
        uv = new Vector2[rays + 2];
        triangles = new int[3 * rays];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < rays + 1; i++)
        {
            float rad_angle = cur_direction * Mathf.PI / 180;
            Vector3 direction_vector = new Vector3(Mathf.Cos(rad_angle), Mathf.Sin(rad_angle));

            RaycastHit2D raycast_hit = Physics2D.Raycast(root, direction_vector, radius, ray_mask);

            if (raycast_hit.collider == null)
            {
                vertices[i + 1] = direction_vector * radius;
            }
            else
            {
                Vector2 bleed_vector = direction_vector * bleed;
                if ((raycast_hit.point + bleed_vector - (Vector2) root).magnitude > radius)
                {
                    vertices[i + 1] = direction_vector * radius;
                }
                else 
                {
                    vertices[i + 1] = raycast_hit.point + bleed_vector - (Vector2) root;
                }
            }

            cur_direction -= delta_angle;

            if (i > 0)
            {
                triangles[3 * (i - 1)] = 0;
                triangles[3 * (i - 1) + 1] = i;
                triangles[3 * (i - 1) + 2] = i + 1;
            }
        }



        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetRadius(float new_radius)
    {
        this.radius = new_radius;
    }

    public void SetDirection(float new_direction)
    {
        this.direction = new_direction-fov/2f;
    }
}
