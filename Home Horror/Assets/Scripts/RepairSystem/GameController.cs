using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void UpdateRepairablesAction();

    public static event UpdateRepairablesAction OnUpdateRepairables;

    public delegate void PickupAction(Material AMaterial);

    public static event PickupAction OnUpdatePickups;

    private void OnEnable()
    {
        MaterialController.MaterialInteraction += Proof;
    }

    private void OnDisable()
    {
        MaterialController.MaterialInteraction -= Proof;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnUpdateRepairables?.Invoke();
        }
    }

    private void Proof(Material AMaterial)
    {
        Debug.Log("Material Components:"+AMaterial.Name+"\n"+AMaterial.Amount);
        OnUpdatePickups?.Invoke(AMaterial);
    }
}
