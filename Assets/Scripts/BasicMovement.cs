using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]

public class BasicMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3 currentDirection;

    private Rigidbody body;

    public float cameraRotation;
    
    private float xDirection, zDirection;

    // Start is called before the first frame update
    void Start()
    {
        StartCheck();

        body = GetComponent<Rigidbody>();
    }

    void StartCheck()
    {
        speed = (speed == 0) ?
            speed = 10 : speed;
    }

    // Update is called once per frame
    void Update()
    {
        Data();

        Rotate();

        Move();
    }

    void Data() // gets the directions and makes them always positve
    {
        Vector3 previousDirection = currentDirection;

        zDirection = Mathf.Abs(Input.GetAxis("Vertical"));

        xDirection = Mathf.Abs(Input.GetAxis("Horizontal"));


        currentDirection = new Vector3(xDirection, 0, zDirection);

        currentDirection += (currentDirection.magnitude > 0) ?
            previousDirection : Vector3.zero;

        if (currentDirection.sqrMagnitude > 1)
            currentDirection.Normalize();

        else
            currentDirection *= 1.4f;
    }

    void Rotate()
    {
        Vector3 lookDir = Camera.main.transform.rotation * (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        body.transform.LookAt(new Vector3(lookDir.x, lookDir.y, lookDir.z) + body.transform.position);
    }


    void Move() //Moves the character relative to where the camera is looking
    {
        Vector3 velocityForward = (transform.forward * currentDirection.z) * speed;
        Vector3 velocityRight = (transform.forward * currentDirection.x) * speed;
        Vector3 velocityUp = (transform.up * Input.GetAxis("Jump")) * (speed/4);
        

        Vector3 VelVec = velocityForward + velocityRight + velocityUp;


        body.velocity = VelVec;
    }
}