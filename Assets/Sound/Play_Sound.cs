using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Sound : MonoBehaviour
{
    public AudioClip Sound;
    private AudioSource audioSource;

    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Piece")){
            //Debug.Log ("Play Sound");
            audioSource.PlayOneShot(Sound);
            Debug.Log("コマが通過した");
        }
    }
}
