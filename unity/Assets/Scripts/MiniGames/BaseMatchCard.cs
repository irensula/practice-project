using UnityEngine;
using UnityEngine.UI;

public class BaseMatchCard : MonoBehaviour
{
    public int WordId { get; private set; }
    public bool IsMatched { get; private set; }

    protected MatchGame game;
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClicked);
    }

    public void Setup(int wordId, MatchGame game)
    {
        this.WordId = wordId;
        this.game = game;
    }

    private void OnClicked()
    {
        if (IsMatched) return;
        game.SelectCard(this);
    }

    public virtual void SetSelected(bool value)
    {
        transform.localScale = value ? Vector3.one * 1.1f : Vector3.one;
    }

    public virtual void SetMatched()
    {
        IsMatched = true;
        button.interactable = false;
        transform.localScale = Vector3.one;
    }
}
