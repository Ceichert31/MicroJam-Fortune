using TMPro;
using UnityEngine;

public class DrillUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sapphireText;
    [SerializeField] private TextMeshProUGUI rubyText;
    [SerializeField] private TextMeshProUGUI emeraldText;
    [SerializeField] private TextMeshProUGUI topazText;

    [SerializeField] private OreCost repairCost;

    public void Start()
    {
        //Init text
        sapphireText.text = $"00/{repairCost.requiredSapphires}";
        rubyText.text = $"00/{repairCost.requiredRubies}";
        emeraldText.text = $"00/{repairCost.requiredEmerald}";
        topazText.text = $"00/{repairCost.requiredTopaz}";
    }

    public void UpdateOreUI(OreEvent ctx)
    {
        switch (ctx.Value.oreType)
        {
            case OreType.Sapphire:
                sapphireText.text = $"0{ctx.Count}/{repairCost.requiredSapphires}";
                break;

            case OreType.Ruby:
                rubyText.text = $"0{ctx.Count}/{repairCost.requiredRubies}";
                break;

            case OreType.Emerald:
                emeraldText.text = $"0{ctx.Count}/{repairCost.requiredEmerald}";
                break;

            case OreType.Topaz:
                topazText.text = $"0{ctx.Count}/{repairCost.requiredTopaz}";
                break;
        }
    }
}
