using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond;
    [SerializeField] private Vector3 axis;

    void Update()
    {
        transform.Rotate(axis, degreesPerSecond * Time.deltaTime);
    }
}
