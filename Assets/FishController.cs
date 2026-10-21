using UnityEngine;

public class FishController : MonoBehaviour
{
    public FishSize size = FishSize.Small;
    public float speed = 2f;

    public bool isCaught { get; private set; }

    private Vector3 moveDir;
    private Rect despawnArea;        // この範囲の外に出たらPoolに戻る
    private Transform followTarget;
    private Vector3 followOffset;

    void OnEnable() { FishGet.AllFish.Add(this); }
    void OnDisable() { FishGet.AllFish.Remove(this); }

    public void Setup(FishSize newSize, float newSpeed, Vector3 dir, Rect area)
    {
        size = newSize;
        speed = newSpeed;
        moveDir = dir.normalized;
        despawnArea = area;
        isCaught = false;
        followTarget = null;

        float scale = 0.4f + 0.3f * (int)size;
        transform.localScale = Vector3.one * scale;

        // 進む方向にカプセルの長い軸を向ける
        transform.rotation = Quaternion.FromToRotation(Vector3.up, moveDir);
    }

    public void Catch(Transform net)
    {
        isCaught = true;
        followTarget = net;
        followOffset = transform.position - net.position;
    }

    void Update()
    {
        if (isCaught)
        {
            if (followTarget != null)
                transform.position = followTarget.position + followOffset;
            return;
        }

        transform.position += moveDir * (speed * Time.deltaTime);

        // 画面外に出たらPoolに戻す
        Vector2 p = new Vector2(transform.position.x, transform.position.z);
        if (!despawnArea.Contains(p))
        {
            FishPool.Instance.Return(this);
        }
    }

    public FishData GetData()
    {
        return new FishData(size, transform.position, this);
    }
}
//using UnityEngine;

/// <summary>
/// 魚の自然な移動を再現するスクリプト
/// </summary>
//public class FishMovement : MonoBehaviour
//{
//    public float speed = 2.0f;          // 移動速度
//    public float turnSpeed = 0.05f;     // 曲がりやすさ
//    public Vector3 swimArea = new Vector3(8f, 4.5f, 8f); // 泳ぐ範囲（ワールド座標）
//    [SerializeField] private F_Data fish;
//    private float angle;                // 現在の向き（ラジアン）
//    private float wanderTimer;          // ランダム方向変更用タイマー
//    private float wanderInterval;       // 次に方向を変えるまでの時間
//    [SerializeField] private GameObject obj;

//    void Start()
//    {
//        // 初期化
//        angle = Random.Range(0f, Mathf.PI * 2f);
//        wanderInterval = Random.Range(0.5f, 2f);
//        turnSpeed = Random.Range(-0.05f, 0.05f);
//        speed = Random.Range(1.0f, 3.0f);
//    }

//    void Update()
//    {
//        // タイマー更新
//        wanderTimer += Time.deltaTime;
//        if (wanderTimer > wanderInterval)
//        {
//            // ランダムに方向を変える
//            turnSpeed = Random.Range(-0.05f, 0.05f);
//            wanderTimer = 0f;
//            wanderInterval = Random.Range(0.5f, 2f);
//        }

//        // 向きを更新
//        angle += turnSpeed;

//        // 移動
//        Vector3 pos = transform.position;
//        pos.x += Mathf.Cos(angle) * speed * Time.deltaTime;
//        pos.z += Mathf.Sin(angle) * speed * Time.deltaTime;

//        // 範囲外で反射
//        if (pos.x < -swimArea.x || pos.x > swimArea.x)
//        {
//            angle = Mathf.PI - angle;
//        }
//        if (pos.z < -swimArea.z || pos.z > swimArea.z)
//        {
//            angle = -angle;
//        }

//        transform.position = pos;

//        // 見た目の向き調整（SpriteやMeshを回転）
//        float deg = angle * Mathf.Rad2Deg;
//        transform.rotation = Quaternion.Euler(0, 0, deg);
//    }
//    public void SetFishGet()
//    {
//        this.fish = fish;
//        //FishPool.ReturnObject();
//        return;
//    }
//}