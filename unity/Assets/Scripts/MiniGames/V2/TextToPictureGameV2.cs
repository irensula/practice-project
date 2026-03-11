using UnityEngine;

public class TextToPictureGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public WordCardV2 textPrefab;
    public ImageCardV2 imagePrefab;
    public DropSlotV2 slotPrefab;

    protected override void BuildBoard()
    {
        ClearContainers();

        foreach (var word in words)
        {
            Debug.Log("Building card for word id: " + word.id);

            var textCard = Instantiate(textPrefab, primaryContainer);
            textCard.Setup(word.id, this);

            var imageCard = Instantiate(imagePrefab, secondaryContainer);
            imageCard.Setup(word.id, this);

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);
        }
    }
}
