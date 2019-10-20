﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetDistance : MonoBehaviour
{

    [SerializeField]
    private GameObject distancePanel;

    [SerializeField]
    private TMP_Text distanceLabel;

    private float cameraOffset = 0;

    private 
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1200) && 
            hit.transform != null)
        {
            distancePanel.SetActive(true);
            distanceLabel.text = (hit.distance - cameraOffset) * 2 + " Km";

        }
        else
        {
            distancePanel.SetActive(false);
        }
    }

}
