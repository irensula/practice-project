using UnityEngine;
using UnityEngine.EventSystems;

public class SoundCard : BaseMatchCard
{
    private AudioClip clip;

    public void SetSound(AudioClip clip)
    {
        this.clip = clip;
    }

    public override void SetSelected(bool value)
    {
        base.SetSelected(value);

        if (value && clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}

