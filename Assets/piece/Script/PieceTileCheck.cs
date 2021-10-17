using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTileCheck : MonoBehaviour
{
    public GameObject Piece;
    public int[] FloorPos = new int[2];

    
    // Start is called before the first frame update
    void Start()
    {
        Piece = this.gameObject;
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(Piece.transform.position.x/10)) + 4;
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(Piece.transform.position.z/10)) + 4;
    }

    // Update is called once per frame
    void Update()
    {
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(Piece.transform.position.x/10)) + 4;
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(Piece.transform.position.z/10)) + 4;
    }
}
