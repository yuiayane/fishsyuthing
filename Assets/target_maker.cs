using UnityEngine;
using UnityEngine.UI;

public class target_maker : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private float followSpeed= 1.0f;
    public void TragetMoker()
    {
        Vector3 mousPos =Input.mousePosition;

         target.transform.position=mousPos;

        //transform.position =Vector3.Lerp(transform.position,targetPos,followSpeed*Time.deltaTime);
        return;
        
    }
  
}
