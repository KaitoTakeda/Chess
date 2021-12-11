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

    //public bool DoUpdate;

    public int[,] PlayerPossibility = new int[8,8];
    public int[,] AllyPossibility = new int[8,8];
    public int[,] EnemyPossibility = new int[8,8];
    public GameObject[] PossibilityTile;

    public GameObject[] PlayerPossibilityTile;
    public GameObject[] CommandPossibilityTile;

    private bool FirstCrick;
    public int WhitePieceNum;
    public int BlackPieceNum;
    public int[] Destination = new int[2];
    public int[] WhiteDest = new int[2];
    public int[] BlackDest = new int[2];

    Turn turn;

    public bool ClickCheck = false;
    public bool NoClick = false;
    public bool FirstClick = false;
    private bool WhitePieceTurn = false;

    public GameObject VisualManager;
    public float Betrayal = 0;


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
        FloorDataUpdate();

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
    //指令があるかどうかも表示
    
    void LookPT()
    {
        GetPossibilityArea();

        int Count = 0;
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                if (EnemyPossibility[x, y] >= 1) PossibilityTile[Count].SetActive(true);
                else PossibilityTile[Count].SetActive(false);

                if (PlayerPossibility[x, y] >= 1) PlayerPossibilityTile[Count].SetActive(true);
                else PlayerPossibilityTile[Count].SetActive(false);

                if (WhitePieceNum == 3)
                {
                    if (x == Destination[0] && y == Destination[1]) CommandPossibilityTile[Count].SetActive(true);
                    else CommandPossibilityTile[Count].SetActive(false);
                }
                else
                {
                    if (x == WhitePieceScripts[3].FloorPos[0] && y == WhitePieceScripts[3].FloorPos[1]) CommandPossibilityTile[Count].SetActive(true);
                    else CommandPossibilityTile[Count].SetActive(false);
                }

                Count++;
            }
    }

    /// <summary>
    /// whiteかblackを選択してコストマップを計算
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private IEnumerator CalcCostMap(Turn color, bool WhitePieceMove)
    {
        GetPossibilityArea();
        WhitePieceNum = 0;
        //WhiteDest = new int[2];
        //BlackDest = new int[2];

        //評価点の絶対値の取得に使う
        int abs = 0;
        //絶対値の差を求めるのに使う
        int sub = 0;
        //何番目のゲームオブジェクトかを指定するために使う
        int num = 0;

        int UCN = 0;

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

            if(Mathf.Abs(maxAbs) + Mathf.Abs(minAbs) > sub)sub = Mathf.Abs(maxAbs) + Mathf.Abs(minAbs);

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
            if (!WhitePieceMove)
            {
                //absの格納
                //if(scrs[i].maxCost == abs || scrs[i].minCost == abs)MaxCostPieces.Add(scrs[i]);
                if (Mathf.Abs(scrs[i].maxCost) + Mathf.Abs(scrs[i].minCost) == sub) MaxCostPieces.Add(scrs[i]);
            }
            else if(WhitePieceMove)
            {
                if (i != 3)
                {
                    //absの格納
                    //if(scrs[i].maxCost == abs || scrs[i].minCost == abs)MaxCostPieces.Add(scrs[i]);
                    if (Mathf.Abs(scrs[i].maxCost) + Mathf.Abs(scrs[i].minCost) == sub) MaxCostPieces.Add(scrs[i]);
                }
            }
        }

        //リストの中からランダムに一つピックアップする
        num = UnityEngine.Random.Range(0, MaxCostPieces.Count);

        

        MaxCostPieces[num].UnderCommand = true;

        for (int i = 0; i < 16; i++)
        {
            if (scrs[i].UnderCommand == true)
            {
                UCN = i;
                scrs[i].UnderCommand = false;
            }
        }

        //ターン,コマ,評価点
        Debug.Log($"{color} GameObject{UCN} {MaxCostPieces[num].GetType()} : 評価点{abs}, 移動座標[{scrs[UCN].MaxCostPos[0]},{scrs[UCN].MaxCostPos[1]}]");
        //そのコマのコストマップを出力
        DebugDataLog(MaxCostPieces[num].costMap);

        Destination = scrs[UCN].MaxCostPos;
        if (color == Turn.white)
        {
            WhitePieceNum = UCN;
            WhiteDest = Destination;
        }
        else
        {
            BlackPieceNum = UCN;
            BlackDest = Destination;
        }
        
        LookPT();
        yield return null;
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
    }

    //クリックしたとき実行
    private void PlayerTern()
    {

        (int X, int Y) DestPos = (Destination[0], Destination[1]);
        (int X, int Y) NowPos = (WhitePieceScripts[3].FloorPos[0], WhitePieceScripts[3].FloorPos[1]);
        (int X, int Y) NextPos = (WhitePiece[3].GetComponent<PlayerMove>().FloorPosOutput[0], WhitePiece[3].GetComponent<PlayerMove>().FloorPosOutput[1]);

        //プレイヤーに命令が来ているとき
        //指令に従い動いた
        if (WhitePieceNum == 3 && DestPos.X == NextPos.X && DestPos.Y == NextPos.Y)
        {
            //ここでプレイヤーを動かす
            WhitePiece[3].GetComponent<PlayerMove>().Move0 = true;
            Debug.Log("プレイヤーに命令があり、命令に従い動いた");

            if(Betrayal >= 0.05)Betrayal -= 0.05f;
            else Betrayal = 0;

            //ターンを黒に渡す
            turn = Turn.black;
            //マップ更新
            StartCoroutine(CalcCostMap(Turn.black, WhitePieceTurn));
        }
        //指令は来ていたが無視して別の方向に動いた
        else if(WhitePieceNum == 3 && DestPos.X != NextPos.X && DestPos.Y != NextPos.Y)
        {
            //ここでプレイヤーを動かす
            WhitePiece[3].GetComponent<PlayerMove>().Move0 = true;
            Debug.Log("プレイヤーに命令があったが、命令に背いて動いた");

            if(Betrayal <= 1)Betrayal += 0.1f;
            else Betrayal = 1;
            VisualManager.GetComponent<VisualManager>().Damage = true;

            //ターンを黒に渡す
            turn = Turn.black;
            //マップ更新
            StartCoroutine(CalcCostMap(Turn.black, WhitePieceTurn));
        }
        //指令に違反し動かなかった
        else if (WhitePieceNum == 3 && NowPos.X == NextPos.X && NowPos.Y == NextPos.Y)
        {
            //ここでプレイヤーを動かす（上下運動）
            WhitePiece[3].GetComponent<PlayerMove>().Move0 = true;
            Debug.Log("プレイヤーに命令があったが、命令に背いて動かなかった");

            if(Betrayal <= 1)Betrayal += 0.1f;
            else Betrayal = 1;
            VisualManager.GetComponent<VisualManager>().Damage = true;

            turn = Turn.white;
            WhitePieceTurn = true;
            //マップ更新
            StartCoroutine(CalcCostMap(Turn.white, WhitePieceTurn));
        }
        //プレイヤーは待機の命令が来ているとき
        //指令に従い動かなかった
        else if (WhitePieceNum != 3 && NowPos.X == NextPos.X && NowPos.Y == NextPos.Y)
        {
            //ここでプレイヤーを動かす（上下運動）
            WhitePiece[3].GetComponent<PlayerMove>().Move0 = true;
            Debug.Log("プレイヤーに命令がなく、命令に従い動かなかった");

            if(Betrayal >= 0.05)Betrayal -= 0.05f;
            else Betrayal = 0;

            WhitePieceTurn = true;
        }
        //指令に違反し動いた
        else if (WhitePieceNum != 3 && PlayerPossibility[NextPos.X,NextPos.Y] >= 1)
        {
            //ここでプレイヤーを動かす
            WhitePiece[3].GetComponent<PlayerMove>().Move0 = true;
            Debug.Log("プレイヤーに命令がなく、命令に背いて動いた");

            if(Betrayal <= 1)Betrayal += 0.1f;
            else Betrayal = 1;
            VisualManager.GetComponent<VisualManager>().Damage = true;

            //マップ更新
            StartCoroutine(CalcCostMap(Turn.black, WhitePieceTurn));
            //ターンを黒に渡す
            turn = Turn.black;
        }
    }

    private void WhiteTurn()
    {
        //ここで白の駒を動かす
        WhitePiece[WhitePieceNum].GetComponent<PieceMove>().Move0 = true;
        WhitePiece[WhitePieceNum].GetComponent<PieceMove>().InputTilePosForPieceMove = Destination;
        Debug.Log("白の駒が動いた");

        //マップ更新
        StartCoroutine(CalcCostMap(Turn.black, WhitePieceTurn));
        //ターンを黒に渡す
        turn = Turn.black;

        WhitePieceTurn = false;
    }

    private void BlackTurn()
    {
        //ここで黒の駒を動かす
        BlackPiece[BlackPieceNum].GetComponent<PieceMove>().Move0 = true;
        BlackPiece[BlackPieceNum].GetComponent<PieceMove>().InputTilePosForPieceMove = Destination;
        
        //マップ更新
        StartCoroutine(CalcCostMap(Turn.white, WhitePieceTurn));
        //ターンを白に渡す
        turn = Turn.white;
    }

    private void TurnControl()
    {
        if(ClickCheck && !NoClick)
        {
            if(!FirstClick)
            {
                turn = Turn.white;
                FloorDataUpdate();
                GetPossibilityArea();
                StartCoroutine(CalcCostMap(turn, WhitePieceTurn));
                LookPT();
                FirstClick = true;
            }
            else
            {
                if (turn == Turn.white && !WhitePieceTurn)
                {
                    PlayerTern();
                    for (int i = 0; i < 16; i++)
                    {
                        if (WhitePiece[3].GetComponent<PlayerMove>().FloorPosOutput[0] == BlackPieceScripts[i].FloorPos[0] && WhitePiece[3].GetComponent<PlayerMove>().FloorPosOutput[1] == BlackPieceScripts[i].FloorPos[1])
                        {
                            BlackPiece[i].GetComponent<Destroy>().DoDestroy = true;
                        }
                        else if (i != 3 && WhitePiece[3].GetComponent<PlayerMove>().FloorPosOutput[0] == WhitePieceScripts[i].FloorPos[0] && WhitePiece[3].GetComponent<PlayerMove>().FloorPosOutput[1] == WhitePieceScripts[i].FloorPos[1])
                        {
                            WhitePiece[i].GetComponent<Destroy>().DoDestroy = true;
                            Betrayal += 1f;
                            VisualManager.GetComponent<VisualManager>().Damage = true;
                        }
                    }
                }
                else if (turn == Turn.white && WhitePieceTurn)
                {
                    WhiteTurn();
                    for (int i = 0; i < 16; i++)
                    {
                        if (WhiteDest[0] == BlackPieceScripts[i].FloorPos[0] && WhiteDest[1] == BlackPieceScripts[i].FloorPos[1])
                        {
                            BlackPiece[i].GetComponent<Destroy>().DoDestroy = true;
                        }
                    }
                }
                else if (turn == Turn.black)
                {
                    BlackTurn();
                    for (int i = 0; i < 16; i++)
                    {
                        if (BlackDest[0] == WhitePieceScripts[i].FloorPos[0] && BlackDest[1] == WhitePieceScripts[i].FloorPos[1])
                        {
                            WhitePiece[i].GetComponent<Destroy>().DoDestroy = true;
                        }
                    }
                }
                NoClick = true;
            }
            ClickCheck = false;
        }

        if(WhitePieceNum != 3 && WhitePiece[WhitePieceNum].GetComponent<PieceMove>().MoveEnd)
        {
            LookPT();
            NoClick = false;
            WhitePiece[WhitePieceNum].GetComponent<PieceMove>().MoveEnd = false;
        }
        else if(WhitePiece[3].GetComponent<PlayerMove>().MoveEnd)
        {
            LookPT();
            NoClick = false;
            WhitePiece[3].GetComponent<PlayerMove>().MoveEnd = false;
        }
        else if(BlackPiece[BlackPieceNum].GetComponent<PieceMove>().MoveEnd)
        {
            LookPT();
            NoClick = false;
            BlackPiece[BlackPieceNum].GetComponent<PieceMove>().MoveEnd = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Betrayal = 0;

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
        //StartCoroutine(CalcCostMap(Turn.white, WhitePieceTurn));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
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
            //LookPT();
            
            //移動させるべきコマをデバッグログに表示
            StartCoroutine(CalcCostMap(Turn.white, WhitePieceTurn));
            //StartCoroutine(CalcCostMap(Turn.black, WhitePieceTurn));


            //DoUpdate = false;
        }
        */
        //DoCrick();
        TurnControl();
        VisualManager.GetComponent<VisualManager>().Betrayal = Betrayal;
    }
}
