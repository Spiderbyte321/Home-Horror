using UnityEngine;

public class GameController : MonoBehaviour
{

    public delegate void UpdateRepairablesAction();

    public static event UpdateRepairablesAction OnUpdateRepairables;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnUpdateRepairables?.Invoke();
        }
    }
}
