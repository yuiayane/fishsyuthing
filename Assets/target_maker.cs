using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class target_maker : MonoBehaviour
{
    [SerializeField] private int NetCount;
    [SerializeField] private int ShotCount;
    //[SerializeField] private Canvas canvas;
    [SerializeField] private GameObject marke;
    //[SerializeField] private RectTransform marke;
    [SerializeField] private float followSpeed;
    
    [SerializeField] private float NetSpeed;
    [SerializeField] private List<GameObject> Net = new();

    private List <FishData> GetFishList = new ();

    public Vector3 TragetMoker()
    {
        Vector3 pos =transform.position;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        //mousePos.x= 
        Vector3 tragetPos = Camera.main.ScreenToWorldPoint(new Vector3( mousePos.x, mousePos.y,mousePos.z));
       
        marke.transform.position = tragetPos;
        if (Input.GetKeyDown(KeyCode.UpArrow) /*&& NetCount > 0*/ /*&& Net[1]==false*/)
        {
            Vector3 pointPos = marke.transform.position;
            NetCount -= 1;
            Shot(pointPos);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) /*&& ShotCount<NetCount*/ /*&& Net[1]==true*/)
        {
            Saluvaji();

        }
        return mousePos;

    }
    public void Shot(Vector3 Pos)
    {
        Net[1].gameObject.SetActive(true);
        Net[1].transform.position = new Vector3(Pos.x,Pos.y,Pos.z);
            //Vector3.Lerp(Pos, worldPos, NetSpeed * Time.deltaTime);
        marke.gameObject.SetActive(false);
    }
    public void Saluvaji()
    {
        float SaluvajiSpeed = 10f;
        //Net[1].transform.position = Vector3.Lerp(NetPos, worldPos, SaluvajiSpeed * Time.deltaTime);
        Net[1].gameObject.SetActive(false);
        marke.gameObject.SetActive(true);
        //回収

    }
    //スピード
    public void NetRturn()
    {
        gameObject.SetActive(false);

        //魚の数を受け取るデータを受け取る
        //if(GetFhisList==Null) return;

        //網の中の魚の大きさと数を分ける
        //for(int i = 0; i <fishCount; i++)
        //{

        //ポイント加算
        //SunmFishPoint=GetFishPoint+FishPoint;
        //return;
        //}

    }
    //戻す
    //if(GetMet.Pos==Get.Pos)
    //{


    //MaxFishCount +=SumFishPoint

    //網をfalesにする

    public void Update()
    {


        TragetMoker();
        


    }
}


