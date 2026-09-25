using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class f_Sammon : MonoBehaviour
{
    [SerializeField] private float samonEria = 15f;

    [SerializeField]
    float spawnInterval = 1f;

    float timer;
    public FishPool pool;
    //Fish fish = pool.Get();

    //pool.Release(fish);
    //fish.transform.position = SpawnPoint();
    //fish.SetActive(true);

    Vector3 SpawnPoint()
    {
        float x = Random.Range(-45f, 45f);

        float z = Random.Range(-45f, 45f);
        //switch (Random.Range(0, 5))
        //{

        //    case 0: x = -45f; break;

        //    case 1: x = 45f; break;
        //    case 2: z = -45f; break;
        //    case 3: z = 45f; break;
        //        default:
        //        break;
        //}

        return new Vector3(x, 0, z);
    }
    public void SpawnFish(Vector3 pos)
    {
        pool.GetObject(pos);

    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Vector3 pos=SpawnPoint();
            SpawnFish(pos);
        }
    }
    public void Spotn(Vector3 pod, int dammonFish)
    {

        float spornEria = Random.Range(dammonFish, samonEria);
        float dir = Random.Range(0, 360);
        Vector3 Distanc = new Vector3(20, 1, 10);
    }

}
