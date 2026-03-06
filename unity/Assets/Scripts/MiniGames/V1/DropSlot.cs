using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int ExpectedWordId;
    private DraggableItem currentWord;
    private MatchGame matchGame;
    private Image slotImage;
    public bool IsMatched { get; private set; }
    [SerializeField] private Color matchedColor = new Color32(131, 106, 234, 50);

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void Setup(int wordId, MatchGame game)
    {
        ExpectedWordId = wordId;
        matchGame = game;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dropped = eventData.pointerDrag.GetComponent<DraggableItem>();

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

            matchGame.ShowCorrect();
            matchGame.CheckAllMatched();
        }
        else
        {
            dropped.ReturnToStart();
        }
    }

    public DraggableItem CurrentWord => currentWord;

    public void SetMatched()
    {
        IsMatched = true;
        slotImage.color = matchedColor;
    }
}