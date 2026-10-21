
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float timeLimit = 60f;
    public int scoreSmall = 100, scoreMedium = 300, scoreLarge = 1000;
    public float comboBonus = 0.5f;           // 2•C–ÚˆÈ~1•C‚²‚Æ‚É +50%

    public bool isPlaying { get; private set; } = true;
    private float timeLeft;
    private int score;
    private int countS, countM, countL;
    private string message = "";
    private float messageTimer;

    void Awake()
    {
        Instance = this;
        timeLeft = timeLimit;
    }

    void Update()
    {
        if (!isPlaying) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f) { timeLeft = 0f; isPlaying = false; }

        if (messageTimer > 0f) messageTimer -= Time.deltaTime;
    }

    // FishGet‚©‚çADataList‚ğ‚Ü‚Æ‚ß‚Ä“n‚³‚ê‚é
    public void AddCatch(List<FishData> list)
    {
        if (list.Count == 0) { Show("ƒ~ƒXc"); return; }

        int baseScore = 0;
        foreach (var d in list)
        {
            switch (d.size)
            {
                case FishSize.Small: baseScore += scoreSmall; countS++; break;
                case FishSize.Medium: baseScore += scoreMedium; countM++; break;
                case FishSize.Large: baseScore += scoreLarge; countL++; break;
            }
        }

        float mult = 1f + comboBonus * (list.Count - 1);
        int gain = Mathf.RoundToInt(baseScore * mult);
        score += gain;

        Show(list.Count >= 2 ? $"{list.Count}•C“¯!  +{gain}" : $"+{gain}");
    }

    private void Show(string text) { message = text; messageTimer = 2f; }

    void OnGUI()
    {
        var big = new GUIStyle(GUI.skin.label) { fontSize = 28 };
        GUI.Label(new Rect(20, 10, 400, 40), $"SCORE {score}", big);
        GUI.Label(new Rect(20, 50, 400, 40), $"TIME {Mathf.CeilToInt(timeLeft)}", big);

        if (messageTimer > 0f)
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 40), message, big);

        if (!isPlaying)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 80, 400, 200),
                $"FINISH!\nSCORE {score}\n¬:{countS} ’†:{countM} ‘å:{countL}", big);
        }
    }
}