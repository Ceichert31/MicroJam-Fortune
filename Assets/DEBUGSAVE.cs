using TMPro;
using UnityEngine;

public class DEBUGSAVE : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI a;

    private void Start()
    {
        a.text = GameManager.Instance.CarryingCapacity.ToString();
    }
}
