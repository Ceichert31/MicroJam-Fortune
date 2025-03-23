using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private int MaxHearts => GameManager.Instance.MaxHealth;

    [SerializeField] GameObject heartPrefab;

    [SerializeField] private List<GameObject> heartList;
    private HorizontalLayoutGroup layoutGroup;

    [Header("Heart Settings")]
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float fadeDelay = 2f;


    private const float FADE_IN_TIME = 0.1f;

    private void Start()
    {
        layoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();

        //Add max hearts at start
        AddHearts(new FloatEvent(MaxHearts));
        UpdateSpacing(new FloatEvent(MaxHearts));
    }
    public void AddHearts(FloatEvent ctx)
    {
        if (ctx.FloatValue > MaxHearts) return;

        //Fade in hearts
        FadeIn();

        //Play heal SFX

        for (int i = 0; i < ctx.FloatValue; i++)
        {
            //Add heart
            heartList.Add(Instantiate(heartPrefab, transform.GetChild(0)));
        }

        //Fade out hearts
        //Invoke(nameof(FadeOut), fadeDelay);
        StartCoroutine(FadeOutDelay());
    }
    public void RemoveHearts(FloatEvent ctx)
    {
        if (heartList.Count <= 0) return;

        //Fade in hearts
        FadeIn();

        //Play hurt SFX

        for (int i = 0; i < ctx.FloatValue; i++)
        {
            //Destroy last heart
            GameObject objectToRemove = heartList[heartList.Count - 1];
            heartList.RemoveAt(heartList.Count - 1);
            Destroy(objectToRemove);
        }

        //Fade out hearts
        //Invoke(nameof(FadeOut), fadeDelay);
        StartCoroutine(FadeOutDelay());
    }

    IEnumerator FadeOutDelay()
    {
        yield return new WaitForSeconds(fadeDelay);
        FadeOut();
    }

    /// <summary>
    /// Fades out all hearts
    /// </summary>
    private void FadeOut()
    {
        DOTween.CompleteAll();
        DOTween.Clear();
        DOTween.KillAll();

        foreach (GameObject obj in heartList)
        {
            if (obj == null) continue;
            obj.GetComponent<Image>().DOFade(0, fadeTime);
        }
    }
    private void FadeIn()
    {
        //Kill all tweens
        DOTween.CompleteAll();
        DOTween.Clear();
        DOTween.KillAll();

        foreach (GameObject obj in heartList)
        {
            if (obj == null) continue;
            obj.GetComponent<Image>().DOFade(1, FADE_IN_TIME);
        }
    }

    /// <summary>
    /// Reshapes spacing based on current health
    /// </summary>
    /// <param name="currentHealth"></param>
    public void UpdateSpacing(FloatEvent ctx)
    {
        switch (ctx.FloatValue)
        {
            case 3:
                layoutGroup.spacing = -2;
                break;

            case 5:
                layoutGroup.spacing = -1;
                break;

            case 7:
                layoutGroup.spacing = 0;
                break;
        }
    }
}
