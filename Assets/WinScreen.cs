using UnityEngine;

public class WinScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("hardModeUnlocked", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
