using UnityEngine.UI;
using UnityEngine;

public class VolumeToggle : MonoBehaviour
{
    public Image icon;
    public Sprite volumeOn;
    public Sprite volumeOff;

    private bool isMuted;
    
    // load saved state
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        ApplyState();
    }

    // toggle the state
    public void Toggle()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Unmute was clicked");

        ApplyState();
    }

    // change the state
    public void ApplyState()
    {
        AudioListener.volume = isMuted ? 0f : 1f;
        icon.sprite = isMuted ? volumeOff : volumeOn;
        Debug.Log("Changing icon sprite to: " + (isMuted ? volumeOff.name : volumeOn.name));
    }
}
