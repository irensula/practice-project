using UnityEngine;
using UnityEngine.UI;

public class SoundToPictureGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public SoundCardV2 soundPrefab;
    public ImageCardV2 imagePrefab;
    public DropSlotV2 slotPrefab;
    
    protected override void BuildBoard()
    {
        ClearContainers();

        foreach (var word in words)
        {
            var soundCard = Instantiate(soundPrefab, primaryContainer);
            soundCard.Setup(word.id, this);

            var imageCard = Instantiate(imagePrefab, secondaryContainer);
            imageCard.Setup(word.id, this);            

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);
        }
    }
}
