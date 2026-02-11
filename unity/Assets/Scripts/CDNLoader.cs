using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.Timeline;
public class CDNLoader : MonoBehaviour
{
    public string imageUrl = "https://cdn.jsdelivr.net/gh/irensula/practice-unity-game-assets@main/images/character.png";
    public string backgroundMusicUrl = "https://cdn.jsdelivr.net/gh/irensula/practice-unity-game-assets@main/audio/background-lofi.mp3";
    public Renderer targetRenderer;
    public AudioSource musicSource;

    void Start()
    {
        StartCoroutine(LoadImage(imageUrl));    
        StartCoroutine(LoadMusic(backgroundMusicUrl));
    }

    IEnumerator LoadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(request);
            targetRenderer.material.mainTexture = tex;
        }
        else
        {
            Debug.LogError("Image load fail: " + request.error);
        }
    }

    IEnumerator LoadMusic(string url)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Audio load failed: " + request.error);
        }
    }
}
