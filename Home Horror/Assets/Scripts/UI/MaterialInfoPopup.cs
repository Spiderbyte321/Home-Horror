using TMPro;
using UnityEngine;

public class MaterialInfoPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI materialsText;

    public void Setup(string materialName, int amount)
    {
        if (materialsText != null)
        {
            materialsText.text = $"{Capitalize(materialName)} +{amount}";
        }
    }

    private string Capitalize(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        return char.ToUpper(word[0]) + word.Substring(1);
    }
}
