using System.Collections.Generic;
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
    public int FishCount;
    public List<GetFish> GetFishList = new();
    public void HitCollider()
    {
        
    }
    public void netHit()
    {
        //foreach (FishData fish in GetFishList)
        //{
          //  if (fish.name == name) {
        //    foreach () {
        //            GetFishList.Add(fish.name,fish.size);
        //        //ネットの捕獲範囲に魚がいるか大体体の8/10が入っていたらカウントする

        //        //魚がいる場合はその範囲にいる魚の大きさと数記録する
        //        //GetFishList.AddRange(fishpoint);

        //        //網の中にいる魚をpoolに返す
             //}
             //   break;
        //}


    }

}
