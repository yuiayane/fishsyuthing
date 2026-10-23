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
