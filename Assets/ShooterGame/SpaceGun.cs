using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class SpaceGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform barrelEnd;
    public float shootingForce = 1000f;


    private InputDevice device;
    private AudioSource audioSource;

    public XRNode hand = XRNode.RightHand;

    
    private bool prevState;

    private void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(hand);
        audioSource = GetComponent<AudioSource>();
        prevState = false;
    }

    private void Update()
    {
        bool triggerValue;

        if (device.isValid && device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue))
        {
            if (prevState == false && triggerValue == true)
            {
                Shoot();
                device.SendHapticImpulse(0, 0.25f, 0.15f);
                
                audioSource.Stop(); audioSource.Play();
            }
            prevState = triggerValue;
        }

    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(barrelEnd.forward * shootingForce);
    }
}
