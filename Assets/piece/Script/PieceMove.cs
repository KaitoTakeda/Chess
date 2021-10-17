using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    public GameObject Piece;
    public int[] InputTilePosForPieceMove = new int[2];
    public bool Move0 = false;
    public bool Move1 = false;
    public bool Move2 = false;

    public Vector3 PiecePos = Vector3.zero;
    public Vector3 PrePiecePos = Vector3.zero;

    public bool FirstTime = false;

    // Start is called before the first frame update
    void Start()
    {
        Piece = this.gameObject;

        InputTilePosForPieceMove[0] = Mathf.FloorToInt(Mathf.Floor(Piece.transform.position.x/10)) + 4;
        InputTilePosForPieceMove[1] = Mathf.FloorToInt(Mathf.Floor(Piece.transform.position.z/10)) + 4;

        PiecePos.x = Piece.transform.position.x;
        PiecePos.z = Piece.transform.position.z;
        PiecePos.y = 0.5f;
    }

    void DoMove0()
    {
        if(FirstTime == false)
        {
            FirstTime = true;
        }
        
        PiecePos.y = Mathf.MoveTowards(PiecePos.y, 2, 0.01f);
        if(PiecePos.y >= 1.95f)
        {
            Move1 = true;
            Move0 = false;
        }
    }

    void DoMove1()
    {
        PiecePos.x = Mathf.Lerp(PiecePos.x, PrePiecePos.x, 0.005f);
        PiecePos.z = Mathf.Lerp(PiecePos.z, PrePiecePos.z, 0.005f);
        if(PiecePos.x >= PrePiecePos.x-0.05f && PiecePos.x <= PrePiecePos.x+0.05f && PiecePos.z >= PrePiecePos.z-0.05f && PiecePos.z <= PrePiecePos.z+0.05f)
        {
            Move2 = true;
            Move1 = false;
        }
    }

    void DoMove2()
    {
        PiecePos.y = Mathf.MoveTowards(PiecePos.y, 0.5f, 0.01f);
        if (PiecePos.y <= 0.55f)
        {
            Move2 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PrePiecePos.x = (InputTilePosForPieceMove[0] - 4) * 10 + 5;
        PrePiecePos.z = (InputTilePosForPieceMove[1] - 4) * 10 + 5;
        PrePiecePos.y = 0.5f;

        if(Move0 == true && Move1 == false && Move2 == false)
        {
            DoMove0();
        }
        else if(Move0 == false && Move1 == true && Move2 == false)
        {
            DoMove1();
        }
        else if (Move0 == false && Move1 == false && Move2 == true)
        {
            DoMove2();
        }
        Piece.transform.position = PiecePos;
    }
}
