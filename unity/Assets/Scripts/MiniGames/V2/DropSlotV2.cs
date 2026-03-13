using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlotV2 : BaseMatchCardV2, IDropHandler
{
    public int ExpectedWordId;
    private DraggableItemV2 currentWord;
    private BaseMatchGameV2 matchGame;
    private Image slotImage;
    public bool IsMatched { get; private set; }
    [SerializeField] private Color matchedColor = new Color32(131, 106, 234, 50);

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void Setup(int wordId, BaseMatchGameV2 game)
    {
        ExpectedWordId = wordId;
        matchGame = game;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItemV2 dropped = eventData.pointerDrag.GetComponent<DraggableItemV2>();

        if (dropped == null)
            return;

        if (dropped.WordId == ExpectedWordId)
        {
            currentWord = dropped;
            
            // center the DraggableItem inside of DropSlot
            RectTransform rect = dropped.GetComponent<RectTransform>();
            dropped.transform.SetParent(transform, false);
            rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(200, 75);
            dropped.transform.localScale = Vector3.one;

            dropped.SetMatched();
            SetMatched();
            
            matchGame.OnCorrectMatch(ExpectedWordId, this);   
            matchGame.CheckAllMatched();
        }
        else
        {
            dropped.ReturnToStart();
        }
    }

    public DraggableItemV2 CurrentWord => currentWord;

    public void SetMatched()
    {
        IsMatched = true;
        slotImage.color = matchedColor;
    }

    // for SoundToPicture
    public void SetCurrentWord(GameObject wordObject)
    {
        currentWord = null;
        IsMatched = true;
        slotImage.color = matchedColor;

        wordObject.SetActive(true);
    }
}
