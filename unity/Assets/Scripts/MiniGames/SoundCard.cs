using UnityEngine;

public class SoundCard : BaseMatchCard
{
    private string wordKey;
    private AudioClip clip;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
        audioSource.volume = 1f;
    }
    public void SetupSound(int wordId, string wordKey, AudioClip clip, MatchGame game)
    {
        base.Setup(wordId, game);
        this.wordKey = wordKey.ToLower();
        this.clip = clip;
    }

    private void PlaySound()
    {
        Debug.Log($"PlaySound called for {wordKey}");
        if (clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"SoundCard: clip not set for word {wordKey}");
        }
    }
    public override void SetSelected(bool value)
    {
        base.SetSelected(value);

        if (value)
        {
            PlaySound();
        }
    }
}

