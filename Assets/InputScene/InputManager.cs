using System.Linq;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField testGeneNumberInputField;
    [SerializeField] private TextMeshProUGUI testGeneNumberText;

    [SerializeField] private TMP_InputField timeInputField;
    [SerializeField] private TextMeshProUGUI geneSizeText;

    [SerializeField] private TMP_InputField speedInputField;

    [SerializeField] private TMP_InputField mutationInputField;

    [SerializeField] private TextMeshProUGUI information;

    private void Start()
    {
        UpdateInformation();
    }
    public void UpdateGeneSize()
    {
        int result;
        if (int.TryParse(testGeneNumberInputField.text, out result))
        {
            GeneManager.passScale = result;
            result = result * (result - 1) / 2;
            testGeneNumberText.text = "Test Gene Number : " + result.ToString();
            testGeneNumberInputField.image.color = Color.green;
        }
        else
        {
            testGeneNumberInputField.image.color = Color.red;
        }

        UpdateInformation();
    }
    public void UpdateTimeScale()
    {
        float result;
        if (float.TryParse(timeInputField.text, out result))
        {
            GeneManager.time = result;
            geneSizeText.text = "Gene Size : " + GeneManager.size.ToString();
            timeInputField.image.color = Color.green;
        }
        else
        {
            timeInputField.image.color = Color.red;
        }

        UpdateInformation();
    }
    public void UpdateSpeed()
    {
        float result;
        if (float.TryParse(speedInputField.text, out result))
        {
            GeneManager.speed = result;
            speedInputField.image.color = Color.green;
        }
        else
        {
            speedInputField.image.color = Color.red;
        }

        UpdateInformation();
    }
    public void UpdateMutation()
    {
        float result;
        if (float.TryParse(mutationInputField.text, out result) && result <= 100)
        {
            GeneManager.mutationRate = result;
            mutationInputField.image.color = Color.green;
        }
        else
        {
            mutationInputField.image.color = Color.red;
        }

        UpdateInformation();
    }
    public void UpdateInformation()
    {
        information.text = "";
        information.text += "Passed Genes : " + GeneManager.passScale + "\n";
        information.text += "Test Genes : " + (GeneManager.passScale * (GeneManager.passScale - 1)) / 2 + "\n";
        information.text += "Gene Size : " + GeneManager.size.ToString() + "\n";
        information.text += "Time Scale : " + GeneManager.time.ToString() + "\n";
        information.text += "Gene Speed : " + GeneManager.speed + "\n";
        information.text += "Mutation Rate : " + GeneManager.mutationRate + "\n";
    }
}
