using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8, 8];

        for(int x = -1; x <= 1; x++)
        for (int y = -1; y <= 1; y++)
        {
            int Check = CheckPos(FloorPos[0] + x, FloorPos[1] + y, FloorPieceData);
            if(Check >= 0)data[FloorPos[0] + x, FloorPos[1] + y] = 1;
        }

        return data;
    }
    
}
