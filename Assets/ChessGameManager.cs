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

    public int[,] Floor = new int[8,8];

    public bool LookPiece;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
        {
            Floor[i, j] = 0;
        }
    }

    void DoLookPiece()
    {
        for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
        {
            Floor[i, j] = 0;
        }

        /*
        何もない = 0
        白のポーン = 1
        プレイヤー = 2
        白のルーク = 3
        白のナイト = 4
        白のビショップ = 5
        白のクイーン = 6
        白のキング = 7
        黒のポーン = 8
        黒のルーク = 9
        黒のナイト = 10
        黒のビショップ = 11
        黒のクイーン = 12
        黒のキング = 13
        */

        for (int p = 0; p < 16; p++)
        {
            if(WhitePiece[p].GetComponent<Destroy>().DoDestroy == false){
                if(p == 3)
                {
                    Floor[WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[0], WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 2;
                }
                else if(p >= 8 && p < 10)
                {
                    Floor[WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[0], WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 3;
                }
                else if(p >= 10 && p < 12)
                {
                    Floor[WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[0], WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 4;
                }
                else if(p >= 12 && p < 14)
                {
                    Floor[WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[0], WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 5;
                }
                else if(p == 14)
                {
                    Floor[WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[0], WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 6;
                }
                else if(p == 15)
                {
                    Floor[WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[0], WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 7;
                }
                else
                {
                    Floor[WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[0], WhitePiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 1;
                }
            }

            if(BlackPiece[p].GetComponent<Destroy>().DoDestroy == false){
                if(p >= 8 && p < 10)
                {
                    Floor[BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[0], BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 9;
                }
                else if(p >= 10 && p < 12)
                {
                    Floor[BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[0], BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 10;
                }
                else if(p >= 12 && p < 14)
                {
                    Floor[BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[0], BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 11;
                }
                else if(p == 14)
                {
                    Floor[BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[0], BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 12;
                }
                else if(p == 15)
                {
                    Floor[BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[0], BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 13;
                }
                else
                {
                    Floor[BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[0], BlackPiece[p].GetComponent<PieceTileCheck>().FloorPos[1]] = 8;
                }
            }

            
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
        LookPiece = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(LookPiece == true)
        {
            DoLookPiece();
        }
    }
}
