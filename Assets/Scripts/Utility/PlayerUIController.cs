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
                sapphireText.text =  $"0{ctx.Count}";
                break;

            case OreType.Ruby:
                rubyText.text = $"0{ctx.Count}";
                break;

            case OreType.Emerald:
                emeraldText.text = $"0{ctx.Count}";
                break;

            case OreType.Topaz:
                topazText.text = $"0{ctx.Count}";
                break;
        }
    }
    public void ResetOreUI(VoidEvent ctx)
    {
        sapphireText.text = "00";
        rubyText.text = "00";
        emeraldText.text = "00";
        topazText.text = "00";
    }
}
