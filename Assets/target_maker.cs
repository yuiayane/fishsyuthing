using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;


public class target_maker : MonoBehaviour
{
    [SerializeField] private int NetCount;
    [SerializeField] private int ShotCount;
    
    [SerializeField] private GameObject marke;
    
    [SerializeField] private float followSpeed;
    
    [SerializeField] private float NetSpeed;
    [SerializeField] private List<GameObject> Net = new();

    float FishCount;
    private List <FishData> GetFishList = new ();

    public object SummonFishPoint { get; private set; }

    public void TragetMoker()
    {
        Vector3 pos =transform.position;
        Vector3 mousePos = Input.mousePosition;

        Vector3 tragetPos = Camera.main.ScreenToWorldPoint(new Vector3( mousePos.x, mousePos.y,10));
       
        marke.transform.position = tragetPos;
        if (Input.GetKeyDown(KeyCode.UpArrow) /*&& NetCount > 0*/ /*&& Net[1]==false*/)
        {
            Vector3 point = tragetPos ;
            //NetCount -= 1;
            Shot(point);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) /*&& ShotCount<NetCount*/ /*&& Net[1]==true*/)
        {
            Saluvaji(Net[1].transform.position);

        }
       

    }
    public void Shot(Vector3 Pos)
    {
        
        Net[1].gameObject.SetActive(true);
        //Net[1].transform.position = new Vector3(Pos.x,10,Pos.z);
        Net[1].transform.position = new Vector3(Pos.x,Pos.y,Pos.z);
        //Net[1].transform.position= Vector3.Lerp(Pos, , NetSpeed * Time.deltaTime);
        marke.gameObject.SetActive(false);
    }
    public void Saluvaji(Vector3 NetPos )
    {
        float SaluvajiSpeed = 10f;
        Vector3 direcshon= new Vector3(0, 0, 0 );
        Net[1].transform.position = Vector3.Lerp(direcshon, NetPos, SaluvajiSpeed * Time.deltaTime);
        Net[1].gameObject.SetActive(false);
        marke.gameObject.SetActive(true);
        //回収

    }
    //スピード
    public void NetRturn()
    {
        gameObject.SetActive(false);

        //魚の数を受け取るデータを受け取る
        if(GetFishList==null) return;

        //網の中の魚の大きさと数を分ける
        for(int i = 0; i <FishCount; i++)
        {
            if(FishCount>0)
            {
                ////魚の大きさを分ける
                //if (GetFish.Size == Data.name)
                //{

                //}
                //else
                //{
                    
                //}
                    //魚のポイントを分ける
                    //魚の数を分ける
                    //counter.CountSystem(data.FishPoint);
                    FishCount -= 1;
            }
            else
            {
                FishCount=0;
                return;
            }
                //ポイント加算
             //SunmFishPoint=GetFishPoint+FishPoint;

        }

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


