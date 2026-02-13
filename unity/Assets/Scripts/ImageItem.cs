using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageItem : MonoBehaviour, IPointerClickHandler
{
    public int id;
    private VocabularyUI gameManager;
    private Color normalColor = Color.white;
    private Color selectedColor = new Color(0.6f, 1f, 0.6f);
    private bool isMatched = false;
    [SerializeField] private Image picture;
    [SerializeField] private Image border;

    void Awake()
    {
        if (picture == null) Debug.LogWarning("Picture not assigned!");
        if (border == null) Debug.LogWarning("Border not assigned!");
    }
    public void Setup(int itemId, Sprite sprite, VocabularyUI manager)
    {
        id = itemId;
        picture.sprite = sprite;
        gameManager = manager;

        border.color = Color.clear;
        picture.color = Color.white;
        isMatched = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMatched) return;
        gameManager.SelectImage(this);
    }
    public void SetSelected (bool value)
    {
        border.color = value ? new Color32(0xFF, 0x8C, 0x00, 0xFF) : Color.clear;
        Debug.Log("SetSelected color: " + border.color);
    }
    public void SetMatched()
    {
        isMatched = true;
        picture.color = new Color(1,1,1,0.4f);
        border.color = new Color32(131, 106, 234, 255);
        Debug.Log("Matched color: " + border.color);
    }

    public bool IsMatched()
    {
        return isMatched;
    }
}

