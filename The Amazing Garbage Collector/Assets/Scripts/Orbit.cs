using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private Vector3 orbitAxis;
    private float revsPerSecond;


    void Update()
    {
        transform.RotateAround(new Vector3(), orbitAxis, revsPerSecond / 360f * Time.deltaTime);
    }


    public void SetOrbit(Vector3 orbitAxis, float revsPerSecond)
    {
        this.orbitAxis = orbitAxis;
        this.revsPerSecond = revsPerSecond;
    }
}
