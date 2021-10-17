using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisp : MonoBehaviour
{
    public Slider Slider1;
    public Slider Slider2;

    [SerializeField, Range(0.0f, 1.0f)]
    public float Value;
    
    // Start is called before the first frame update
    void Start()
    {
        Slider1.value = 0;
        Slider2.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Slider1.value = Value;
        Slider2.value = Value;
    }
}
