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
    [SerializeField] private float transitionTime;

    private void Start()
    {
        layoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();

        AddHearts(new FloatEvent(MaxHearts));

        UpdateSpacing(MaxHearts);
    }

    [ContextMenu("Add Health")]
    public void Heal()
    {
        AddHearts(new FloatEvent(2));
    }

    [ContextMenu("Take Damage")]
    public void TEST()
    {
        RemoveHearts(new FloatEvent(2));
    }
    public void AddHearts(FloatEvent ctx)
    {
        if (ctx.FloatValue > MaxHearts) return;

        for (int i = 0; i < ctx.FloatValue; i++)
        {
            //Add heart
            heartList.Add(Instantiate(heartPrefab, transform.GetChild(0)));
        }

        //Instaniate and add new hearts when healing
        //Destroy old hearts
        UpdateSpacing(ctx.FloatValue);
    }
    public void RemoveHearts(FloatEvent ctx)
    {
        if (heartList.Count <= 0) return;

        for (int i = 0; i < ctx.FloatValue; i++)
        {
            //Destroy last heart
            GameObject objectToRemove = heartList[heartList.Count - 1];
            heartList.RemoveAt(heartList.Count - 1);
            Destroy(objectToRemove);
        }

        //Reshape spacing based on current health
        float currentHealth = MaxHearts - ctx.FloatValue;
        UpdateSpacing(currentHealth);
    }

    /// <summary>
    /// Reshapes spacing based on current health
    /// </summary>
    /// <param name="currentHealth"></param>
    private void UpdateSpacing(float currentHealth)
    {
        switch (currentHealth)
        {
            case 3:
                layoutGroup.spacing = -2;
                break;

            case 5:
                layoutGroup.spacing = 0;
                break;
        }
    }
}
