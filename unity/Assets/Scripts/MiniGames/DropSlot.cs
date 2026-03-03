using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int ExpectedWordId;
    private DraggableItem currentWord;
    private MatchGame matchGame;

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

        Debug.Log("Dropped word id: " + dropped.WordId + 
              " | Expected: " + ExpectedWordId);

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
            matchGame.ShowCorrect();
            matchGame.CheckAllMatched();
        }
        else
        {
            dropped.ReturnToStart();
        }
    }

    public DraggableItem CurrentWord => currentWord;
}

// using UnityEngine; using UnityEngine.EventSystems; public class DropSlot : MonoBehaviour, IDropHandler { public int ExpectedWordId; private DraggableItem currentWord; public void Setup(int wordId) { ExpectedWordId = wordId; } public void OnDrop(PointerEventData eventData) { DraggableItem dropped = eventData.pointerDrag.GetComponent<DraggableItem>(); if (dropped == null) return; Debug.Log("Dropped word id: " + dropped.WordId + " | Expected: " + ExpectedWordId); if (dropped.WordId == ExpectedWordId) { currentWord = dropped; dropped.transform.SetParent(transform, false); dropped.transform.localPosition = Vector3.zero; dropped.SetMatched(); FindObjectOfType<MatchGame>().CheckAllMatched(); } else { dropped.ReturnToStart(); } } }
