using System.Collections.Generic;
using UnityEngine;


public struct F_Data
{
    public string fishName;
    public float size;
    public float point;
    public string f_class;
}
public class NetHitCollider
{
    public float distance;
    public float dir;
    
}
public class GetFish
{
    public string name;
    public float size;
    public float point;
    public string s_Class;

}


public class GetCollider : MonoBehaviour
{
    FishPool fishPool;
    public Collider FishGetCollider;
    public int FishCount;
    public Transform[] Fishs;
    public List<GetFish> GetFishList = new();
    public List<FishData> fishdataList = new();
    public float radius1 = 1.0f;
    public float radius2 = 1.0f;
    public void HitCollider(Collider other)
    {
        Collider[] hitColliders = Physics.OverlapBox(FishGetCollider.bounds.center,
            FishGetCollider.bounds.extents, FishGetCollider.transform.rotation);
        foreach (Collider fishCollider in hitColliders)
        {
            if (fishCollider.GetComponent("Fish"))
            {
                float hitArea = CalculateOverlapRatio(FishGetCollider, fishCollider);
                if (hitArea >= 0.8)
                {
                    FishData fish = other.GetComponent<FishData>();
                    //GetFish Fish = NetHit(fish, fishCollider);
                }
                Vector3 direction = GetComponent<Collider>().transform.position - transform.position;
                float distanceSqr = direction.sqrMagnitude;
                float totalTadiusSqr = (radius1 + radius2) * (radius1 + radius2);
            }
        }
    }
    float CalculateOverlapRatio(Collider get, Collider fish)
    {
        Bounds getBounds = get.bounds;
        Bounds fishBounds = fish.bounds;
        if (!getBounds.Intersects(fish.bounds))
            return 0.0f;
        Bounds overlapBounds = new Bounds();
        overlapBounds.center = getBounds.center;
        overlapBounds.extents = getBounds.extents;

        float overlapVolume = overlapBounds.size.x * overlapBounds.size.y * overlapBounds.size.z;
        float fishVolume = fishBounds.size.x * fishBounds.size.y * fishBounds.size.z;
        return overlapVolume / fishVolume;
    }
    public GetFish NetHit(F_Data data,Collider obuject)
    {
        if(data.fishName == null)
        {
            GetFish fish = new GetFish();
            Debug.Log($"GetFishName:{fish.name}");
            Debug.Log($"GetFishSize:{fish.size}");
            Debug.Log($"GetFishPoint:{fish.point}");
            Debug.Log($"GetFishClass:{fish.s_Class}");
            GetFishList.Add(fish);
            fishPool.ReturnObject(obuject.gameObject);
            return fish;
        }
        return null;
    }

    //FishPool.ReturnObject(fish.object);
    //    foreach (FishData fish in Fishs)
    //    {
    //        if (fish.name == name)
    //        {
    //            //    foreach () {
    //            //            GetFishList.Add(fish.name,fish.size);

    //            //GetFishList.AddRange(fishpoint);
    //            //ObjectPool.RetrunObject();

    //        } 
    //}

}


