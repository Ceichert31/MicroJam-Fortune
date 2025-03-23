using System.Collections;
using UnityEngine;
using TMPro;

public class BiomePopupManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI biomeText;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float stayDuration = 2f;
    [SerializeField] private float fadeOutDuration = 1f;

    private Coroutine currentFadeCoroutine;
    private GameManager.Biomes lastBiome;

    private GameManager.Biomes currentBiome => GameManager.Instance.CurrentBiome;

    private void Start()
    {
        if (biomeText != null)
        {
            Color textColor = biomeText.color;
            textColor.a = 0f;
            biomeText.color = textColor;
        }

        lastBiome = currentBiome;
    }

    private void Update()
    {
        if (currentBiome != lastBiome)
        {
            ShowBiomeText(currentBiome);
            lastBiome = currentBiome;
        }
    }

    private void ShowBiomeText(GameManager.Biomes biomeType)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        biomeText.text = GetBiomeDisplayName(biomeType);

        biomeText.color = GetBiomeColor(biomeType);

        currentFadeCoroutine = StartCoroutine(FadeTextCoroutine());
    }

    private IEnumerator FadeTextCoroutine()
    {
        float elapsedTime = 0f;
        Color textColor = biomeText.color;
        textColor.a = 0f;
        biomeText.color = textColor;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeInDuration; // normalized time for lerp

            textColor.a = Mathf.Lerp(0f, 1f, t);
            biomeText.color = textColor;

            yield return null;
        }

        textColor.a = 1f;
        biomeText.color = textColor;

        yield return new WaitForSeconds(stayDuration);

        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeOutDuration; // normalized time for lerp

            textColor.a = Mathf.Lerp(1f, 0f, t);
            biomeText.color = textColor;

            yield return null;
        }

        textColor.a = 0f;
        biomeText.color = textColor;
    }

    private Color GetBiomeColor(GameManager.Biomes biomeType)
    {
        switch (biomeType)
        {
            case GameManager.Biomes.YELLOW:
                return new Color(1f, 0.92f, 0.016f); // Bright yellow
            case GameManager.Biomes.RED:
                return new Color(0.9f, 0.2f, 0.2f); // Bright red
            case GameManager.Biomes.BLUE:
                return new Color(0.2f, 0.6f, 1f); // Bright blue
            case GameManager.Biomes.GREEN:
                return new Color(0.2f, 0.8f, 0.2f); // Bright green
            case GameManager.Biomes.DEFAULT:
            default:
                return Color.white;
        }
    }

    private string GetBiomeDisplayName(GameManager.Biomes biomeType)
    {
        switch (biomeType)
        {
            case GameManager.Biomes.YELLOW:
                return "Amber Hollow";
            case GameManager.Biomes.RED:
                return "Fiery Depths";
            case GameManager.Biomes.BLUE:
                return "Frostbit Grotto";
            case GameManager.Biomes.GREEN:
                return "Toxic Shafts";
            case GameManager.Biomes.DEFAULT:
            default:
                return "Drill";
        }
    }
}