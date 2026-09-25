using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetHitCollider
{
    public float distance;
    public float dir;
    
}
public class GetFish
{
    public string name;
    private float size;
    private float point;

}

public class GetCollider : MonoBehaviour
{
    public Collider FishGetCollider;
    public int FishCount;
    public Transform[] Fishs;
    public List<GetFish> GetFishList = new();
    public List<FishData> fishdataList = new();
    public float radius1 = 1.0f;
    public float radius2 = 1.0f;
    public void HitCollider()
    {
        Collider[] hitColliders = Physics.OverlapBox(FishGetCollider.bounds.center,
            FishGetCollider.bounds.extents, FishGetCollider.transform.rotation);
        foreach(Collider fishCollider in hitColliders)
        {
            if (fishCollider.GetComponent("Fish"))
            {
                float hitArea = CalculateOverlapRatio(FishGetCollider, fishCollider);
                if (hitArea >= 0.8)
                {
                    Debug.Log($"{fishCollider.name}ÇÉQÉbÉgÅI");
                    FishData fish = FishGetCollider.GetComponent<FishData>();
                    if (fish != null)
                    {
                        netHit(fish);
                    }
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
        Bounds fishBounds= fish.bounds;
        if(!getBounds.Intersects(fish.bounds))
            return 0.0f;
        Bounds overlapBounds=new Bounds();
        overlapBounds.center = getBounds.center;
        overlapBounds.extents = getBounds.extents;

        float overlapVolume = overlapBounds.size.x*overlapBounds.size.y*overlapBounds.size.z;
        float fishVolume = fishBounds.size.x*fishBounds.size.y*fishBounds.size.z;
        return overlapVolume/fishVolume;
    }
    public void netHit(FishData data)
    {
        switch (data.f_class)
        {
        //    case c_Greet:
        //        break;
        //    case c_Hipeer
        //:
        //        break;

        }
        //GetFishList.AddRange(data.);
        // FishPool.ReturnObject(fish.object);
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

}
