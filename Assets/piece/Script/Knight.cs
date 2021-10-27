using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];

        int FR = CheckPos(FloorPos[0] + 1, FloorPos[1] + 2, FloorPieceData);
        int FL = CheckPos(FloorPos[0] - 1, FloorPos[1] + 2, FloorPieceData);
        int BR = CheckPos(FloorPos[0] + 1, FloorPos[1] - 2, FloorPieceData);
        int BL = CheckPos(FloorPos[0] - 1, FloorPos[1] - 2, FloorPieceData);
        int RF = CheckPos(FloorPos[0] + 2, FloorPos[1] + 1, FloorPieceData);
        int RB = CheckPos(FloorPos[0] + 2, FloorPos[1] - 1, FloorPieceData);
        int LF = CheckPos(FloorPos[0] - 2, FloorPos[1] + 1, FloorPieceData);
        int LB = CheckPos(FloorPos[0] - 2, FloorPos[1] - 1, FloorPieceData);

        if(FR >= 0)data[FloorPos[0] + 1, FloorPos[1] + 2] = 1;
        if(FL >= 0)data[FloorPos[0] - 1, FloorPos[1] + 2] = 1;
        if(BR >= 0)data[FloorPos[0] + 1, FloorPos[1] - 2] = 1;
        if(BL >= 0)data[FloorPos[0] - 1, FloorPos[1] - 2] = 1;
        if(RF >= 0)data[FloorPos[0] + 2, FloorPos[1] + 1] = 1;
        if(RB >= 0)data[FloorPos[0] + 2, FloorPos[1] - 1] = 1;
        if(LF >= 0)data[FloorPos[0] - 2, FloorPos[1] + 1] = 1;
        if(LB >= 0)data[FloorPos[0] - 2, FloorPos[1] - 1] = 1;
        
        return data;
    }
}
