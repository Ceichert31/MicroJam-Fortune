using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private int MaxHearts => GameManager.Instance.MaxHealth;

    [SerializeField] private List<GameObject> layoutGroup;
    private List<Image> heartImages;

    [Header("Heart Settings")]
    [SerializeField] private float transitionTime;

    private void Start()
    {
        
    }

    [ContextMenu("Take Damage")]
    public void TEST()
    {
        UpdateHealthFromDamage(new FloatEvent(2));
    }

    public void UpdateHealthFromDamage(FloatEvent ctx)
    {
        //9 hearts
        //Start at end of list
        //Skip disabled hearts

        for (int i = 0; i < layoutGroup.Count; i++)
        {
            //Enable hearts below max health
            if (i <= MaxHearts)
            {
                layoutGroup[i].SetActive(true);

                //Start at end of list and damage hearts 
            }
        }

        //Start from max hearts and go down list
        for (int i = MaxHearts - 1; i < ctx.FloatValue; i--) 
        {
            layoutGroup[i].GetComponent<Image>().DOFade(0.3f, transitionTime);
        }        
    }
}
