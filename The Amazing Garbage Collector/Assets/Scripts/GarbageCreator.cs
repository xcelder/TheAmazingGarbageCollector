using UnityEngine;

public class GarbageCreator : MonoBehaviour
{
    [SerializeField] private Transform debrisContainer;

    [Header("Debris Prefabs")]
    [SerializeField] private GameObject smallDebris;
    [SerializeField] private GameObject mediumDebris;
    [SerializeField] private GameObject largeDebris;


    [SerializeField] private TextAsset jsonFile;


    void Start()
    {
        // Create the initial debris.
        DebrisContainer debrisData = JsonUtility.FromJson<DebrisContainer>(jsonFile.text);
        for (int i = 0; i < debrisData.debrisData.Length; i++)
        {
            Vector3 currentPosition = LatLongToVector3(debrisData.debrisData[i].lat1, debrisData.debrisData[i].lon1, debrisData.debrisData[i].alt + 3000);
            // Calculate movement Vector using its next position.
            Vector3 nextPosition = LatLongToVector3(debrisData.debrisData[i].lat2, debrisData.debrisData[i].lon2, debrisData.debrisData[i].alt + 3000);
            Vector3 movementVector = nextPosition - currentPosition;
            // Create the debris object.
            GameObject debris = CreateDebris(smallDebris, debrisContainer, currentPosition);
            // Set the debris orbit.
            Orbit orbit = debris.GetComponent<Orbit>();
            if (orbit != null)
            {
                orbit.SetOrbit(Vector3.Cross(-currentPosition, movementVector), debrisData.debrisData[i].revs_per_day / (24));
            }
        }
    }


    private Vector3 LatLongToVector3(float latitude, float longitude, float height)
    {
        float Xpos = height * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        float Ypos = height * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        float Zpos = height * Mathf.Sin(latitude);
        return new Vector3(Xpos, Ypos, Zpos);
    }


    private GameObject CreateDebris(GameObject prefab, Transform container, Vector3 localPosition)
    {
        GameObject debris = Instantiate(prefab, container);
        debris.transform.localPosition = localPosition;
        // TODO: Set random rotation.
        return debris;
    }

}
