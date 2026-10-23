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


