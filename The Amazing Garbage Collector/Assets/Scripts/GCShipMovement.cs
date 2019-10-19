using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCShipMovement : MonoBehaviour
{

    public float speedH = 1.0f;
    public float speedV = 1.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private int acceleration = 1;

    private int currentSpeed = 0;
    private float currentShipSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();

        if ((Input.GetKey("up") || Input.GetKey("w")))
        {
            currentSpeed += acceleration;
        }
        else if ((Input.GetKey("down") || Input.GetKey("s")))
        {
            currentSpeed -= acceleration;
        }

        currentShipSpeed = Mathf.Lerp(currentShipSpeed, currentSpeed, Time.deltaTime * acceleration);

        transform.position += transform.TransformDirection(Vector3.down) * currentShipSpeed * Time.deltaTime;
    }

    void UpdateCameraPosition()
    {
        yaw -= speedH * Input.GetAxis("Mouse X");
        pitch += speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, 0.0f, yaw);
    }
}
