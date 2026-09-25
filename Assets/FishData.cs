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
    [SerializeField] private FishClass F_Class;
    [SerializeField] private float MaxSize;
    [SerializeField] private float MinSize;
    [SerializeField] private float SizelDistance;
    [SerializeField] private SizeData FS_Data;
    public List<SizeData> SizePointList = new();

    public string fishName {  get { return FishName; } }
    public FishClass f_class { get { return F_Class; } }
    public float maxSize { get { return MaxSize; } }
    public float minSize { get { return MinSize; } }
     public SizeData fs_data { get { return FS_Data; } }


  
}
