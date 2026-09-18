using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum Size
{
    Monstar,
    Big,
    Midium,
    Nomal,
    Smoale,
    Minimamu

}
public enum FishClass
{ 
    c_Greet,
    c_Hipper,
    c_Supper,
    c_Nomal,
    c_mini
}
[System.Serializable]
public class SizeData
{
    [SerializeField] public Size size;
    [SerializeField] public float fishPoint;
}

[CreateAssetMenu(fileName = "FishData", menuName = "Scriptable Objects/FishData/Data")]
public class FishData : ScriptableObject
{
    [SerializeField] private string FishName;
    [SerializeField] private FishClass f_class;
    [SerializeField] private float MaxSize;
    [SerializeField] private float MinSize;
    [SerializeField] private float SizelDistance;

    public List<SizeData> SizePointList = new();

  
}
