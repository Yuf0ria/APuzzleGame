using UnityEngine;
using UnityEngine.UI;

public class ExitInstructions : MonoBehaviour
{
    [SerializeField] GameObject instructionUI;
    void Start()
    {
        instructionUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        
    }

    public void ExitInstruction()
    {
        instructionUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
