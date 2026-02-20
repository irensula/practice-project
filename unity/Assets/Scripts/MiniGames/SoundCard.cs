using UnityEngine;
using UnityEngine.EventSystems;

public class SoundCard : MonoBehaviour, IPointerClickHandler
{
    private int wordId;
    private MatchGame manager;

    [SerializeField] private AudioSource audioSource;
    public void Setup(int id, MatchGame gameManager)
    {
        wordId = id;
        manager = gameManager;
    }

    public void SetSound(AudioClip clip)
    {
        if (audioSource != null)
            audioSource.clip = clip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null)
            audioSource.Play();

        manager?.OnCardSelected(wordId);
    }
}

