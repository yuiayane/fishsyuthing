using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetController : MonoBehaviour
{
    private enum NetState { Dropping, Resting, Salvaging }

    // 網1枚ごとの状態
    private class NetUnit
    {
        public GameObject obj;
        public Vector3 target;       // 水面の着地点
        public Vector3 top;          // 引き上げ先(上空)
        public NetState state = NetState.Dropping;
        public List<FishData> held = new List<FishData>(); // この網の中の魚
    }

    public FishGet fishGet;
    public Camera cam;
    public GameObject netPrefab;
    public int maxNets = 2;           // サルベージしていない網の最大数
    public float waterHeight = 0f;
    public float dropHeight = 6f;
    public float netSpeed = 15f;
    public float salvageSpeed = 6f;

    private Plane waterPlane;
    private readonly List<NetUnit> nets = new List<NetUnit>();

    public object ActiveNets { get; internal set; }
    public object HeldCount { get; internal set; }

    void Start()
    {
        if (cam == null) cam = Camera.main;
        waterPlane = new Plane(Vector3.up, new Vector3(0f, waterHeight, 0f));
    }

    // 水面にある(まだサルベージしていない)網の数
    private int ActiveCount()
    {
        int n = 0;
        foreach (var u in nets)
            if (u.state != NetState.Salvaging) n++;
        return n;
    }

    void Update()
    {
        HandleThrow();
        HandleSalvageInput();

        // 後ろから回す(途中で削除するため)
        for (int i = nets.Count - 1; i >= 0; i--)
        {
            var u = nets[i];
            switch (u.state)
            {
                case NetState.Dropping: UpdateDropping(u); break;
                case NetState.Resting: fishGet.Collect(u.target, u.obj.transform, u.held); break;
                case NetState.Salvaging:
                    if (UpdateSalvaging(u)) nets.RemoveAt(i);
                    break;
            }
        }
    }

    private void HandleThrow()
    {
        if (netPrefab == null || !GameManager.Instance.isPlaying) return;
        if (ActiveCount() >= maxNets) return;   // 最大数に達していたら投入できない
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame) return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!waterPlane.Raycast(ray, out float dist)) return;

        var u = new NetUnit();
        u.target = ray.GetPoint(dist);
        u.top = u.target + Vector3.up * dropHeight;

        u.obj = Instantiate(netPrefab, u.top, netPrefab.transform.rotation);
        foreach (var c in u.obj.GetComponentsInChildren<Collider>()) Destroy(c);
        foreach (var r in u.obj.GetComponentsInChildren<Rigidbody>()) Destroy(r);

        nets.Add(u);
    }

    // サルベージ: 水面にある網をまとめて引き上げる
    private void HandleSalvageInput()
    {
        bool pressed = (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
                    || (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame);

        // 時間切れのときは自動で引き上げる
        if (!pressed && GameManager.Instance.isPlaying) return;

        foreach (var u in nets)
            if (u.state != NetState.Salvaging) u.state = NetState.Salvaging;
    }

    private void UpdateDropping(NetUnit u)
    {
        u.obj.transform.position = Vector3.MoveTowards(
            u.obj.transform.position, u.target, netSpeed * Time.deltaTime);

        if (u.obj.transform.position == u.target) u.state = NetState.Resting;
    }

    // 引き上げ完了で true(ここで初めて得点になる)
    private bool UpdateSalvaging(NetUnit u)
    {
        u.obj.transform.position = Vector3.MoveTowards(
            u.obj.transform.position, u.top, salvageSpeed * Time.deltaTime);

        if (u.obj.transform.position != u.top) return false;

        fishGet.Salvage(u.held);
        Destroy(u.obj);
        return true;
    }

    void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.label) { fontSize = 24 };

        int caught = 0;
        foreach (var u in nets)
            if (u.state != NetState.Salvaging) caught += u.held.Count;

        GUI.Label(new Rect(20, 100, 700, 40),
            $"網: {ActiveCount()} / {maxNets}   網の中: {caught}匹", style);

        if (ActiveCount() > 0)
            GUI.Label(new Rect(20, 135, 700, 40), "右クリック / スペースで引き上げ", style);
    }
}
