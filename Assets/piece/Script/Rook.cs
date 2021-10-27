using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        bool f, b, r, l;
        f = b = r = l = false;

        for (int i = 1; i < 8; i++)
        {
            int Front = CheckPos(FloorPos[0], FloorPos[1] + i, FloorPieceData);
            int Back = CheckPos(FloorPos[0], FloorPos[1] - i, FloorPieceData);
            int Right = CheckPos(FloorPos[0] + i, FloorPos[1], FloorPieceData);
            int Left = CheckPos(FloorPos[0] - i, FloorPos[1], FloorPieceData);

            if(Front >= 0 && !f)data[FloorPos[0], FloorPos[1] + i] = 1;
            if(Back >= 0 && !b)data[FloorPos[0], FloorPos[1] - i] = 1;
            if(Right >= 0 && !r)data[FloorPos[0] + i, FloorPos[1]] = 1;
            if(Left >= 0 && !l)data[FloorPos[0] - i, FloorPos[1]] = 1;

            if(Front <= 0)f = true;
            if(Back <= 0)b = true;
            if(Right <= 0)r = true;
            if(Left <= 0)l = true;
            
            if(f && b && r && l)break;
        }

        return data;
    }
}
