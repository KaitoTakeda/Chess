using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int PieceID;
    public int PieceType;
    
    public GameObject PieceObj;
    public int[] FloorPos = new int[2]{0,0};

    public int CheckPos(int x,int y,int[,] FloorPieceData)
    {
        //移動不可 -1
        //移動可能だがそれ以上のマスを更新しない 0
        //移動可能 1
        
        if(x < 0 || x > 7)return -1;
        if(y < 0 || y > 7)return -1;

        if(PieceType == 10)
        {
            if(FloorPieceData[x,y] == 0)return 1;
            else return 0;
        }

        if(FloorPieceData[x,y]*PieceType > 0)return -1;
        else if(FloorPieceData[x,y]*PieceType < 0)return 0;

        return 1;
    }

    public virtual int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        return data;
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
