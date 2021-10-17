using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int PieceID;
    public int PieceType;
    
    public GameObject PieceObj;
    public int[] FloorPos = new int[2]{0,0};
    
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
