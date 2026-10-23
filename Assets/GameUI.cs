using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public NetController net;
    public bool autoCreateCanvas = true;   // 未設定ならCanvasとTextを自動で作る

    public Text scoreText;     // 点数
    public Text timeText;      // 残り時間
    public Text caughtText;    // 捕獲した匹数(サイズ別)
    public Text netText;       // 網の枚数・網の中の匹数
    public Text messageText;   // +300 / 2匹同時! など
    public Text resultText;    // 終了時の結果

    private Font font;

    void Awake()
    {
        if (net == null) net = GetComponent<NetController>();
        if (autoCreateCanvas && scoreText == null) BuildCanvas();
    }

    void Start()
    {
        FixEventSystem();
    }

    // ---- Canvasの自動作成 ----
    private void BuildCanvas()
    {
        // OSの日本語フォントを使う(無ければArial)
        font = Font.CreateDynamicFontFromOSFont(
            new[] { "Yu Gothic UI", "Meiryo", "Hiragino Sans", "Noto Sans CJK JP", "Arial" }, 32);

        var go = new GameObject("GameCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        var canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // 画面サイズが変わっても、UIの大きさが保たれる
        var scaler = go.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        Transform t = go.transform;
        scoreText = MakeText(t, "ScoreText", new Vector2(0, 1), new Vector2(30, -20), new Vector2(700, 60), 40, TextAnchor.UpperLeft);
        netText = MakeText(t, "NetText", new Vector2(0, 1), new Vector2(30, -80), new Vector2(1000, 50), 30, TextAnchor.UpperLeft);
        timeText = MakeText(t, "TimeText", new Vector2(1, 1), new Vector2(-30, -20), new Vector2(400, 60), 40, TextAnchor.UpperRight);
        caughtText = MakeText(t, "CaughtText", new Vector2(0, 0), new Vector2(30, 30), new Vector2(1000, 50), 30, TextAnchor.LowerLeft);
        messageText = MakeText(t, "MessageText", new Vector2(0.5f, 0.5f), new Vector2(0, 150), new Vector2(900, 80), 48, TextAnchor.MiddleCenter);
        resultText = MakeText(t, "ResultText", new Vector2(0.5f, 0.5f), new Vector2(0, 0), new Vector2(900, 300), 56, TextAnchor.MiddleCenter);
        resultText.gameObject.SetActive(false);
    }

    private Text MakeText(Transform parent, string name, Vector2 anchor, Vector2 pos,
                          Vector2 size, int fontSize, TextAnchor align)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Text), typeof(Outline));
        var rt = (RectTransform)go.transform;
        rt.SetParent(parent, false);
        rt.anchorMin = rt.anchorMax = rt.pivot = anchor;   // 画面の隅・中央に固定
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;

        var text = go.GetComponent<Text>();
        text.font = font;
        text.fontSize = fontSize;
        text.alignment = align;
        text.color = Color.white;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.raycastTarget = false;
        return text;
    }

    // ---- Input System環境での EventSystem 対策 ----
    // UIメニューで作ったEventSystemは旧Input用(StandaloneInputModule)で、
    // Input Systemのみの設定だとエラーが出続けるため、差し替える
    private void FixEventSystem()
    {
        var es = EventSystem.current;
        if (es == null) return;

        var old = es.GetComponent<StandaloneInputModule>();
        if (old != null) Destroy(old);

        if (es.GetComponent<InputSystemUIInputModule>() == null)
            es.gameObject.AddComponent<InputSystemUIInputModule>();
    }

    // ---- 表示の更新 ----
    void Update()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        int total = gm.CountSmall + gm.CountMedium + gm.CountLarge;

        Set(scoreText, $"SCORE {gm.Score}");
        Set(timeText, $"TIME {Mathf.CeilToInt(gm.TimeLeft)}");
        Set(caughtText, $"小:{gm.CountSmall}  中:{gm.CountMedium}  大:{gm.CountLarge}  (計 {total}匹)");
        Set(messageText, gm.Message);

        if (net != null)
            Set(netText, $"網 {net.ActiveNets} / {net.maxNets}   網の中 {net.HeldCount}匹");

        if (resultText != null)
        {
            resultText.gameObject.SetActive(!gm.isPlaying);
            resultText.text =
                $"FINISH!\nSCORE {gm.Score}\n小:{gm.CountSmall}  中:{gm.CountMedium}  大:{gm.CountLarge}";
        }
    }

    private static void Set(Text t, string s)
    {
        if (t != null) t.text = s;
    }
}