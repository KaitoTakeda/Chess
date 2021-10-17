using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField, Range(0.0f, 8.0f)]
    public float StartDist;

    [SerializeField, Range(0.0f, 8.0f)]
    public float EndDist;
    public Color Col;
    public Color DamageCol;
    public Color TreasonCol;
    public Color OutputCol;
    public bool Damage;
    public bool Treason;


    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        OutputCol = Col;
        /*
        ColorValue[0] = Col.r;
        ColorValue[1] = Col.g;
        ColorValue[2] = Col.b;
        PreColorValue[0] = DamageCol.r;
        PreColorValue[1] = DamageCol.g;
        PreColorValue[2] = DamageCol.b;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(Damage == true)
        {
            OutputCol = DamageCol;
            Damage = false;
        }

        if(Treason == true){
            Col = TreasonCol;
        }

        OutputCol.r = Mathf.MoveTowards(OutputCol.r, Col.r, 0.015f);
        OutputCol.g = Mathf.MoveTowards(OutputCol.g, Col.g, 0.015f);
        OutputCol.b = Mathf.MoveTowards(OutputCol.b, Col.b, 0.015f);
        RenderSettings.fogColor = OutputCol;
        RenderSettings.fogStartDistance = StartDist*10;
        RenderSettings.fogEndDistance = EndDist*10;
    }
}
