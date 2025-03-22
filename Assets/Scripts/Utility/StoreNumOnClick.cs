using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreNumOnClick : MonoBehaviour
{
    private List<int> randomNums = RandomStat.randomNum;
    public Button button;
    public int storeNum;

    public int OnButtonClick()
    {
        button = GetComponent<Button>();
        storeNum = int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text);

        return storeNum;
    }

}
