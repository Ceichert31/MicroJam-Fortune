using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private float Vision => GameManager.Instance.Vision;
    private float vision;

    private Light2D sceneLight;

    private void Start()
    {
        sceneLight = GetComponent<Light2D>();
    }

    private void Update()
    {
        vision = Vision / 10;

        sceneLight.intensity = Mathf.Clamp(vision, 0, 1);
    }
}
