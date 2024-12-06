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
            GeneManager.PassScale = result;
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
            GeneManager.Time = result;
            geneSizeText.text = "Gene Size : " + GeneManager.Size.ToString();
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
            GeneManager.Speed = result;
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
            GeneManager.MutationRate = result;
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
        information.text += "Passed Genes : " + GeneManager.PassScale + "\n";
        information.text += "Test Genes : " + (GeneManager.PassScale * (GeneManager.PassScale - 1)) / 2 + "\n";
        information.text += "Gene Size : " + GeneManager.Size.ToString() + "\n";
        information.text += "Time Scale : " + GeneManager.Time.ToString() + "\n";
        information.text += "Gene Speed : " + GeneManager.Speed + "\n";
        information.text += "Mutation Rate : " + GeneManager.MutationRate + "\n";
    }
}
