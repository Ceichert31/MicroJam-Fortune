using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] string prefName;
    int value = 0;
    [SerializeField] private Sprite[] sprites;
    Image spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("hardModeUnlocked") != 1)
        {
            if (PlayerPrefs.GetInt("hardMode") != 0)
                PlayerPrefs.SetInt("hardMode", 0);
            gameObject.SetActive(false);
        }

        spriteRenderer = GetComponent<Image>();
        value = PlayerPrefs.GetInt(prefName);
        spriteRenderer.sprite = sprites[value];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        switch (value)
        {
            case 0:
                value = 1;
                break;
            case 1:
                value = 0;
                break;
        }

        PlayerPrefs.SetInt(prefName, value);
        spriteRenderer.sprite = sprites[value];

        Debug.Log("Hard Mode: " + value);
    }
}
