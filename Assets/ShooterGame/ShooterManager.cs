using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{

    public GameObject targetPrefab;

    public List<Transform> spawPoints;

    void Start()
    {
        StartCoroutine(SpawTargets());
    }

    IEnumerator SpawTargets()
    {
        yield return new WaitForSeconds(5.5f);
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            int idx = Random.Range(0, spawPoints.Count);
            Vector3 position = new Vector3(spawPoints[idx].localPosition.x, spawPoints[idx].localPosition.y, spawPoints[idx].localPosition.z);
            Instantiate(targetPrefab, position, Quaternion.identity);
        }
    }
}
