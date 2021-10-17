using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Material DeathMat;
    private float DeathRange;
    public bool DoDeath = false;
    // Start is called before the first frame update
    void Start()
    {
        DeathRange = 0;
        DoDeath = false;
        DeathMat.SetFloat("_Value", DeathRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(DoDeath == true)
        {
            DeathRange = Mathf.Lerp(DeathRange, 1.1f, 0.005f);
        }
        else
        {
            DeathRange = Mathf.Lerp(DeathRange, -0.1f, 0.01f);
        }
        DeathMat.SetFloat("_Value", DeathRange);
    }
}
