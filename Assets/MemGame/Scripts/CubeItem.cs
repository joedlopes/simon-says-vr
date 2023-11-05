using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCode : int
{
    Red = 0,
    Green = 1,
    Blue = 2,
    Yellow = 3,
    Black = 4,
}

public class CubeItem : MonoBehaviour
{
    public ItemCode code;
    public AudioClip sound;
    public Material color;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;

        GetComponent<Renderer>().material = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Table")
        {
            other.gameObject.GetComponent<Table>().OnItemAttached(this);
            audioSource.Play();            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Table")
        {
            other.gameObject.GetComponent<Table>().OnItemRemoved(this);
        }
    }



}
