using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public Camera cam;
    public int count = 10;              // 常にこの数を維持する
    public float fishHeight = 0f;       // 魚が泳ぐ面の高さ
 
    public float spawnMargin = 1.5f;    // 画面の外側、どれだけ離れて出現するか
    public float despawnMargin = 20f;    // 画面の外側、どこまで行ったら消すか
    public float angleRange = 30f;      // 進む方向のばらつき(度)

    // 大物ほど出にくい(小60% / 中30% / 大10%)
    public float rateSmall = 0.5f, rateMedium = 0.3f, rateLarge=0.2f ;
    [Header("出現タイミング")]
    [Range(0f, 10f)] public float startDelay = 1f;     // ゲーム開始から最初の1匹までの時間
    [Range(0f, 10f)] public float intervalMin = 0.3f;  // 次の魚が出るまでの最短時間
    [Range(0f, 10f)] public float intervalMax = 1.5f;  // 次の魚が出るまでの最長時間

    private float timer;
    private float nextInterval;
    private Rect viewArea;      // 画面に映る範囲(x = X, y = Z)
    private Rect despawnArea;


    void Start()
    {
        if (cam == null) cam = Camera.main;
        CalcArea();
        timer = -startDelay;

    }


    // 画面の四隅を水面に投影して、映っている範囲を求める
    private void CalcArea()
    {
        var plane = new Plane(Vector3.up, new Vector3(0f, fishHeight, 0f));
        float minX = float.MaxValue, maxX = float.MinValue;
        float minZ = float.MaxValue, maxZ = float.MinValue;

        Vector2[] corners = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };
        foreach (var c in corners)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(c.x, c.y, 0f));
            if (!plane.Raycast(ray, out float d)) continue;
            Vector3 p = ray.GetPoint(d);
            minX = Mathf.Min(minX, p.x); maxX = Mathf.Max(maxX, p.x);
            minZ = Mathf.Min(minZ, p.z); maxZ = Mathf.Max(maxZ, p.z);
        }

        viewArea = Rect.MinMaxRect(minX, minZ, maxX, maxZ);
        despawnArea = Rect.MinMaxRect(minX - despawnMargin, minZ - despawnMargin,
                                      maxX + despawnMargin, maxZ + despawnMargin);
    }
    
    void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        // 魚が定数(count)に達している間は、時間を進めない
        if (FishGet.AllFish.Count >= count) return;

        timer += Time.deltaTime;
        if (timer >= nextInterval)
        {
            timer = 0f;
            SpawnOne();

            // 次の出現までの時間は、最短?最長の間でランダムに決まる
            float min = Mathf.Min(intervalMin, intervalMax);
            float max = Mathf.Max(intervalMin, intervalMax);
            nextInterval = Random.Range(min, max);
        }
    }

    private void SpawnOne()
    {
        float r = Random.value;
        FishSize size = r < rateSmall ? FishSize.Small
                      : r < rateSmall + rateMedium ? FishSize.Medium
                      : FishSize.Large ;

        // 画面外の四方向(上・下・左・右)からランダムに選ぶ
        Vector3 pos, baseDir;
        float m = spawnMargin;

        switch (Random.Range(0, 4))
        {
            case 0: // 上から → 下へ
                pos = new Vector3(Random.Range(viewArea.xMin, viewArea.xMax), fishHeight, viewArea.yMax + m);
                baseDir = Vector3.back;
                break;
            case 1: // 下から → 上へ
                pos = new Vector3(Random.Range(viewArea.xMin, viewArea.xMax), fishHeight, viewArea.yMin - m);
                baseDir = Vector3.forward;
                break;
            case 2: // 左から → 右へ
                pos = new Vector3(viewArea.xMin - m, fishHeight, Random.Range(viewArea.yMin, viewArea.yMax));
                baseDir = Vector3.right;
                break;
            default: // 右から → 左へ
                pos = new Vector3(viewArea.xMax + m, fishHeight, Random.Range(viewArea.yMin, viewArea.yMax));
                baseDir = Vector3.left;
                break;
        }

        // 少し角度をつけて、直線的になりすぎないようにする
        Vector3 dir = Quaternion.Euler(0f, Random.Range(-angleRange, angleRange), 0f) * baseDir;

        FishPool.Instance.Get(size, pos, dir, despawnArea);
    }
}

