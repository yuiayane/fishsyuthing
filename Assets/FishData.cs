using UnityEngine;

public enum SizePoint
{
}

[CreateAssetMenu(fileName = "FishData", menuName = "Scriptable Objects/FishData")]
public class FishData : ScriptableObject
{

    [SerializeField] private float MaxSize;
    [SerializeField] private float MinSize;
    [SerializeField] private float Size;
    [SerializeField] private float FishPoint;
    [SerializeField] private SizePoint SizelPoint;
    [SerializeField] private float SizelDistance;

}
