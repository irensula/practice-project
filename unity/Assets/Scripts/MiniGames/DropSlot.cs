using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int ExpectedWordId;
    private DraggableItem currentWord;

    public void Setup(int wordId)
    {
        ExpectedWordId = wordId;
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
            dropped.SetMatched();
            currentWord = dropped;
            dropped.transform.SetParent(transform);
            dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            

            FindObjectOfType<MatchGame>().CheckAllMatched();
        }
        else
        {
            dropped.ReturnToStart();
        }
    }
}
