using CustomInspector;
using System.Linq;
using TMPro;
using UnityEngine;

public class InputFieldCotroller : MonoBehaviour
{
    [SerializeField, SelfFill(true)] private TMP_InputField inputField;

    public string GetText()
    {
        return inputField.text;
    }

    public bool ValidateText()
    {
        return inputField.text.Count() > 3;
    }

    private void Start()
    {
        inputField.onValueChanged.AddListener(ValidateInput);
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
