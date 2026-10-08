using UnityEngine;
using UnityEngine.UI;

public class counter : MonoBehaviour
{
    private int point;
    public Text PointText;

    void Start()
    {
        point = 0;
    }


    private void Update()
    {
        PointText.text = point.ToString();
    }
    public void CountSystem(int FishPoint)
    {
        point += FishPoint;

    }
}
