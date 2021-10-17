using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Material Mat;
    public bool DoDestroy = false;
    private float Value = 0;

    // Start is called before the first frame update
    void Start()
    {
        Value = 0;
        Mat.SetFloat("_Range", Value);
    }

    // Update is called once per frame
    void Update()
    {
        if(DoDestroy == true)
        {
            if(Value < 0.005f)
            {
                Value += 0.0001f;
            }
            else if(Value >= 0.005f && Value < 0.01f)
            {
                Value += 0.0005f;
            }
            else if(Value >= 0.01f && Value < 0.05f)
            {
                Value += 0.001f;
            }
            else if(Value >= 0.05f && Value < 0.5f)
            {
                Value += 0.01f;
            }
            else if(Value >= 0.5f && Value < 1)
            {
                Value += 0.05f;
            }
        }

        if (Value >= 1)
        {
            this.gameObject.SetActive(false);
            Value = 0;
        }

        Mat.SetFloat("_Range", Value);
    }
}
