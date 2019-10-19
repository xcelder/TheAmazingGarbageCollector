using UnityEngine;

public class GarbageCreator : MonoBehaviour
{
    private const int EARTH_BASE_RADIUS = 3000;

    [SerializeField] private Transform debrisContainer;

    [Header("Debris Prefabs")]
    [SerializeField] private GameObject[] smallDebris;
    [SerializeField] private GameObject[] mediumDebris;
    [SerializeField] private GameObject[] largeDebris;

    [Header("Debris Data")]
    [SerializeField] private TextAsset jsonFile;


    void Start()
    {
        // Create the initial debris.
        DebrisData[] debrisData = JsonUtility.FromJson<DebrisContainer>(jsonFile.text).debris;
        for (int i = 0; i < debrisData.Length; i++)
        {
            Vector3 currentPosition = LatLongToVector3(debrisData[i].lat1, debrisData[i].lon1, debrisData[i].alt + EARTH_BASE_RADIUS);
            // Calculate movement Vector using its next position.
            Vector3 nextPosition = LatLongToVector3(debrisData[i].lat2, debrisData[i].lon2, debrisData[i].alt + EARTH_BASE_RADIUS);
            Vector3 movementVector = nextPosition - currentPosition;
            // Choose the debris to instantiate depending on its size.
            DebrisSize size = debrisData[i].size;
            // Create the debris object.
            GameObject debris = CreateDebris(GetRandomDebrisOfSize(size), debrisContainer, currentPosition);
            // Set the debris orbit.
            Orbit orbit = debris.GetComponent<Orbit>();
            if (orbit != null)
            {
                orbit.SetOrbit(Vector3.Cross(-currentPosition, movementVector), debrisData[i].revs_per_day * 100);
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


    private GameObject[] GetDebrisCollection(DebrisSize size)
    {
        switch (size)
        {
            default:
            case DebrisSize.Small:
                return smallDebris;
            case DebrisSize.Medium:
                return mediumDebris;
            case DebrisSize.Large:
                return largeDebris;
        }
    }

    private GameObject GetRandomDebrisOfSize(DebrisSize size)
    {
        GameObject[] debrisCollection = GetDebrisCollection(size);
        return debrisCollection[Random.Range(0, debrisCollection.Length)];
    }

    private GameObject CreateDebris(GameObject prefab, Transform container, Vector3 localPosition)
    {
        GameObject debris = Instantiate(prefab, container);
        debris.transform.localPosition = localPosition;
        // TODO: Set random rotation.
        return debris;
    }

}
