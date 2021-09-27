using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP : MonoBehaviour
{
    public GameObject Player;
    private float Height;

    public float R;
    private float Degree;
    private float H,V;
    public float MouseSensitive;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        H = Input.GetAxis("Mouse X");
        V = Input.GetAxis("Mouse Y");
        Degree += H*MouseSensitive;
        Height -= V*MouseSensitive;
        Height = Mathf.Clamp(Height, -85, 90);

        transform.eulerAngles = new Vector3(Height, Degree, 0);
        /*
        float x = Mathf.Cos(Degree)*R;
        float y = Height;
        float z = Mathf.Sin(Degree)*R;

        transform.position = new Vector3(x, y, z);
        transform.LookAt(Player.transform.position);
        transform.forward *= -1;
        */
    }
}
