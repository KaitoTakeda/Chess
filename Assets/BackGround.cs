using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Material Mat;

    public Color Col;
    public Color DamCol;
    public Color OutputCol;
    public bool Damage;
    // Start is called before the first frame update
    void Start()
    {
        OutputCol = Col;
    }

    // Update is called once per frame
    void Update()
    {
        if(Damage == true)
        {
            OutputCol = DamCol;
            Damage = false;
        }
        OutputCol.r = Mathf.MoveTowards(OutputCol.r, Col.r, 0.025f);
        OutputCol.g = Mathf.MoveTowards(OutputCol.g, Col.g, 0.025f);
        OutputCol.b = Mathf.MoveTowards(OutputCol.b, Col.b, 0.025f);

        Mat.SetColor("_Col", new Color(OutputCol.r,OutputCol.g,OutputCol.b,1));
    }
}
