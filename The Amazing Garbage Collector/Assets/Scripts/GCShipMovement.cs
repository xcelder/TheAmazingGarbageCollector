using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCShipMovement : MonoBehaviour
{


    public float speedH = 1.0f;
    public float speedV = 1.0f;

    [SerializeField]
    private int acceleration = 1;

    [SerializeField]
    private int lateralAcceleration = 3;

    [SerializeField]
    private int verticalAcceleration = 3;

    public int currentForwardSpeed = 0;
    private float currenForwardtShipSpeed = 0f;
    private int currentLateralSpeed = 0;
    private float currentLateraltShipSpeed = 0f;
    private int currentVerticalSpeed = 0;
    private float currentVerticalShipSpeed = 0f;

    private Rigidbody shipBody;

    // Start is called before the first frame update
    void Start()
    {
        shipBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        ShipMovement();
    }

    void ShipMovement()
    {
        // Forward
        if ((Input.GetKey("up") || Input.GetKey("w")))
        {
            currentForwardSpeed += acceleration;
        }
        else if ((Input.GetKey("down") || Input.GetKey("s")))
        {
            currentForwardSpeed -= acceleration;
        }

        // Lateral

        if ((Input.GetKey("right") || Input.GetKey("d")))
        {
            currentLateralSpeed += lateralAcceleration;
        }
        else if ((Input.GetKey("left") || Input.GetKey("a")))
        {
            currentLateralSpeed -= lateralAcceleration;
        }
        else if (currentLateralSpeed != 0)
        {
            if (currentLateralSpeed > 0)
            {
                currentLateralSpeed -= lateralAcceleration;
                currentLateralSpeed = Mathf.Max(currentLateralSpeed, 0);
            }
            else
            {
                currentLateralSpeed += lateralAcceleration;
                currentLateralSpeed = Mathf.Min(currentLateralSpeed, 0);
            }
        }

        // Vertical

        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentVerticalSpeed -= verticalAcceleration;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            currentVerticalSpeed += verticalAcceleration;
        }
        else if (currentVerticalSpeed != 0)
        {
            if (currentVerticalSpeed > 0)
            {
                currentVerticalSpeed -= verticalAcceleration;
                currentVerticalSpeed = Mathf.Max(currentVerticalSpeed, 0);
            }
            else
            {
                currentVerticalSpeed += verticalAcceleration;
                currentVerticalSpeed = Mathf.Min(currentVerticalSpeed, 0);
            }
        }

        currenForwardtShipSpeed = Mathf.Lerp(currenForwardtShipSpeed, currentForwardSpeed, Time.deltaTime * acceleration);
        currentLateraltShipSpeed = Mathf.Lerp(currentLateraltShipSpeed, currentLateralSpeed, Time.deltaTime * acceleration);
        currentVerticalShipSpeed = Mathf.Lerp(currentVerticalShipSpeed, currentVerticalSpeed, Time.deltaTime * acceleration);

        transform.position += transform.TransformDirection(Vector3.forward) * currenForwardtShipSpeed * Time.deltaTime;
        transform.position += transform.TransformDirection(Vector3.right) * currentLateraltShipSpeed * Time.deltaTime;
        transform.position += transform.TransformDirection(Vector3.up) * currentVerticalShipSpeed * Time.deltaTime;
    }

    void CameraMovement()
    {
        HideCursor();

        float horizontalMovement = speedH * Input.GetAxis("Mouse X");
        float verticalMovement = -speedV * Input.GetAxis("Mouse Y");

        transform.RotateAround(transform.position, transform.up, horizontalMovement);
        transform.RotateAround(transform.position, transform.right, verticalMovement);

    }

    void HideCursor()
    {
        Cursor.visible = false;
    }
}
