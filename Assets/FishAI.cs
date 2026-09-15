using UnityEngine;

/// <summary>
/// 魚の自然な移動を再現するスクリプト
/// </summary>
public class FishMovement : MonoBehaviour
{
    public float speed = 2.0f;          // 移動速度
    public float turnSpeed = 0.05f;     // 曲がりやすさ
    public Vector2 swimArea = new Vector2(8f, 4.5f); // 泳ぐ範囲（ワールド座標）

    private float angle;                // 現在の向き（ラジアン）
    private float wanderTimer;          // ランダム方向変更用タイマー
    private float wanderInterval;       // 次に方向を変えるまでの時間

    void Start()
    {
        // 初期化
        angle = Random.Range(0f, Mathf.PI * 2f);
        wanderInterval = Random.Range(0.5f, 2f);
        turnSpeed = Random.Range(-0.05f, 0.05f);
        speed = Random.Range(1.0f, 3.0f);
    }

    void Update()
    {
        // タイマー更新
        wanderTimer += Time.deltaTime;
        if (wanderTimer > wanderInterval)
        {
            // ランダムに方向を変える
            turnSpeed = Random.Range(-0.05f, 0.05f);
            wanderTimer = 0f;
            wanderInterval = Random.Range(0.5f, 2f);
        }

        // 向きを更新
        angle += turnSpeed;

        // 移動
        Vector3 pos = transform.position;
        pos.x += Mathf.Cos(angle) * speed * Time.deltaTime;
        pos.y += Mathf.Sin(angle) * speed * Time.deltaTime;

        // 範囲外で反射
        if (pos.x < -swimArea.x || pos.x > swimArea.x)
        {
            angle = Mathf.PI - angle;
        }
        if (pos.y < -swimArea.y || pos.y > swimArea.y)
        {
            angle = -angle;
        }

        transform.position = pos;

        // 見た目の向き調整（SpriteやMeshを回転）
        float deg = angle * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, deg);
    }
}