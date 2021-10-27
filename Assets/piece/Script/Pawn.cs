using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        int Step = 1;
        if(PieceType < 0)Step = -1;
        int Check = CheckPos(FloorPos[0], FloorPos[1] + Step, FloorPieceData);
        if(Check >= 0)data[FloorPos[0], FloorPos[1] + Step] = 1;
        return data;
    }
}
