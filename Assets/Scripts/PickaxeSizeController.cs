using UnityEngine;

public class PickaxeSizeController : MonoBehaviour
{
    private float Confidence => GameManager.Instance.Confidence;
    private float confidence;
    

    private void Update()
    {
        confidence = Confidence / 3;
        confidence = Mathf.Clamp(confidence, 1, 2);
        gameObject.transform.localScale = new Vector3(confidence, confidence, confidence);
    }
}
