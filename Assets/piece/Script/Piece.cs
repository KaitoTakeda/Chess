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
        

        if(FloorPieceData[x,y]*PieceType > 0)return -1;
        else if(FloorPieceData[x,y]*PieceType < 0)return 0;

        return 1;
    }

    public virtual int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        return data;
    }

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
                        case 6: costMap[i, j] *= 10; break;
                        case 10: costMap[i, j] /= 10; break;
                        default : break;
                    }
                
                //負の値だったら正の値に反転させる
                if(costMap[i,j] < 0) costMap[i, j] *= -1;
                costMap[i, j] += myMoveMap[i, j] - eneMap[i,j] * pieceCost;
                if(costMap[i,j] == 0) costMap[i, j] = -1;

                

                if(costMap[i,j] > maxCost) maxCost = costMap[i, j];
                if(costMap[i,j] < minCost) minCost = costMap[i, j];
            }
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        PieceObj = this.gameObject;
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.x/10)) + 4;
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.z/10)) + 4;
    }

    // Update is called once per frame
    void Update()
    {
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.x/10)) + 4;
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(PieceObj.transform.position.z/10)) + 4;
    }
}
