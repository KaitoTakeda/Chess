using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        bool fr, fl, br, bl;
        fr = fl = br = bl = false;

        for (int i = 1; i < 8; i++)
        {
            int FrontRight = CheckPos(FloorPos[0] + i, FloorPos[1] + i, FloorPieceData);
            int FrontLeft = CheckPos(FloorPos[0] - i, FloorPos[1] + i, FloorPieceData);
            int BackRight = CheckPos(FloorPos[0] + i, FloorPos[1] - i, FloorPieceData);
            int BackLeft = CheckPos(FloorPos[0] - i, FloorPos[1] - i, FloorPieceData);

            if(FrontRight >= 0 && !fr)data[FloorPos[0] + i, FloorPos[1] + i] = 1;
            if(FrontLeft >= 0 && !fl)data[FloorPos[0] - i, FloorPos[1] + i] = 1;
            if(BackRight >= 0 && !br)data[FloorPos[0] + i, FloorPos[1] - i] = 1;
            if(BackLeft >= 0 && !bl)data[FloorPos[0] - i, FloorPos[1] - i] = 1;

            if(FrontRight <= 0)fr = true;
            if(FrontLeft <= 0)fl = true;
            if(BackRight <= 0)br = true;
            if(BackLeft <= 0)bl = true;
            
            if(fr && fl && br && bl)break;
        }

        return data;
    }
}
