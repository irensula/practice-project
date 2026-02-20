using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class WordCard : MonoBehaviour, IPointerClickHandler
{
    private int wordId;
    private MatchGame manager;

    [SerializeField] private TMP_Text text;
    public void Setup(int id, MatchGame gameManager)
    {
        wordId = id;
        manager = gameManager;
    }

    public void SetText(string value)
    {
        if (text != null)
            text.text = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager?.OnCardSelected(wordId);
    }
}

// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
// using TMPro;

// public class WordCard : MonoBehaviour, IPointerClickHandler
// {
//     [SerializeField] private TMP_Text textComponent;
//     [SerializeField] private Image background;
//     private Color normalColor = new Color32(131, 106, 234, 255);
//     private Color selectedColor = new Color32(255, 140, 0, 255);
//     private Color matchedColor = new Color32(141, 50, 212, 255);
//     private bool matched = false;

//     public override void SetSelected (bool value)
//     {
//         if (background == null || matched) return;
//         background.color = value ? selectedColor : normalColor;
//     }
//     public override void SetMatched()
//     {
//         matched = true;
//         if (background != null)
//             background.color = matchedColor;
//     }

//      public override void ResetItem()
//     {
//         matched = false;
//         if (background != null)
//             background.color = normalColor;
//     }

//     public override bool IsMatched()
//     {
//         return matched;
//     }

//     public void OnPointerClick(PointerEventData eventData)
//     {
//         if (!matched && game != null)
//         {
//             game.SelectItem(this, true);
//         }
//     }
// }

