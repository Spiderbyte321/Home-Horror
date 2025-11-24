using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string materialKey;
    public Image icon;
    public TextMeshProUGUI countText;

    public void UpdateSlot(int count)
    {
        countText.text = count.ToString();
        icon.enabled = count > 0;  // hide icon if empty
    }
}