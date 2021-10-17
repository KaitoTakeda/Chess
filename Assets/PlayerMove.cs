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

    private bool Move0 = false;
    private bool Move1 = false;
    private bool Move2 = false;

    public int[] FloorPos = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        PlayerPos = Player.transform.position;
        PrePlayerPos = Player.transform.position;
        FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(PlayerPos.x/10)) + 4;
        FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(PlayerPos.z/10)) + 4;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
        RayPos = Input.mousePosition;
        RayPos.z = 10;
        //RayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RayPos.y = 0;
        //this.transform.position = Camera.main.ScreenToWorldPoint(RayPos);

        /*
        if(Physics.Raycast(Camera.main.transform.position, (Camera.main.ScreenToWorldPoint(RayPos) - Camera.main.transform.position), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, (Camera.main.ScreenToWorldPoint(RayPos) - Camera.main.transform.position) * hit.distance, Color.red);
        }
        */

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red);
        }

        if (Move0 == false && Move1 == false && Move2 == false && Input.GetMouseButtonDown(0))
        {
            if(hit.collider.CompareTag("Floor"))
            {
                //PrePlayerPos = new Vector3(hit.point.x, 0.5f, hit.point.z);
                PrePlayerPos.x = (Mathf.Floor(hit.point.x/10) + 0.5f)*10;
                PrePlayerPos.y = 0.5f;
                PrePlayerPos.z = (Mathf.Floor(hit.point.z/10) + 0.5f)*10;
                FloorPos[0] = Mathf.FloorToInt(Mathf.Floor(hit.point.x/10)) + 4;
                FloorPos[1] = Mathf.FloorToInt(Mathf.Floor(hit.point.z/10)) + 4;
                Move0 = true;
            }
        }

        if(Move0 == true && Move1 == false && Move2 == false)
        {
            PlayerPos.y = Mathf.Lerp(PlayerPos.y, 2, 0.1f);
            if(PlayerPos.y >= 1.95f)
            {
                Move1 = true;
                Move0 = false;
            }
        }
        else if(Move0 == false && Move1 == true && Move2 == false)
        {
            PlayerPos.x = Mathf.Lerp(PlayerPos.x, PrePlayerPos.x, 0.01f);
            PlayerPos.z = Mathf.Lerp(PlayerPos.z, PrePlayerPos.z, 0.01f);
            if(PlayerPos.x >= PrePlayerPos.x-0.05f && PlayerPos.x <= PrePlayerPos.x+0.05f && PlayerPos.z >= PrePlayerPos.z-0.05f && PlayerPos.z <= PrePlayerPos.z+0.05f)
            {
                Move2 = true;
                Move1 = false;
            }
        }
        else if(Move0 == false && Move1 == false && Move2 == true)
        {
            PlayerPos.y = Mathf.Lerp(PlayerPos.y, 0.5f, 0.1f);
            if(PlayerPos.y <= 0.55f)
            {
                Move2 = false;
            }
        }
        
        /*
        if(Move0 == true && Move1 == false)
        {
            PlayerPos.y = Mathf.MoveTowards(PlayerPos.y, 2, 0.05f);
            if(PlayerPos.y >= 1.95f)
            {
                Move1 = true;
                Move0 = false;
            }
        }
        else if(Move0 == false && Move1 == true)
        {
            PlayerPos.y = Mathf.MoveTowards(PlayerPos.y, 0.5f, 0.05f);
            if(PlayerPos.y <= 0.55f)
            {
                Move2 = false;
            }
        }
        */

        //PlayerPos.x = Mathf.MoveTowards(PlayerPos.x, PrePlayerPos.x, 0.025f);
        //PlayerPos.z = Mathf.MoveTowards(PlayerPos.z, PrePlayerPos.z, 0.025f);
        Player.transform.position = PlayerPos;
    }
}
