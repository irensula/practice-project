using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundToPictureGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public SoundCardV2 soundPrefab;
    public ImageCardV2 imagePrefab;
    public DropSlotV2 slotPrefab;
    
    protected override void BuildBoard()
    {
        ClearContainers();

        var soundWords = new List<WordData>(words);
        Shuffle(soundWords);

        var imageWords = new List<WordData>(words);
        Shuffle(imageWords);

        foreach (var word in soundWords)
        {
            var soundCard = Instantiate(soundPrefab, primaryContainer);
            soundCard.Setup(word.id, this);
            var draggeble = soundCard.gameObject.AddComponent<DraggableItemV2>(); 
            draggeble.Init(soundCard);
        }

        foreach (var word in imageWords)
        {
            var imageCard = Instantiate(imagePrefab, secondaryContainer);
            imageCard.Setup(word.id, this);            

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);
        }
    }
}
