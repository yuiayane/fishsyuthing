using System.Collections.Generic;
using UnityEngine;


public class FishPool : MonoBehaviour
{
    //public GameObject Prefab;
    [SerializeField]public int poolSize;
    [SerializeField]private List<GameObject> pool;
    //pool =new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pool[i]);
            obj.SetActive(false);
            //pool.Add(obj);
        }
    }
    public GameObject GetObject(Vector3 pos)
    {
        foreach(var obj in pool)
        {
            if(!obj.activeInHierarchy)
            {
                obj.transform.position = pos;
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }
    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

}
