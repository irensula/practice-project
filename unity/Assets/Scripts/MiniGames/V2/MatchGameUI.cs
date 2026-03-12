using UnityEngine;
using UnityEngine.UI;

public class MatchGameUI : MonoBehaviour
{
    [Header("Result UI")]
    public GameObject ResultPanel;
    public Image resultIcon;
    public Sprite correctSprite;
    public Sprite wrongSprite;
    public PopAnimationV2 resultPop;
    public GameObject blockerPanel;

    [Header("Win UI")]
    public GameObject winPanel;
    public Button btnCloseWinPanel;

    [Header("Result Sounds")]
    public AudioClip correctClip;
    public AudioClip wrongClip;
    public AudioClip winClip;
}
