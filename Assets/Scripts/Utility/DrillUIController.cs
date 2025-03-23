using DG.Tweening;
using TMPro;
using UnityEngine;

public class DrillUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sapphireText;
    [SerializeField] private TextMeshProUGUI rubyText;
    [SerializeField] private TextMeshProUGUI emeraldText;
    [SerializeField] private TextMeshProUGUI topazText;

    [SerializeField] private OreCost repairCost;
    [SerializeField] OreCost hard;

    public void Start()
    {

        //Init text
        sapphireText.text = $"00/{repairCost.requiredSapphires}";
        rubyText.text = $"00/{repairCost.requiredRubies}";
        emeraldText.text = $"00/{repairCost.requiredEmerald}";
        topazText.text = $"00/{repairCost.requiredTopaz}";

        if (PlayerPrefs.GetInt("hardMode") == 0)
        {
            sapphireText.text = $"00/{repairCost.requiredSapphires}";
            rubyText.text = $"00/{repairCost.requiredRubies}";
            emeraldText.text = $"00/{repairCost.requiredEmerald}";
            topazText.text = $"00/{repairCost.requiredTopaz}";
        }
        else
        {
            //Hard
            sapphireText.text = $"00/{hard.requiredSapphires}";
            rubyText.text = $"00/{hard.requiredRubies}";
            emeraldText.text = $"00/{hard.requiredEmerald}";
            topazText.text = $"00/{hard.requiredTopaz}";
        }
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
    //open when player near
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).DOScaleX(1, 0.2f).SetEase(Ease.OutQuint);
        }
    }

    //close when player not near
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).DOScaleX(0, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}
