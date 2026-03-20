using Microsoft.VisualBasic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class SoundToTextGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public WordCardV2 textPrefab;
    public SoundCardV2 soundPrefab;
    public DropSlotV2 slotPrefab;
    public ImageCardV2 imagePrefab;

    private Dictionary<int, ImageCardV2> imageCards = new Dictionary<int, ImageCardV2>();

    protected override void BuildBoard()
    {
        ClearContainers();

        var soundWords = new List<WordData>(words);
        Shuffle(soundWords);
        
        var textWords = new List<WordData>(words);
        Shuffle(textWords);

        for (int i = 0; i < soundWords.Count; i++)
        {
            var word = soundWords[i];

            // Sound card
            var soundCard = Instantiate(soundPrefab, primaryContainer);
            soundCard.Setup(word.id, this);

            // Drop slot
            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);

            // make imageCard active but transparent
            var imageCard = Instantiate(imagePrefab, resultContainer);
            imageCard.Setup(word.id, this);

            SetAllImagesAlpha(imageCard.gameObject, 0f);

            imageCard.transform.SetSiblingIndex(i); // under the corresponding word
            imageCards[word.id] = imageCard;
        }
         for (int i = 0; i < textWords.Count; i++)
        {
            var word = textWords[i];

            var textCard = Instantiate(textPrefab, secondaryContainer);
            textCard.Setup(word.id, this);

            var draggable = textCard.gameObject.AddComponent<DraggableItemV2>();
            draggable.Init(textCard);
        }
        RebuildLayoutDelayed();
    }

    public override void OnCorrectMatch(int wordId, DropSlotV2 slot)
    {
        base.OnCorrectMatch(wordId, slot);

        if (imageCards.TryGetValue(wordId, out var imageCard))
        {
            SetAllImagesAlpha(imageCard.gameObject, 1f);
        }
    }

    private void SetAllImagesAlpha(GameObject obj, float alpha)
    {
        var images = obj.GetComponentsInChildren<Image>(true);
        foreach (var img in images)
        {
            Color c = img.color;
            img.color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}
