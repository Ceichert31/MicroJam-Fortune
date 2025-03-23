using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class PlayerUIController : MonoBehaviour
{
    [Header("Bag UI References")]
    [SerializeField] private TextMeshProUGUI carryCapacityText;
    [SerializeField] private TextMeshProUGUI sapphireText;
    [SerializeField] private TextMeshProUGUI rubyText;
    [SerializeField] private TextMeshProUGUI emeraldText;
    [SerializeField] private TextMeshProUGUI topazText;

    private Animator bagAnimator;

    private int carryCapacity => GameManager.Instance.CarryingCapacity;
    private int capacity;

    [SerializeField] private AudioPitcherSO bagOpen;
    [SerializeField] private AudioPitcherSO bagClose;
    private AudioSource source;

    private void Start()
    {
        bagAnimator = GetComponent<Animator>();

        source = GetComponent<AudioSource>();

        capacity = carryCapacity;
        capacity = Mathf.Clamp(capacity, 1, 100);

        carryCapacityText.text = $"0/{capacity}";
    }

    public void SetCapacityText(FloatEvent ctx)
    {
        carryCapacityText.text = $"{ctx.FloatValue}/{capacity}";
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

    /// <summary>
    /// Sets the bag animaton
    /// </summary>
    /// <param name="ctx"></param>
    public void SetBagState(BoolEvent ctx)
    {
        bagAnimator.SetBool("IsOpen", ctx.Value);

        if (ctx.Value)
        {
            bagOpen.Play(source);
        }
        else
        {
            bagClose.Play(source);
        }
    }
}
