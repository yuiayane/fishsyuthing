using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{
    //public GameObject Prefab;
    [SerializeField]public int poolSize;
    [SerializeField]private List<GameObject> pool;
    private void Awake()
    {
        pool =new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject gameoabject = Instantiate(pool[i]);
            GameObject obj = gameoabject;
            obj.SetActive(false);
            pool.Add(obj);
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
        //GameObject newObj =Instantiate(Prefab);
        //newObj.SetActive(true);
        //return newObj;
        return null;
    }
    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

}
