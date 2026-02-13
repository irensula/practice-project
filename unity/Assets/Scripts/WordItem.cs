using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.IO.Compression;
public class WordItem : MonoBehaviour, IPointerClickHandler
{
    public int id;
    private VocabularyUI gameManager;
    private TMP_Text text;
    private Image background;
    private Color normalColor = new Color32(222, 226, 255, 255);
    private Color selectedColor = new Color32(131, 106, 234, 255);
    private bool isMatched = false;

    void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        background = GetComponent<Image>();
    }

    public void Setup(int itemId, string word, VocabularyUI manager)
    {
        id = itemId;
        text.text = word;
        gameManager = manager;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMatched) return;

        gameManager.SelectWord(this);
    }

    public void SetMatched()
    {
        isMatched = true;
        text.color = new Color32(222, 226, 255, 255);
    } 

    public void SetSelected(bool value)
    {
        background.color = value ? selectedColor : normalColor;
    }

    public bool IsMatched()
    {
        return isMatched;
    }

    public void ResetItem()
    {
        isMatched = false;
        background.color = normalColor;
        text.color = Color.white;
    }
}
