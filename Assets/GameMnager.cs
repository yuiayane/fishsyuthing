
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
    // UI‚©‚ç“Ç‚İæ‚é—p
    public int Score => score;
    public float TimeLeft => timeLeft;
    public int CountSmall => countS;
    public int CountMedium => countM;
    public int CountLarge => countL;
    public string Message => messageTimer > 0f ? message : "";
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
    public void Salvage(List<FishData> held)
    {
        GameManager.Instance.AddCatch(held);

        foreach (var d in held)
        {
            FishPool.Instance.Return(d.source);
        }
    }
  
}