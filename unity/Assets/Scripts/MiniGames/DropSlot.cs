using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int ExpectedWordId;
    private WordCard currentWord;

    public void Setup(int wordId)
    {
        ExpectedWordId = wordId;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop triggered on: " + gameObject.name);

        WordCard dropped = eventData.pointerDrag.GetComponent<WordCard>();

        if (dropped == null)
            return;

        Debug.Log("Dropped word id: " + dropped.WordId + 
              " | Expected: " + ExpectedWordId);

        if (dropped.WordId == ExpectedWordId)
        {
            currentWord = dropped;
            dropped.transform.SetParent(transform);
            dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            dropped.SetMatched();

            FindObjectOfType<MatchGame>().CheckAllMatched();
        }
        else
        {
            dropped.ReturnToStart();
        }
    }

    void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered slot: " + gameObject.name);
    }
}
