//using System.Collections.Generic;
//using UnityEngine;

//public enum Size
//{
//    Monstar,
//    Big,
//    Midium,
//    Nomal,
//    Smoale,
//    Minimamu

//}
//public enum FishClass
//{
//    c_Greet,
//    c_Hipper,
//    c_Supper,
//    c_Nomal,
//    c_mini
//}
//[System.Serializable]
//public class SizeData
//{
//    [SerializeField] public Size size;
//    [SerializeField] public float fishPoint;
//}

//[CreateAssetMenu(fileName = "FishData", menuName = "Scriptable Objects/FishData/Data")]
//public class F_Data : ScriptableObject
//{
//    [SerializeField] private string FishName;
//    [SerializeField] private FishClass F_Class;
//    [SerializeField] private float MaxSize;
//    [SerializeField] private float MinSize;
//    [SerializeField] private float SizelDistance;
//    public List<SizeData> SizePointList = new();

//    public string fishName { get { return FishName; } }
//    public FishClass f_class { get { return F_Class; } }
//    public float maxSize { get { return MaxSize; } }
//    public float minSize { get { return MinSize; } }
//    public SizeData fs_data { get { return SizePointList[0]; } }

//}
using UnityEngine;

public enum FishSize { Small, Medium, Large }

[System.Serializable]
public class FishData
{
    public FishSize size;
    public Vector3 position;
    public FishController source; // å≥ÇÃãõ(ïﬂälå„ÇÃçÌèúóp)

    public FishData(FishSize size, Vector3 position, FishController source)
    {
        this.size = size;
        this.position = position;
        this.source = source;
    }
}
