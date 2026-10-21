using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{
    public static FishPool Instance { get; private set; }

    public int poolSize = 20;                       // 事前に用意する数
    private readonly Queue<FishController> pool = new Queue<FishController>();

    void Awake()
    {
        Instance = this;

        // あらかじめ魚を作って非表示にしておく
        for (int i = 0; i < poolSize; i++)
        {
            pool.Enqueue(CreateFish());
        }
    }

    private FishController CreateFish()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Capsule); // Unity標準
        Destroy(go.GetComponent<Collider>());                       // Collider不使用
        go.transform.SetParent(transform);

        var fish = go.AddComponent<FishController>();
        go.SetActive(false);
        return fish;
    }

    // Poolから呼び出す(位置・進む方向・消える範囲を渡す)
    public FishController Get(FishSize size, Vector3 position, Vector3 dir, Rect despawnArea)
    {
        var fish = pool.Count > 0 ? pool.Dequeue() : CreateFish();

        fish.Setup(size, Random.Range(1f, 2f) * (1f + 0.5f * (int)size), dir, despawnArea);
        fish.transform.position = position;
        fish.gameObject.SetActive(true);
        return fish;
    }

    // Poolに戻す
    public void Return(FishController fish)
    {
        if (fish == null || !fish.gameObject.activeSelf) return; // 二重返却防止

        fish.gameObject.SetActive(false);
        pool.Enqueue(fish);
    }
}
//using System.Collections.Generic;
//using UnityEngine;


//public class FishPool : MonoBehaviour
//{
//    //public GameObject Prefab;
//    [SerializeField]public int poolSize;
//    [SerializeField]public List<GameObject> pool;
//    //pool =new List<GameObject>();
//    private void Awake()
//    {
//        for (int i = 0; i < poolSize; i++)
//        {
//            GameObject obj = Instantiate(pool[i]);
//            obj.SetActive(false);
//            //pool.Add(obj);
//        }
//    }
//    public GameObject GetObject(Vector3 pos)
//    {
//        foreach(var obj in pool)
//        {
//            if(!obj.activeInHierarchy)
//            {
//                obj.transform.position = pos;
//                obj.SetActive(true);
//                return obj;
//            }
//        }
//        return null;
//    }
//    public void ReturnObject(GameObject obj)
//    {
//        obj.gameObject.SetActive(false);
//    }

//}
