using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;

public class RandomStat : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    PlayerBaseStats playerStats;

    List<int> randomNum = new List<int>();
    System.Random random = new System.Random();
    string text;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomNumber = random.Next(-1, 3);
            randomNum.Add(randomNumber);
        }

        text = string.Join(",", randomNum.ToArray());

        canvas.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    void Update()
    {
        
    }
}
