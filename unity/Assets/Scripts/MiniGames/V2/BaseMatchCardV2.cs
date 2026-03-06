using UnityEngine;
using UnityEngine.UI;

public class BaseMatchCardV2 : MonoBehaviour
{
    public int WordId { get; private set; }
    protected BaseMatchGameV2 game;

    public virtual void Setup(int wordId, BaseMatchGameV2 game)
    {
        WordId = wordId;
        this.game = game;
    }
}