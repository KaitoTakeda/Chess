using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int PieceID;
    public int PieceType;

    public GameObject PieceObj;
    public int[] FloorPos = new int[2] { 0, 0 };
    //自分の移動候補のコストマップ
    //一番コストが高い場所に移動しようとする
    public int[,] costMap = new int[8, 8];
    public int pieceCost = 1;
    public int maxCost { get; private set; }
    public int minCost { get; private set; }

    public int[] MaxCostPos = new int[2] { 0, 0 };

    public bool UnderCommand = false;

    //移動可能な範囲を検出
    public int CheckPos(int x,int y,int[,] FloorPieceData)
    {
        //移動不可 -1
        //移動可能だがそれ以上のマスを更新しない 0
        //移動可能 1
        
        //盤面の中であること
        if(x < 0 || x > 7)return -1;
        if(y < 0 || y > 7)return -1;

        /*
        if(PieceType == 10)
        {
            if(FloorPieceData[x,y] == 0)return 1;
            else return 0;
        }
        */
        
        //味方がいる場所
        if(FloorPieceData[x,y]*PieceType > 0)return -1;
        //敵がいる場所
        else if(FloorPieceData[x,y]*PieceType < 0)return 0;

        //それ以外
        return 1;
    }

    //プレイヤーポーンの移動可能範囲を検出
    public int PlayerCheckPos(int x,int y,int[,] FloorPieceData)
    {
        //移動不可 -1
        //移動可能だがそれ以上のマスを更新しない 0
        //移動可能 1
        
        //盤面の中であること
        if(x < 0 || x > 7)return -1;
        if(y < 0 || y > 7)return -1;

        //何も無い場所
        if(FloorPieceData[x,y] == 0)return 1;
        //それ以外
        else return 0;
    }

    //コマ毎の移動可能範囲の設定、それぞれのスクリプトでオーバーライド
    public virtual int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        return data;
    }

    //プレイヤーポーンの移動可能範囲の設定、プレイヤーポーンのスクリプトでオーバーライド、ゲームAIでは読み込まない
    public virtual int[,] PlayerPawnCanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        return data;
    }

    //eneMapは敵の行動予測マップ
    //FloorPieceDataは現在のマップ情報
    public IEnumerator CalcMyCostMap(int[,] eneMap, int[,] FloorPieceData)
    {
        int[,] myMoveMap = CanMove(FloorPieceData);

        //最も得をするか最も危険かのどちらかで動かすため、絶対値が大きい値(最大値と最小値)を取得する
        maxCost = -1000;
        minCost = 1000;
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
            {
                //自分の移動先にある敵のコマを判別
                costMap[i, j] = FloorPieceData[i, j] * myMoveMap[i, j];
                //敵のコマ毎の重みづけ
                if (costMap[i, j] != 0)
                    switch (Math.Abs(costMap[i, j]))
                    {
                        //キングは10倍
                        case 6: costMap[i, j] *= 10; break;
                        //プレイヤーは普通のポーンと同じ点数に調整
                        case 10: costMap[i, j] /= 10; break;
                        default : break;
                    }
                
                //負の値だったら正の値に反転させる
                if(costMap[i,j] < 0) costMap[i, j] *= -1;
                costMap[i, j] += myMoveMap[i, j] - eneMap[i,j] * pieceCost;
                //if(costMap[i,j] == 0) costMap[i, j] = -1;

                //最大値
                if(costMap[i,j] > maxCost) 
                {
                    MaxCostPos = new int[2] { i, j };
                    maxCost = costMap[i, j];
                }
                //最小値
                if(costMap[i,j] < minCost && i == FloorPos[0] && j == FloorPos[1]) minCost = costMap[i, j];
            }
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        PieceObj = this.gameObject;
        //このコマがあるマスの検出
        //x 0~7
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.x/10)) + 4;
        //y 0~7
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.z/10)) + 4;
    }

    // Update is called once per frame
    void Update()
    {
        //このコマがあるマスの検出
        //x 0~7
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.x/10)) + 4;
        //x 0~7
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.z/10)) + 4;
    }
}
