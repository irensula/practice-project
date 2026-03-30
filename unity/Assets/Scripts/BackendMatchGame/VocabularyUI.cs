using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;

[System.Serializable]
public class VocabularyItem
{
    public int id;
    public string word;
    public string image;
    public string audio;
}
public class VocabularyUI : MonoBehaviour
{
    public string url = "http://localhost:3001/match-game/lang/fi";
    private string imageBaseUrl = "http://localhost:3001/cdn-assets/";
    public GameObject wordPrefab;
    public GameObject imagePrefab;
    public Sprite doneSprite;
    public Sprite wrongSprite;
    
    public Image resultIcon;
    public GameObject winPanel;
    public Transform wordsRow;
    public Transform imagesRow;
    private WordItem selectedWord;
    private ImageItem selectedImage;
    public Button btnCloseWinPanel;
    public TMP_Text txtTitle;

    void Start()
    {
        StartCoroutine(LoadVocabulary());
        btnCloseWinPanel.onClick.AddListener(CloseWinPanel);
        resultIcon.gameObject.SetActive(false);
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    IEnumerator LoadVocabulary()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            
            if(request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                
                List<VocabularyItem> items = JsonHelper.FromJson<VocabularyItem>(json);

                items = items.OrderBy(x => Random.value).Take(8).ToList(); // take 8 random words and images

                // make copies of words and images
                List<VocabularyItem> shuffledWords = new List<VocabularyItem>(items);
                List<VocabularyItem> shuffledImages = new List<VocabularyItem>(items);
                
                // shuffle words and images
                Shuffle(shuffledWords);
                Shuffle(shuffledImages);

                foreach (var item in shuffledWords)
                {
                    CreateWord(item);
                }
                foreach (var item in shuffledImages)
                {
                    yield return StartCoroutine(CreateImage(item));
                }
                yield return null;
                LayoutRebuilder.ForceRebuildLayoutImmediate(wordsRow.GetComponent<RectTransform>());
                LayoutRebuilder.ForceRebuildLayoutImmediate(imagesRow.GetComponent<RectTransform>());
            }
            else
            {
                txtTitle.text = "To play this game requires an active connection to the database";
                Debug.LogError(request.error);
            }
        }
    }

    void CreateWord(VocabularyItem item)
    {
        GameObject obj = Instantiate(wordPrefab, wordsRow);
        WordItem wordItem = obj.GetComponent<WordItem>();
        string audioFullUrl = "http://localhost:3001/cdn-assets/" + item.audio;

        wordItem.Setup(item.id, item.word, this, audioFullUrl);
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
            ImageItem imageItem = obj.GetComponent<ImageItem>();
            imageItem.Setup(item.id, sprite, this);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    public void SelectWord(WordItem word)
    {
        if (selectedWord != null)
            selectedWord.SetSelected(false);

        selectedWord = word;
        selectedWord.SetSelected(true);
        TryMatch();
    }
    public void SelectImage(ImageItem image)
    {
        if (selectedImage != null)
            selectedImage.SetSelected(false);
        
        selectedImage = image;
        selectedImage.SetSelected(true);

        TryMatch();
    }

    void TryMatch()
    {
        if (selectedWord != null && selectedImage != null)
        {
            Debug.Log($"TryMatch: Word {selectedWord.id}, Image {selectedImage.id}");
            if (selectedWord.id == selectedImage.id)
            {
                selectedWord.SetMatched();
                selectedImage.SetMatched();
                
                StartCoroutine(ShowResult(doneSprite));

                CheckAllMatched();
            }
            else
            {
                Debug.Log("No match");
                selectedWord.SetSelected(false);
                selectedImage.SetSelected(false);
                
                StartCoroutine(ShowResult(wrongSprite));
            }
            
            selectedWord = null;
            selectedImage = null;
        }
    }

    void CheckAllMatched()
    {
        WordItem[] words = wordsRow.GetComponentsInChildren<WordItem>();
        ImageItem[] images = imagesRow.GetComponentsInChildren<ImageItem>();

        bool allMatched = true;

        foreach (var w in words)
            if (!w.IsMatched()) allMatched = false;

        foreach (var i in images)
            if (!i.IsMatched()) allMatched = false;

        if (allMatched)
        {
            StartCoroutine(ShowWinPanel());
        }
    }

    IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(1.5f);
        winPanel.SetActive(true);
    }

    public void CloseWinPanel()
    {
        winPanel.SetActive(false);

        WordItem[] words = wordsRow.GetComponentsInChildren<WordItem>();
        foreach (var w in words)
            w.ResetItem();

        ImageItem[] images = imagesRow.GetComponentsInChildren<ImageItem>();
        foreach (var i in images)
            i.ResetItem();
    }

    IEnumerator ShowResult(Sprite sprite)
    {
        resultIcon.sprite = sprite;
        resultIcon.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        resultIcon.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("LessonScene");
    } 
}