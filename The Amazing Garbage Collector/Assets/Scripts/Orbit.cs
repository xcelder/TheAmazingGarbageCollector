using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private Vector3 orbitAxis;
    [SerializeField] private float revsPerSecond;

    [SerializeField] private bool debug;


    void Update()
    {
        Vector3 prevPosition = transform.position;
        transform.RotateAround(new Vector3(), orbitAxis, revsPerSecond / 360f * Time.deltaTime);
        if (debug) {
            Vector3 newPosition = transform.position;
            float distance = Vector3.Distance(newPosition, prevPosition);
            Debug.LogFormat("{0} moved a distance of {1} units. Speed: {2} u/s.", gameObject.name, distance, distance / Time.deltaTime);
        }
    }


    public void SetOrbit(Vector3 orbitAxis, float revsPerSecond)
    {
        this.orbitAxis = orbitAxis;
        this.revsPerSecond = revsPerSecond;
    }
}
