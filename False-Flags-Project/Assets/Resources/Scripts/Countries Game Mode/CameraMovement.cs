using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [HideInInspector]
    public bool MovingCamera;

    public float Speed = 20.0f;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 Position = transform.position;
        
            //MovingCamera = true;
        if (Input.GetKey("w"))
        {
            Position.y += Speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            Position.y -= Speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            Position.x += Speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            Position.x -= Speed * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            gameObject.GetComponent<Camera>().fieldOfView -= Speed * Time.deltaTime;
        }
        if(Input.GetKey("q"))
        {
            gameObject.GetComponent<Camera>().fieldOfView += Speed * Time.deltaTime;
        }
        transform.position = Position;
    }
}
