using CustomInspector;
using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    [SerializeField, SelfFill(true)] private Image inputImage;
    [SerializeField, SelfFill(true)] private TMP_InputField inputField;

    [SerializeField] private Color incorrectColor;
    [SerializeField] private float incorrectAnimDuration;

    private Color startColor;


    public string GetText()
    {
        return inputField.text;
    }

    public bool ValidateText()
    {
        bool isCorrect = inputField.text.Count() > 3;

        if (!isCorrect)
        {
            inputImage.DOColor(incorrectColor, incorrectAnimDuration / 2)
                .OnComplete(() => inputImage.DOColor(startColor, incorrectAnimDuration / 2));
        }
        return isCorrect;
    }

    private void Start()
    {
        inputField.onValueChanged.AddListener(ValidateInput);

        startColor = inputImage.color;
    }

    private void ValidateInput(string input)
    {
        string validatedText = "";
        foreach (char c in input)
        {
            if (IsEnglishLetterOrDigit(c))
            {
                validatedText += c;
            }
        }

        if (input != validatedText)
        {
            inputField.text = validatedText;
        }
    }

    private bool IsEnglishLetterOrDigit(char c)
    {
        // Проверка, является ли символ буквой (A-Z, a-z) или цифрой (0-9)
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
    }
}
