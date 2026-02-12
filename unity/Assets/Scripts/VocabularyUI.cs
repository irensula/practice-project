using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class VocabularyItem
{
    public int id;
    public string word;
    public string image;
}
public class VocabularyUI : MonoBehaviour
{
    public string url = "http://localhost:3001/match-game/lang/fi";
    private string imageBaseUrl = "http://localhost:3001/cdn-assets/";
    public GameObject wordPrefab;
    public GameObject imagePrefab;
    public Transform wordsRow;
    public Transform imagesRow;
    void Start()
    {
        StartCoroutine(LoadVocabulary());
    }

    IEnumerator LoadVocabulary()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            Debug.Log("Requesting: " + url);
            if(request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                List<VocabularyItem> items = JsonHelper.FromJson<VocabularyItem>(json);

                foreach (var item in items)
                {
                    CreateWord(item);
                    StartCoroutine(CreateImage(item));
                }
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }

    void CreateWord(VocabularyItem item)
    {
        GameObject obj = Instantiate(wordPrefab, wordsRow);
        obj.GetComponentInChildren<TMP_Text>().text = item.word;
    }

    IEnumerator CreateImage(VocabularyItem item)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageBaseUrl + item.image);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0,0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            GameObject obj = Instantiate(imagePrefab, imagesRow);
            obj.GetComponent<Image>().sprite = sprite;
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}