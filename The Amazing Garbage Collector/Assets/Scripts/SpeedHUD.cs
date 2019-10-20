using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedHUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text speedLabel;

    private GCShipMovement shipMovement;

    const string speedUnit = "Km/h";

    // Start is called before the first frame update
    void Start()
    {
        shipMovement = GetComponent<GCShipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float realSpeed = (shipMovement.currentForwardSpeed * 45000) / 265;
        speedLabel.text = realSpeed + " " + speedUnit;
    }
}
