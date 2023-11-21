using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceProjectile : MonoBehaviour
{
    public float lifeTime = 5f;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
