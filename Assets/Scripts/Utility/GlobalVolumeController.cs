using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolumeController : MonoBehaviour
{
    [SerializeField] private VolumeProfile defaultProfile,
        blueProfile,
        redProfile,
        greenProfile,
        yellowProfile;
    private Volume volume;

    private GameManager.Biomes currentBiome => GameManager.Instance.CurrentBiome;

    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Update()
    {
        switch (currentBiome)
        {
            case GameManager.Biomes.DEFAULT:
                volume.profile = defaultProfile;
                break;

            case GameManager.Biomes.BLUE:
                volume.profile = blueProfile;
                break;

            case GameManager.Biomes.YELLOW:
                volume.profile = yellowProfile;
                break;

            case GameManager.Biomes.GREEN:
                volume.profile = greenProfile;
                break;

            case GameManager.Biomes.RED:
                volume.profile = redProfile;
                break;
        }
    }
}
