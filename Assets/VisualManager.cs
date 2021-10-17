using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    [SerializeField, Range(0.0f, 8.0f)]
    public float FogRange;
    [SerializeField, Range(0.0f, 1.0f)]
    public float FloorDistortionRange;

    //public Color DamageColor;
    public bool Damage;
    //private int MaxDamTimes = 10;

    public bool Death;
    // Start is called before the first frame update
    void Start()
    {
        FogRange = GetComponent<Fog>().EndDist;
    }

    void DoDamage()
    {
        if (FloorDistortionRange < 1)
        {
            GetComponent<Fog>().Damage = Damage;
            FloorDistortionRange += 0.1f;
        }
        else if(FloorDistortionRange >= 1)
        {
            if(GetComponent<Fog>().Treason == false)
            {
                //GetComponent<Fog>().Damage = Damage;
                GetComponent<Fog>().Treason = true;
                GetComponent<Floor>().Treason = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Damage == true)
        {
            DoDamage();
            Damage = false;
        }
        GetComponent<Fog>().EndDist = FogRange;
        GetComponent<Floor>().DistortionRange = FloorDistortionRange;
        GetComponent<Death>().DoDeath = Death;
    }
}
