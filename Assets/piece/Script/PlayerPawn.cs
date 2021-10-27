using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Piece
{
    public override int[,] CanMove(int[,] FloorPieceData)
    {
        var data = new int[8,8];
        int Front = CheckPos(FloorPos[0], FloorPos[1] + 1, FloorPieceData);
        int Back = CheckPos(FloorPos[0], FloorPos[1] - 1, FloorPieceData);
        int Right = CheckPos(FloorPos[0] + 1, FloorPos[1], FloorPieceData);
        int Left = CheckPos(FloorPos[0] - 1, FloorPos[1], FloorPieceData);
        if(Front >= 0)data[FloorPos[0], FloorPos[1] + 1] = 1;
        if(Back >= 0)data[FloorPos[0], FloorPos[1] - 1] = 1;
        if(Right >= 0)data[FloorPos[0] + 1, FloorPos[1]] = 1;
        if(Left >= 0)data[FloorPos[0] - 1, FloorPos[1]] = 1;
        return data;
    }
}
