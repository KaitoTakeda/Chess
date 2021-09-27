using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //public Camera MainCamera;
    private Vector3 RayPos = Vector3.zero;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
        RayPos = Input.mousePosition;
        RayPos.z = 10;
        //RayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RayPos.y = 0;
        //this.transform.position = Camera.main.ScreenToWorldPoint(RayPos);

        /*
        if(Physics.Raycast(Camera.main.transform.position, (Camera.main.ScreenToWorldPoint(RayPos) - Camera.main.transform.position), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, (Camera.main.ScreenToWorldPoint(RayPos) - Camera.main.transform.position) * hit.distance, Color.red);
        }
        */

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red);
        }
    }
}
