using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI carryCapacityText;

    private void Start()
    {
        carryCapacityText.text = $"0/{GameManager.Instance.CarryCapacity}";
    }

    public void SetCapacityText(FloatEvent ctx)
    {
        carryCapacityText.text = $"{ctx.FloatValue}/{GameManager.Instance.CarryCapacity}";
    }
}
