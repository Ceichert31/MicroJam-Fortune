using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
 

    [Header("Bag UI References")]
    [SerializeField] private TextMeshProUGUI carryCapacityText;
    [SerializeField] private TextMeshProUGUI sapphireText;
    [SerializeField] private TextMeshProUGUI rubyText;
    [SerializeField] private TextMeshProUGUI emeraldText;
    [SerializeField] private TextMeshProUGUI topazText;

    private void Start()
    {
        carryCapacityText.text = $"0/{GameManager.Instance.CarryCapacity}";
    }

    public void SetCapacityText(FloatEvent ctx)
    {
        carryCapacityText.text = $"{ctx.FloatValue}/{GameManager.Instance.CarryCapacity}";
    }

    /// <summary>
    /// Updates ore UI based on ore type
    /// </summary>
    /// <param name="ctx"></param>
    public void UpdateOreCountUI(OreEvent ctx)
    {
        switch (ctx.Value.oreType)
        {
            case OreType.Sapphire:
                sapphireText.text = ctx.Count.ToString();
                break;

            case OreType.Ruby:
                rubyText.text = ctx.Count.ToString();
                break;

            case OreType.Emerald:
                emeraldText.text = ctx.Count.ToString();
                break;

            case OreType.Topaz:
                topazText.text = ctx.Count.ToString();
                break;
        }
    }
}
