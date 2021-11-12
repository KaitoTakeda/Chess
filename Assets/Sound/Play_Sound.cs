using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Sound : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Piece")){
            Debug.Log ("Play Sound");
        }
    }
}
