using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private int MaxHearts => GameManager.Instance.MaxHealth;

    [SerializeField] GameObject heartPrefab;

    [SerializeField] private List<GameObject> layoutGroup;

    [Header("Heart Settings")]
    [SerializeField] private float transitionTime;

    private void Start()
    {
        AddHearts(new FloatEvent(MaxHearts));
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
            layoutGroup.Add(Instantiate(heartPrefab, transform.GetChild(0)));
        }

        //Instaniate and add new hearts when healing
        //Destroy old hearts

    }
    public void RemoveHearts(FloatEvent ctx)
    {
        if (layoutGroup.Count <= 0) return;

        for (int i = 0; i < ctx.FloatValue; i++)
        {
            //Destroy last heart
            GameObject objectToRemove = layoutGroup[layoutGroup.Count - 1];
            layoutGroup.RemoveAt(layoutGroup.Count - 1);
            Destroy(objectToRemove);
        }

        //Instaniate and add new hearts when healing
        //Destroy old hearts

    }
}
