using UnityEngine;
using System.Collections.Generic;

public class TextToPictureGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public WordCardV2 textPrefab;
    public ImageCardV2 imagePrefab;
    public DropSlotV2 slotPrefab;

    protected override void BuildBoard()
    {
        ClearContainers();

        var textWords = new List<WordData>(words);
        Shuffle(textWords);
        
        var imageWords = new List<WordData>(words);
        Shuffle(imageWords);

        // create text cards
        foreach (var word in textWords)
        {
            var textCard = Instantiate(textPrefab, primaryContainer);
            textCard.Setup(word.id, this);
            
            var draggable = textCard.gameObject.AddComponent<DraggableItemV2>();
            draggable.Init(textCard);
        }

        // create image cards
        foreach (var word in imageWords)
        {
            var imageCard = Instantiate(imagePrefab, secondaryContainer);
            imageCard.Setup(word.id, this);

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);
        }
    }
}
