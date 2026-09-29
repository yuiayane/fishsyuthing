using System.Collections;
using System.Collections.Generic;
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


    void Update()
    {
        PointText.text = point.ToString();
    }
    public void CountSystem(int FishPoint)
    {
        point += 1;

    }
}
