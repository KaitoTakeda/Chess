using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Material FloorMat;
    public Color ColBlack;
    public Color Colwhite;
    public Color TreasonCol;
    private Color OutputColwhite;
    [SerializeField, Range(0.0f, 1.0f)]
    public float DistortionRange;
    public bool Treason;

    // Start is called before the first frame update
    void Start()
    {
        FloorMat.SetColor("_ColBlack", new Color(ColBlack.r,ColBlack.g,ColBlack.b,ColBlack.a));
        FloorMat.SetColor("_ColWhite", new Color(Colwhite.r,Colwhite.g,Colwhite.b,Colwhite.a));
        FloorMat.SetFloat("_Range", DistortionRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(Treason == true){
            Colwhite = TreasonCol;
        }
        OutputColwhite.r = Mathf.MoveTowards(OutputColwhite.r, Colwhite.r, 0.025f);
        OutputColwhite.g = Mathf.MoveTowards(OutputColwhite.g, Colwhite.g, 0.025f);
        OutputColwhite.b = Mathf.MoveTowards(OutputColwhite.b, Colwhite.b, 0.025f);
        FloorMat.SetFloat("_Range", DistortionRange);
        FloorMat.SetColor("_ColWhite", new Color(OutputColwhite.r,OutputColwhite.g,OutputColwhite.b,OutputColwhite.a));
    }
}
