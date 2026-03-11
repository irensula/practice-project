using UnityEngine;
using UnityEngine.UI;

public class BaseMatchCardV2 : MonoBehaviour
{
    public int WordId { get; private set; }
    protected BaseMatchGameV2 game;
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        // if (button != null)
        //     button.onClick.AddListener(OnClicked);
    }

    public virtual void Setup(int wordId, BaseMatchGameV2 game)
    {
        WordId = wordId;
        this.game = game;
    }
}