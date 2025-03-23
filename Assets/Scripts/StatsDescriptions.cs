using DG.Tweening;
using TMPro;
using UnityEngine;

public class StatsDescriptions : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(string title, string desc)
    {
        transform.DOScaleX(1, 0.2f).SetEase(Ease.OutQuint);
        titleText.text = title;
        descText.text = desc;
    }

    public void Close()
    {
        transform.DOScaleX(0, 0.2f).SetEase(Ease.OutQuint);
    }
}
