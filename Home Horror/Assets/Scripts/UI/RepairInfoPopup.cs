using TMPro;
using UnityEngine;

public class RepairInfoPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI repairText;

    public void Setup(int moneyCost, string materialName, int amount)
    {
        if (repairText != null)
        {
            repairText.text = $"R{moneyCost} OR {Capitalize(materialName)}: {amount}";
        }
    }

    private string Capitalize(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        return char.ToUpper(word[0]) + word.Substring(1);
    }
}
