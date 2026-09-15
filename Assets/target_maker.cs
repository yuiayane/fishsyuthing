//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;


public class target_maker : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject marke;
    //[SerializeField] private RectTransform maker;
    [SerializeField] private float followSpeed;
    Vector3 mousePos, worldPos;

    public Vector3 TragetMoker()
    {

        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z));
        //mousePos.x=Mathf.Clamp(transform.position.x, 10.0f, 20.0f);
        //mousePos.y=Mathf.Clamp(transform.position.y, 10.0f, 10.0f);
        marke.transform.position = Vector3.Lerp(mousePos, worldPos, followSpeed * Time.deltaTime);
        Vector3 pos = marke.transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 tragetPos = new Vector3(pos.x, 0, pos.z);
            Shot(tragetPos);
        }
        else if(Input.GetMouseButtonDown(1)) 
        {
        
            Saluvaji();
        }
        return mousePos;
    }
    public void Shot(Vector3 Pos)
    {
        //オブジェクトプールで管理されている網を呼び出す
        //net.transform.position = ;
        //呼び出した網を可視化する
        // =ture
        //飛んでいくスピードの計算
        //可視化した網をターゲットマーカーで発射ボタンを押された場所に飛んでいく
        transform.position = Pos;
        
    }
    public void Saluvaji()
    {

        //スピード
        //SaluvajiSpeed=;
        //transformPosition.
        //戻す
        //回収
        //if(GetMet.Pos==Get.Pos)
        //{
        
        //魚の数を受け取るデータを受け取る
        //if(GetFhisList==Null) return;
 
        //網の中の魚の大きさと数を分ける
        //for(int i = 0; i <fishCount; i++)
        //{

        //ポイント加算
        //SunmFishPoint=GetFishPoint+FishPoint;
        //return;
        //}
        //MaxFishCount +=SumFishPoint

        //網をfalesにする
        //(fales)

    }

    void Update()
    {
    
        Vector3 pos=TragetMoker();
 
    }

}
