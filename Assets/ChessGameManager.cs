using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameManager : MonoBehaviour
{
    /*
    WhitePiece[0] WhitePawn1
    WhitePiece[1] WhitePawn2
    WhitePiece[2] WhitePawn3
    WhitePiece[3] WhitePawn4
    WhitePiece[4] WhitePawn5
    WhitePiece[5] WhitePawn6
    WhitePiece[6] WhitePawn7
    WhitePiece[7] WhitePawn8
    WhitePiece[8] WhiteRook1
    WhitePiece[9] WhiteRook2
    WhitePiece[10] WhiteKnight1
    WhitePiece[11] WhiteKnight2
    WhitePiece[12] WhiteBishop1
    WhitePiece[13] WhiteBishop2
    WhitePiece[14] WhiteQueen
    WhitePiece[15] WhiteKing

    BlackPiece[0] BlackPawn1
    BlackPiece[1] BlackPawn2
    BlackPiece[2] BlackPawn3
    BlackPiece[3] BlackPawn4
    BlackPiece[4] BlackPawn5
    BlackPiece[5] BlackPawn6
    BlackPiece[6] BlackPawn7
    BlackPiece[7] BlackPawn8
    BlackPiece[8] BlackRook1
    BlackPiece[9] BlackRook2
    BlackPiece[10] BlackKnight1
    BlackPiece[11] BlackKnight2
    BlackPiece[12] BlackBishop1
    BlackPiece[13] BlackBishop2
    BlackPiece[14] BlackQueen
    BlackPiece[15] BlackKing
    */

    public GameObject[] WhitePiece = new GameObject[16];
    public GameObject[] BlackPiece = new GameObject[16];

    public Piece[] WhitePieceScripts = new Piece[16];
    public Piece[] BlackPieceScripts = new Piece[16];

    public Destroy[] WDest = new Destroy[16];
    public Destroy[] BDest = new Destroy[16];

    public int[,] Floor = new int[8,8];

    public bool LookPiece;

    public int[,] AllyPossibility = new int[8,8];
    public int[,] EnemyPossibility = new int[8,8];

    void FloorDataUpdate()
    {
        Floor = new int[8,8];

        /*
        何もない = 0
        白のポーン = 1
        白のルーク = 2
        白のナイト = 3
        白のビショップ = 4
        白のクイーン = 5
        白のキング = 6
        プレイヤー = 10
        黒のポーン = -1
        黒のルーク = -2
        黒のナイト = -3
        黒のビショップ = -4
        黒のクイーン = -5
        黒のキング = -6
        */
        
        for (int p = 0; p < 16; p++)
        {
            if(!WDest[p].DoDestroy)
            Floor[WhitePieceScripts[p].FloorPos[0],WhitePieceScripts[p].FloorPos[1]] = WhitePieceScripts[p].PieceType;

            if(!BDest[p].DoDestroy)
            Floor[BlackPieceScripts[p].FloorPos[0],BlackPieceScripts[p].FloorPos[1]] = BlackPieceScripts[p].PieceType;
        }

        Debug.Log ("\r\n" + 
            Floor[0,7] + " " + Floor[1,7] + " " + Floor[2,7] + " " + Floor[3,7] + " " + Floor[4,7] + " " + Floor[5,7] + " " + Floor[6,7] + " " + Floor[7,7] + "\r\n" + 
            Floor[0,6] + " " + Floor[1,6] + " " + Floor[2,6] + " " + Floor[3,6] + " " + Floor[4,6] + " " + Floor[5,6] + " " + Floor[6,6] + " " + Floor[7,6] + "\r\n" + 
            Floor[0,5] + " " + Floor[1,5] + " " + Floor[2,5] + " " + Floor[3,5] + " " + Floor[4,5] + " " + Floor[5,5] + " " + Floor[6,5] + " " + Floor[7,5] + "\r\n" + 
            Floor[0,4] + " " + Floor[1,4] + " " + Floor[2,4] + " " + Floor[3,4] + " " + Floor[4,4] + " " + Floor[5,4] + " " + Floor[6,4] + " " + Floor[7,4] + "\r\n" + 
            Floor[0,3] + " " + Floor[1,3] + " " + Floor[2,3] + " " + Floor[3,3] + " " + Floor[4,3] + " " + Floor[5,3] + " " + Floor[6,3] + " " + Floor[7,3] + "\r\n" + 
            Floor[0,2] + " " + Floor[1,2] + " " + Floor[2,2] + " " + Floor[3,2] + " " + Floor[4,2] + " " + Floor[5,2] + " " + Floor[6,2] + " " + Floor[7,2] + "\r\n" + 
            Floor[0,1] + " " + Floor[1,1] + " " + Floor[2,1] + " " + Floor[3,1] + " " + Floor[4,1] + " " + Floor[5,1] + " " + Floor[6,1] + " " + Floor[7,1] + "\r\n" + 
            Floor[0,0] + " " + Floor[1,0] + " " + Floor[2,0] + " " + Floor[3,0] + " " + Floor[4,0] + " " + Floor[5,0] + " " + Floor[6,0] + " " + Floor[7,0]
            );
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int p = 0; p < 16; p++)
        {
            WhitePieceScripts[p] = WhitePiece[p].GetComponent<Piece>();
            BlackPieceScripts[p] = BlackPiece[p].GetComponent<Piece>();
            WDest[p] = WhitePiece[p].GetComponent<Destroy>();
            BDest[p] = BlackPiece[p].GetComponent<Destroy>();
        }
        FloorDataUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if(LookPiece == true)
        {
            FloorDataUpdate();
            LookPiece = false;
        }
    }
}
