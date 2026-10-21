using System.Collections.Generic;
using UnityEngine;

public class FishGet : MonoBehaviour
{
    public static readonly List<FishController> AllFish = new List<FishController>();

    public float getDistance = 1.0f;

    // 網の範囲に入った魚を、網の中(held)にまとめる(まだ得点にしない)
    public void Collect(Vector3 netPos, Transform net, List<FishData> held)
    {
        Vector2 b = new Vector2(netPos.x, netPos.z);

        foreach (var fish in AllFish)
        {
            if (fish.isCaught) continue;

            Vector2 a = new Vector2(fish.transform.position.x, fish.transform.position.z);
            float fishRadius = fish.transform.localScale.x * 0.5f;

            if (Vector2.Distance(a, b) <= getDistance + fishRadius)
            {
                fish.Catch(net);
                held.Add(fish.GetData());
            }
        }
    }

    // サルベージ: まとめて計算して加点 → Poolに戻す
    public void Salvage(List<FishData> held)
    {
        GameManager.Instance.AddCatch(held);

        foreach (var d in held)
        {
            FishPool.Instance.Return(d.source);
        }
    }
}
//using System.Collections.Generic;
//using UnityEditor.SpeedTree.Importer;
//using UnityEngine;


////public class NetHitCollider
////{
////    public float distance;
////    public float dir;

////}
//public class GetFish
//{
//    public string name;
//    public float size;
//    public float point;
//    public string s_Class;

//}


//public class GetCollider : MonoBehaviour
//{
//    //FishPool FishPool;
//    public Collider FishGetCollider;
//    [SerializeField]private F_Data fish;
//    [SerializeField] private float fishDistance=1.5f;
//    public bool IsGetFish { get; private set; } 
//    //public List<GetFish> GetFishList = new();
//    public void Start()
//    {
//        if (fish ==null)
//        {
//            Debug.LogWarning("get:fishが設定されていません");
//        }
//    }
//    private void Update()
//    {
//        if (IsGetFish || fish == null) return;



//        Vector3 diff = fish.transform.position - transform.position;
//        diff.x = 0f;
//        diff.z = 0;
//        while (diff.sqrMagnitude <= fishDistance)
//        {
//            transform .position = Vector3.Lerp(transform.position, fish.transform.position, Time.deltaTime * 5f);
//            if (diff.sqrMagnitude <= fishDistance * fishDistance)
//            {
//                g_Fish();
//            }

//        }
//    }
//    public void g_Fish()
//    {
//        IsGetFish = true;
//        //maker.CanMove=false;
//        Debug.Log("魚を取得しました");


//        //Collider[] huts = Physics.OverlapBox(FishGetCollider.bounds.center, FishGetCollider.bounds.extents, FishGetCollider.transform.rotation);
//        //foreach (var fishCollider in huts)
//        //{
//        //    Fish fish = fishCollider.GetComponent<Fish>();
//        //    if (fish != null)
//        //    {
//        //        GetFish getFish = new GetFish();
//        //        getFish.name = fish.data.fishName;
//        //        getFish.size = fish.data.size;
//        //        getFish.point = fish.data.point;
//        //        getFish.s_Class = fish.data.f_class;
//        //        Debug.Log($"GetFishName:{getFish.name}");
//        //        Debug.Log($"GetFishSize:{getFish.size}");
//        //        Debug.Log($"GetFishPoint:{getFish.point}");
//        //        Debug.Log($"GetFishClass:{getFish.s_Class}");
//        //        GetFishList.Add(getFish);
//        //        FishPool.ReturnObject(fish.gameObject);
//        //    }
//        //}
//    }
//    private void Get_Fish()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube(FishGetCollider.bounds.center, FishGetCollider.bounds.size);
//    }

//}
