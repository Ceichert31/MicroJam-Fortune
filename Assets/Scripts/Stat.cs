using TMPro;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] StatsDescriptions describer;
    [SerializeField] string title;
    [SerializeField] string description;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        describer.UpdateText(title, description);
    }

    public void UnHover()
    {
        describer.Close();
    }
}
