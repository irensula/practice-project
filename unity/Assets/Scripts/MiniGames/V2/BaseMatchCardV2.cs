using UnityEngine;
using UnityEngine.UI;

public class BaseMatchCardV2 : MonoBehaviour
{
    public int WordId { get; private set; }
    public bool IsMatched { get; private set; }
    protected BaseMatchGameV2 game;
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        // if (button != null)
        //     button.onClick.AddListener(OnClicked);
    }

    // reference to BaseMatchGameV2 for DraggableItem
    public BaseMatchGameV2 BaseMatchGameV2 => game;

    public virtual void Setup(int wordId, BaseMatchGameV2 game)
    {
        WordId = wordId;
        this.game = game;
    }

    public virtual void SetSelected(bool value)
    {
        // transform.localScale = value ? Vector3.one * 1.1f : Vector3.one;
        Debug.Log("SetSelected is called");
    }

    public virtual void SetMatched()
    {
        // IsMatched = true;
        // button.interactable = false;
        // transform.localScale = Vector3.one;
        Debug.Log("SetMatched is called");
    }
}