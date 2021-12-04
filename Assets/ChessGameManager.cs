using System;
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

    public bool DoUpdate;

    public int[,] PlayerPossibility = new int[8,8];
    public int[,] AllyPossibility = new int[8,8];
    public int[,] EnemyPossibility = new int[8,8];
    public GameObject[] PossibilityTile;

    public GameObject[] PlayerPossibilityTile;

    //マス表示用
    public void DebugDataLog(int[,] data)
    {
        Debug.Log ("\r\n" + 
            data[0,7] + " " + data[1,7] + " " + data[2,7] + " " + data[3,7] + " " + data[4,7] + " " + data[5,7] + " " + data[6,7] + " " + data[7,7] + "\r\n" + 
            data[0,6] + " " + data[1,6] + " " + data[2,6] + " " + data[3,6] + " " + data[4,6] + " " + data[5,6] + " " + data[6,6] + " " + data[7,6] + "\r\n" + 
            data[0,5] + " " + data[1,5] + " " + data[2,5] + " " + data[3,5] + " " + data[4,5] + " " + data[5,5] + " " + data[6,5] + " " + data[7,5] + "\r\n" + 
            data[0,4] + " " + data[1,4] + " " + data[2,4] + " " + data[3,4] + " " + data[4,4] + " " + data[5,4] + " " + data[6,4] + " " + data[7,4] + "\r\n" + 
            data[0,3] + " " + data[1,3] + " " + data[2,3] + " " + data[3,3] + " " + data[4,3] + " " + data[5,3] + " " + data[6,3] + " " + data[7,3] + "\r\n" + 
            data[0,2] + " " + data[1,2] + " " + data[2,2] + " " + data[3,2] + " " + data[4,2] + " " + data[5,2] + " " + data[6,2] + " " + data[7,2] + "\r\n" + 
            data[0,1] + " " + data[1,1] + " " + data[2,1] + " " + data[3,1] + " " + data[4,1] + " " + data[5,1] + " " + data[6,1] + " " + data[7,1] + "\r\n" + 
            data[0,0] + " " + data[1,0] + " " + data[2,0] + " " + data[3,0] + " " + data[4,0] + " " + data[5,0] + " " + data[6,0] + " " + data[7,0]
            );
    }

    //現在盤面のどこに何があるのか情報を取得
    //コマ毎に座標を送ってもらう
    public void FloorDataUpdate()
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

        //DebugDataLog(Floor);
    }

    //コマ毎の移動可能範囲を取得
    void GetPossibilityArea()
    {
        PlayerPossibility = new int[8,8];
        AllyPossibility = new int[8,8];
        EnemyPossibility = new int[8,8];

        for (int p = 0; p < 16; p++)
        {
            var Ally = WhitePieceScripts[p].CanMove(Floor);
            var Player = WhitePieceScripts[3].PlayerPawnCanMove(Floor);
            var Enemy = BlackPieceScripts[p].CanMove(Floor);

            for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
            {
                AllyPossibility[x, y] += Ally[x, y];
                PlayerPossibility[x, y] += Player[x, y];
                EnemyPossibility[x, y] += Enemy[x, y];
            }
        }
    }

    //移動の可能性があるマスを表示
    void LookPT()
    {
        int Count = 0;
        for (int y = 0; y < 8; y++)
        for (int x = 0; x < 8; x++)
        {
            if(EnemyPossibility[x,y] >= 1)PossibilityTile[Count].SetActive(true);
            else PossibilityTile[Count].SetActive(false);
            
            if(PlayerPossibility[x,y] >= 1)PlayerPossibilityTile[Count].SetActive(true);
            else PlayerPossibilityTile[Count].SetActive(false);

            Count++;
        }
    }

    void DoNotLookPT()
    {
        for (int x = 0; x <= 8; x++)PossibilityTile[x].SetActive(false);
    }

    /// <summary>
    /// whiteかblackを選択してコストマップを計算
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private IEnumerator CalcCostMap(Turn color)
    {
        //評価点の絶対値の取得に使う
        int abs = 0;
        //何番目のゲームオブジェクトかを指定するために使う
        int num = 0;

        Piece[] scrs = null;
        int[,] possibilities = null;
        //どちらのターンかによって返す情報を変える
        //possibilitiesに後で呼び出す情報を格納する
        if (color == Turn.white)
        {
            scrs = WhitePieceScripts;
            //白のターンの時は黒の移動可能エリアを格納
            possibilities = EnemyPossibility;
        }
        else if (color == Turn.black)
        {
            scrs = BlackPieceScripts;
            //黒のターンの時は白の移動可能エリアを格納
            possibilities = AllyPossibility;
        }
        else Debug.Log("Error: 不正なターン形式です。");

        for (int i = 0; i < 16;i++)
        {
            //possibilitiesに相手の移動可能エリアが格納されているので、コマ一つ一つにその情報を送る
            //現在の盤面の状況も送る
            //評価点(Cost)を返してもらう
            //コルーチンによってこの処理を終えるまで後の処理を一時停止して、終わったらそれ以下の処理を行う
            yield return StartCoroutine(scrs[i].CalcMyCostMap(possibilities, Floor));

            //帰ってきた評価点の最大値と最小値のどちらが絶対値として大きいか判別
            //絶対値が大きい方を使う
            int maxAbs = scrs[i].maxCost;
            int minAbs = scrs[i].minCost;
            if(maxAbs > abs || minAbs > abs)
            {
                if(maxAbs > minAbs) abs = maxAbs;
                else abs = minAbs;
            }
        }

        //リスト(動的二次元配列)を用意し、先ほど用意したabsの値を格納していく
        List<Piece> MaxCostPieces = new List<Piece>();
        for (int i = 0; i < 16; i++)
        {
            //absの格納
            if(scrs[i].maxCost == abs || scrs[i].minCost == abs)MaxCostPieces.Add(scrs[i]);
        }

        //リストの中からランダムに一つピックアップする
        num = UnityEngine.Random.Range(0, MaxCostPieces.Count);

        //ターン,コマ,評価点
        Debug.Log($"{color} GameObject{num} {MaxCostPieces[num].GetType()} : {abs}");
        //そのコマのコストマップを出力
        DebugDataLog(MaxCostPieces[num].costMap);
    }

    /*
    コストの計算
    移動できるマス +1
    移動できないマス -1
    移動するとコマを取れるマス +コマに設定されている点数+1
    移動すると取られる可能性のあるマス -自分のコマに設定されている点数

    ポーン 1
    ルーク 4
    ナイト 2
    ビショップ 3
    クイーン 5
    キング 100
    */

    public void FloorVisualUpdate()
    {
        FloorDataUpdate();
        GetPossibilityArea();
        LookPT();
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

        //なぜかstartでマップ情報が正常に読み込まれない
        /*
        FloorDataUpdate();
        GetPossibilityArea();
        LookPT();
        DebugDataLog(PlayerPossibility);
        */
        //StartCoroutine(CalcCostMap(Turn.white));
    }

    // Update is called once per frame
    void Update()
    {
        if(DoUpdate == true)
        {
            //盤面の状況を取得
            FloorDataUpdate();

            //盤面の状況をデバッグログで表示
            //DebugDataLog(Floor);

            //コマ毎の移動可能範囲を取得
            GetPossibilityArea();

            //移動可能範囲をデバッグログで表示
            //DebugDataLog(PlayerPossibility);
            //DebugDataLog(AllyPossibility);
            //DebugDataLog(EnemyPossibility);

            //移動の可能性があるエリアをフロアに表示
            LookPT();
            
            //移動させるべきコマをデバッグログに表示
            //StartCoroutine(CalcCostMap(Turn.white));
            StartCoroutine(CalcCostMap(Turn.black));


            DoUpdate = false;
        }
    }
}
