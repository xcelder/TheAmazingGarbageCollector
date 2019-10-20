using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 directionVector;
    [SerializeField] private float minDegPerSec;
    [SerializeField] private float maxDegPerSec;

    void Start() {
        directionVector = new Vector3(
            Random.Range(minDegPerSec, maxDegPerSec),
            Random.Range(minDegPerSec, maxDegPerSec),
            Random.Range(minDegPerSec, maxDegPerSec)
        );
    }

    void Update()
    {
        transform.Rotate(directionVector * Time.deltaTime);
    }
}
