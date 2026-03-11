using Microsoft.VisualBasic;
using UnityEngine;

public class SoundToTextGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public WordCardV2 textPrefab;
    public SoundCardV2 soundPrefab;
    public DropSlotV2 slotPrefab;

    protected override void BuildBoard()
    {
        ClearContainers();

        foreach (var word in words)
        {
            var soundCard = Instantiate(soundPrefab, primaryContainer);
            soundCard.Setup(word.id, this);

            var textCard = Instantiate(textPrefab, secondaryContainer);
            textCard.Setup(word.id, this);           

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);
        }
    }
}
