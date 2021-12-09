using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //public Camera MainCamera;
    private Vector3 RayPos = Vector3.zero;
    private RaycastHit hit;

    public GameObject Player;
    private Vector3 PlayerPos = Vector3.zero;
    private Vector3 PrePlayerPos = Vector3.zero;

    public bool Move0 = false;
    private bool Move1 = false;
    private bool Move2 = false;
    public bool MoveEnd = false;

    public int[] FloorPos = new int[2];
    public int[] FloorPosOutput = new int[2];

    private int[,] Floor = new int[8, 8];
    private int[,] CanMoveFloor = new int[8, 8];
    private Piece PlayerPawn;
    private GameObject CGM;
    private ChessGameManager ChessGameManager;
    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        PlayerPos = Player.transform.position;
        PrePlayerPos = Player.transform.position;
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(PlayerPos.x/10)) + 4;
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(PlayerPos.z/10)) + 4;

        CGM = GameObject.Find ("ChessGameManager"); 
        ChessGameManager = CGM.GetComponent<ChessGameManager>();
        PlayerPawn = gameObject.GetComponent<Piece>();
    }

    public void DoClick()
    {
        if(Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Floor") && !ChessGameManager.ClickCheck && !ChessGameManager.NoClick)
        {
            FloorPosOutput[0] = Mathf.FloorToInt(Mathf.Floor(hit.point.x/10)) + 4;
            FloorPosOutput[1] = Mathf.FloorToInt(Mathf.Floor(hit.point.z/10)) + 4;

            if(!ChessGameManager.FirstClick)
            {
                ChessGameManager.ClickCheck = true;
            }
            else
            {
                //盤面をアップデート
                ChessGameManager.FloorDataUpdate();
                //取得した盤面の情報をゲームマネージャーから持ってくる
                Floor = ChessGameManager.Floor;
                //移動可能なエリアを取得
                CanMoveFloor = PlayerPawn.PlayerPawnCanMove(Floor);

                PrePlayerPos.x = (Mathf.Floor(hit.point.x/10) + 0.5f)*10;
                PrePlayerPos.y = 0.5f;
                PrePlayerPos.z = (Mathf.Floor(hit.point.z/10) + 0.5f)*10;
                

                if (CanMoveFloor[FloorPos[0], FloorPos[1]] >= 1)
                {
                    ChessGameManager.ClickCheck = true;
                }
            }
            ChessGameManager.NoClick = false;
        }
    }

    private void DoMove0()
    {
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(PlayerPos.x/10)) + 4;
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(PlayerPos.z/10)) + 4;

        PlayerPos.y = Mathf.Lerp(PlayerPos.y, 2, 0.05f);
        if(PlayerPos.y >= 1.95f)
        {
            Move1 = true;
            Move0 = false;
        }
    }

    private void DoMove1()
    {
        PlayerPos.x = Mathf.Lerp(PlayerPos.x, PrePlayerPos.x, 0.025f);
        PlayerPos.z = Mathf.Lerp(PlayerPos.z, PrePlayerPos.z, 0.025f);
        if(PlayerPos.x >= PrePlayerPos.x-0.05f && PlayerPos.x <= PrePlayerPos.x+0.05f && PlayerPos.z >= PrePlayerPos.z-0.05f && PlayerPos.z <= PrePlayerPos.z+0.05f)
        {
            Move2 = true;
            Move1 = false;
        }
    }

    private void DoMove2()
    {
        PlayerPos.y = Mathf.Lerp(PlayerPos.y, 0.5f, 0.05f);
        if(PlayerPos.y <= 0.55f)
        {
            MoveEnd = true;
            Move2 = false;
        }
    }

    void Update()
    {
        
        
        RayPos = Input.mousePosition;
        RayPos.z = 10;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red);
        }

        if(Move0 == true && !Move1 && !Move2)
        {
            DoMove0();
        }
        else if(!Move0 && Move1 == true && !Move2)
        {
            DoMove1();
        }
        else if(!Move0&& !Move1 && Move2 == true)
        {
            DoMove2();
        }
        
        Player.transform.position = PlayerPos;

        DoClick();
    }
}
