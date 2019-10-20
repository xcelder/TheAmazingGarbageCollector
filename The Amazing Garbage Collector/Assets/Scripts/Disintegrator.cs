using System.Collections;
using UnityEngine;

public class Disintegrator : MonoBehaviour
{
    private const string DISINTEGRATION_PROPERTY_ID = "Vector1_DBFF534E";

    [SerializeField] private float effectTime = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            // Deactivate Collider
            other.enabled = false;
            // Disintegrate OMEGAOMG
            StartCoroutine(Disintegrate(other.gameObject, effectTime));
        }
    }


    private IEnumerator Disintegrate(GameObject target, float time)
    {
        float remainingTime = time;
        Renderer renderer = target.GetComponent<Renderer>();
        var block = new MaterialPropertyBlock();

        while (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            block.SetFloat(DISINTEGRATION_PROPERTY_ID, Mathf.Lerp(-1f, 1f, (time - remainingTime) / time));
            renderer.SetPropertyBlock(block);
            yield return null;
        }
        Destroy(target);
    }

}
