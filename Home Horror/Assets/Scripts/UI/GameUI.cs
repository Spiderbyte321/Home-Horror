using System.Text;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public InventorySlot[] slots;
    
    public GameObject materialsPopup;
    private GameObject materialsActivePopup;
    
    public GameObject repairPopup;
    private GameObject repairActivePopup;

    public GameObject winScreen;
    
    public Transform spawnPoint;
    
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private GameManager gameManager;
    
    public GameObject loseScreen;

    private void Update()
    {
        UpdateMoneyText();
        UpdateInventorySlots();
    }

    private void UpdateInventorySlots()
    {
        if (playerInventory == null) return;

        var materials = playerInventory.GetAllMaterials();

        foreach (var slot in slots)
        {
            materials.TryGetValue(slot.materialKey.ToLower(), out int count);
            slot.UpdateSlot(count);
        }
    }
    
    private string Capitalize(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        return char.ToUpper(word[0]) + word.Substring(1);
    }
    
    public void ShowMaterialInfoPopup(string materialName, int amount)
    {
        if (materialsPopup == null) return;

        if (materialsActivePopup != null)
            Destroy(materialsActivePopup);

        materialsActivePopup = Instantiate(materialsPopup, spawnPoint.position, Quaternion.identity);
        materialsActivePopup.transform.SetParent(GameObject.Find("Canvas").transform, worldPositionStays: true); // Place on canvas
        
        // Access MaterialPopupUI script inside the instantiated popup
        var popupUI = materialsActivePopup.GetComponent<MaterialInfoPopup>();
        if (popupUI != null)
        {
            popupUI.Setup(materialName, amount);
        }
    }

    public void HideMaterialInfoPopup()
    {
        if (materialsActivePopup != null)
            Destroy(materialsActivePopup);
    }
    
    public void ShowRepairInfoPopup(int moneyCost, string materialName, int amount)
    {
        if (repairPopup == null) return;

        if (repairActivePopup != null)
            Destroy(repairActivePopup);

        repairActivePopup = Instantiate(repairPopup, spawnPoint.position, Quaternion.identity);
        repairActivePopup.transform.SetParent(GameObject.Find("Canvas").transform, worldPositionStays: true); // Place on canvas
        
        // Access RepairPopupUI script inside the instantiated popup
        var popupUI = repairActivePopup.GetComponent<RepairInfoPopup>();
        if (popupUI != null)
        {
            popupUI.Setup(moneyCost, materialName, amount);
        }
    }

    public void HideRepairInfoPopup()
    {
        if (repairActivePopup != null)
            Destroy(repairActivePopup);
    }

    private void UpdateMoneyText()
    {
        if (playerInventory == null || gameManager == null) return;

        int currentMoney = playerInventory.Money;
        int moneyGoal = gameManager.moneyGoal;

        moneyText.text = $"R{currentMoney} / R{moneyGoal}";
    }

    public void ShowWinScreen()
    {
        if (winScreen == null) return;
        HideMaterialInfoPopup();
        HideRepairInfoPopup();
        winScreen.SetActive(true);
        
        Time.timeScale = 0f;
    }
    
    public void ShowLoseScreen()
    {
        if (loseScreen == null) return;
    
        HideMaterialInfoPopup();
        HideRepairInfoPopup();
    
        loseScreen.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }
}
