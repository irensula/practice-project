using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


[System.Serializable]
public class VocabularyItem
{
    public int id;
    public string word;
}
public class VocabularyUI : MonoBehaviour
{
    public string url = "http://localhost:3000/vocabulary";
    public GameObject cardPrefab;
    public Transform contentParent;
    void Start()
    {
        StartCoroutine(LoadVocabulary());
    }

    IEnumerator LoadVocabulary()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Request error: " + request.error);
            }
            else
            {
                VocabularyItem[] words = JsonHelper.FromJson<VocabularyItem>(request.downloadHandler.text);

                foreach (var w in words)
                {
                    GameObject card = Instantiate(cardPrefab, contentParent);
                    Debug.Log("Word: " + w.word);
                    TMP_Text wordText = card.transform.Find("WordText").GetComponent<TMP_Text>();
                    if (wordText != null)
                        wordText.text = w.word;
                }
            }
        }
    }
}

// Helper class for deserializing a JSON array
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        json = "{\"Items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}