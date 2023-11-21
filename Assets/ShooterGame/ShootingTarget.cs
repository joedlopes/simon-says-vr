using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            audioSource.Play();
            Destroy(gameObject, 0.05f);
        }

        
    }
}
