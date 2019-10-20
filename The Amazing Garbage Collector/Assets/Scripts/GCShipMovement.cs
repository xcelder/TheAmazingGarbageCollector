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

    [SerializeField]
    private int lateralDecceleration = 6;

    [SerializeField]
    private int verticalDecceleration = 6;

    [SerializeField]
    private ParticleSystem leftGas;

    [SerializeField]
    private ParticleSystem topGas;

    [SerializeField]
    private ParticleSystem rightGas;

    [SerializeField]
    private ParticleSystem bottomGas;

    public int currentForwardSpeed = 0;
    private float currenForwardShipSpeed = 0f;
    private int currentLateralSpeed = 0;
    [SerializeField]
    private float currentLateralShipSpeed = 0f;
    private int currentVerticalSpeed = 0;
    [SerializeField]
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
        ActivateThrottles();

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
                currentLateralSpeed -= lateralDecceleration;
                currentLateralSpeed = Mathf.Max(currentLateralSpeed, 0);
            }
            else
            {
                currentLateralSpeed += lateralDecceleration;
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
                currentVerticalSpeed -= verticalDecceleration;
                currentVerticalSpeed = Mathf.Max(currentVerticalSpeed, 0);
            }
            else
            {
                currentVerticalSpeed += verticalDecceleration;
                currentVerticalSpeed = Mathf.Min(currentVerticalSpeed, 0);
            }
        }


        currenForwardShipSpeed = Mathf.Lerp(currenForwardShipSpeed, currentForwardSpeed, Time.deltaTime * acceleration);


            currentLateralShipSpeed = Mathf.Lerp(currentLateralShipSpeed, currentLateralSpeed, Time.deltaTime * acceleration);
        if (Mathf.Abs(currentLateralShipSpeed) < 1)
        {
            currentLateralShipSpeed = 0;
        }

        currentVerticalShipSpeed = Mathf.Lerp(currentVerticalShipSpeed, currentVerticalSpeed, Time.deltaTime * acceleration);

        if (Mathf.Abs(currentVerticalShipSpeed) < 1)
        {
            currentVerticalShipSpeed = 0;
        }

        ActivateCompensators();

        transform.position += transform.TransformDirection(Vector3.forward) * currenForwardShipSpeed * Time.deltaTime;
        transform.position += transform.TransformDirection(Vector3.right) * currentLateralShipSpeed * Time.deltaTime;
        transform.position += transform.TransformDirection(Vector3.up) * currentVerticalShipSpeed * Time.deltaTime;
    }

    void ActivateThrottles()
    {
        CheckThrusterOfDirection(rightGas, KeyCode.D);
        CheckThrusterOfDirection(topGas, KeyCode.LeftControl);
        CheckThrusterOfDirection(leftGas, KeyCode.A);
        CheckThrusterOfDirection(bottomGas, KeyCode.Space);
    }

    void CheckThrusterOfDirection(ParticleSystem thruster, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            thruster.Play();
        }
        else if (Input.GetKeyUp(keyCode))
        {
            thruster.Stop();
        }
    }

    void ActivateCompensators()
    {
        CheckCompensatorOfDirection(rightGas, KeyCode.A, currentLateralSpeed, currentLateralShipSpeed);
        CheckCompensatorOfDirection(leftGas, KeyCode.D, currentLateralSpeed, currentLateralShipSpeed);
        CheckCompensatorOfDirection(topGas, KeyCode.Space, currentVerticalSpeed, currentVerticalShipSpeed);
        CheckCompensatorOfDirection(bottomGas, KeyCode.LeftControl, currentVerticalSpeed, currentVerticalShipSpeed);
    }

    void CheckCompensatorOfDirection(ParticleSystem thruster, KeyCode keyCode, float speed, float shipSpeed)
    {
        if (Input.GetKeyUp(keyCode))
        {
            thruster.Play();
        }
        if (Input.GetKey(keyCode) || (Mathf.Abs(speed) == 0 && Mathf.Abs(shipSpeed) == 0))
        {
            thruster.Stop();
        }
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
