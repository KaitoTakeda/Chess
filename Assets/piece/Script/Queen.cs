using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        bool f, b, r, l, fr, fl, br, bl;
        f = b = r = l = fr = fl = br = bl = false;

        for (int i = 1; i < 8; i++)
        {
            int Front = CheckPos(FloorPos[0], FloorPos[1] + i, FloorPieceData);
            int Back = CheckPos(FloorPos[0], FloorPos[1] - i, FloorPieceData);
            int Right = CheckPos(FloorPos[0] + i, FloorPos[1], FloorPieceData);
            int Left = CheckPos(FloorPos[0] - i, FloorPos[1], FloorPieceData);
            int FrontRight = CheckPos(FloorPos[0] + i, FloorPos[1] + i, FloorPieceData);
            int FrontLeft = CheckPos(FloorPos[0] - i, FloorPos[1] + i, FloorPieceData);
            int BackRight = CheckPos(FloorPos[0] + i, FloorPos[1] - i, FloorPieceData);
            int BackLeft = CheckPos(FloorPos[0] - i, FloorPos[1] - i, FloorPieceData);

            if(Front >= 0 && !f)data[FloorPos[0], FloorPos[1] + i] = 1;
            if(Back >= 0 && !b)data[FloorPos[0], FloorPos[1] - i] = 1;
            if(Right >= 0 && !r)data[FloorPos[0] + i, FloorPos[1]] = 1;
            if(Left >= 0 && !l)data[FloorPos[0] - i, FloorPos[1]] = 1;
            if(FrontRight >= 0 && !fr)data[FloorPos[0] + i, FloorPos[1] + i] = 1;
            if(FrontLeft >= 0 && !fl)data[FloorPos[0] - i, FloorPos[1] + i] = 1;
            if(BackRight >= 0 && !br)data[FloorPos[0] + i, FloorPos[1] - i] = 1;
            if(BackLeft >= 0 && !bl)data[FloorPos[0] - i, FloorPos[1] - i] = 1;

            if(Front <= 0)f = true;
            if(Back <= 0)b = true;
            if(Right <= 0)r = true;
            if(Left <= 0)l = true;
            if(FrontRight <= 0)fr = true;
            if(FrontLeft <= 0)fl = true;
            if(BackRight <= 0)br = true;
            if(BackLeft <= 0)bl = true;
            
            if(f && b && r && l && fr && fl && br && bl)break;
        }
        
        return data;
    }
}
