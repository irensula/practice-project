using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class WordCard : BaseMatchCard
{
    [SerializeField] private TMP_Text text;

    public void SetText(string value)
    {
        if (text != null)
            text.text = value;
    }
}
