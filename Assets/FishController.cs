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
