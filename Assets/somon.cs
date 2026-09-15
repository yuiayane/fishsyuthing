using UnityEngine;

public class FishPool : MonoBehaviour
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
        float x = Random.Range(-50f, 50f);

        float z = Random.Range(-50f, 50f);
        switch (Random.Range(0, 4))
        {

            case 0: x = -50f; break;

            case 1: x = 50f; break;
            case 2: z = -50f; break;
            case 3: z = 50f; break;
        }

        return new Vector3(x, 0, z);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            //SpawnFish();
        }
    }
    public void Spotn(Vector3 pod, int dammonFish)
    {

        float spornEria = Random.Range(dammonFish, samonEria);
        float dir = Random.Range(0, 360);
        Vector3 Distanc = new Vector3(20, 1, 10);
    }

}
