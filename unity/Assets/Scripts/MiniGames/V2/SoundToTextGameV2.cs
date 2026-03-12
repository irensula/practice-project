using Microsoft.VisualBasic;
using UnityEngine;
using System.Collections.Generic;

public class SoundToTextGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public WordCardV2 textPrefab;
    public SoundCardV2 soundPrefab;
    public DropSlotV2 slotPrefab;

    protected override void BuildBoard()
    {
        ClearContainers();

        var soundWords = new List<WordData>(words);
        Shuffle(soundWords);
        
        var textWords = new List<WordData>(words);
        Shuffle(textWords);

        foreach (var word in soundWords)
        {
            var soundCard = Instantiate(soundPrefab, primaryContainer);
            soundCard.Setup(word.id, this);
            var draggable = soundCard.gameObject.AddComponent<DraggableItemV2>();
            draggable.Init(soundCard);
        }

        foreach (var word in textWords)
        {
            var textCard = Instantiate(textPrefab, secondaryContainer);
            textCard.Setup(word.id, this);           

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);
        }
    }
}
