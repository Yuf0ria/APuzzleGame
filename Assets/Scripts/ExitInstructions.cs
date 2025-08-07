using UnityEngine;
using UnityEngine.UI;

public class ExitInstructions : MonoBehaviour
{
    [SerializeField] GameObject instructionUI;
    [SerializeField] Button exitButton;
    void Start()
    {
        instructionUI.SetActive(true);
    }

    void Update()
    {
        
    }

    public void ExitInstruction()
    {
        instructionUI.SetActive(false);
    }
}
