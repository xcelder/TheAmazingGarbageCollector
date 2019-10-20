using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond;

    void Update()
    {
        transform.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime);
    }
}
