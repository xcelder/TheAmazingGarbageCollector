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
            CreateDebris(smallDebris, debrisContainer, LatLongToVector3(debrisData.debrisData[i].lat, debrisData.debrisData[i].lon, debrisData.debrisData[i].alt + 3000));
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
