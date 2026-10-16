using System.Collections.Generic;
using UnityEngine;


//public class NetHitCollider
//{
//    public float distance;
//    public float dir;
    
//}
public class GetFish
{
    public string name;
    public float size;
    public float point;
    public string s_Class;

}


public class GetCollider : MonoBehaviour
{
    FishPool FishPool;
    public Collider FishGetCollider;
    public List<GetFish> GetFishList = new();
    public void Start()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            g_Fish();
        }
    }
    public void g_Fish()
    {
        Collider[] huts = Physics.OverlapBox(FishGetCollider.bounds.center, FishGetCollider.bounds.extents, FishGetCollider.transform.rotation);
        foreach (var fishCollider in huts)
        {
            Fish fish = fishCollider.GetComponent<Fish>();
            if (fish != null)
            {
                GetFish getFish = new GetFish();
                getFish.name = fish.data.fishName;
                getFish.size = fish.data.size;
                getFish.point = fish.data.point;
                getFish.s_Class = fish.data.f_class;
                Debug.Log($"GetFishName:{getFish.name}");
                Debug.Log($"GetFishSize:{getFish.size}");
                Debug.Log($"GetFishPoint:{getFish.point}");
                Debug.Log($"GetFishClass:{getFish.s_Class}");
                GetFishList.Add(getFish);
                FishPool.ReturnObject(fish.gameObject);
            }
        }
    }
}
