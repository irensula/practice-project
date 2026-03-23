using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundToPictureGameV2 : BaseMatchGameV2
{
    [Header("Card Prefabs")]
    public SoundCardV2 soundPrefab;
    public ImageCardV2 imagePrefab;
    public DropSlotV2 slotPrefab;
    public WordSoundCard textPrefab;
    private Dictionary<int, WordSoundCard> textCards = new Dictionary<int, WordSoundCard>();
    private Dictionary<int, SoundCardV2> soundCards = new Dictionary<int, SoundCardV2>();
    
    protected override void BuildBoard()
    {
        ClearContainers();
        textCards.Clear();

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

            soundCards[word.id] = soundCard;
        }

        foreach (var word in imageWords)
        {
            var imageCard = Instantiate(imagePrefab, secondaryContainer);
            imageCard.Setup(word.id, this);            

            var slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(word.id, this);

            var textCard = Instantiate(textPrefab, resultContainer);
            textCard.Setup(word.id, this);
            textCard.gameObject.SetActive(false);

            textCards[word.id] = textCard;
        }
    }

    public override void OnCorrectMatch(int wordId, DropSlotV2 slot)
    {
        // correct/wrong icon and sound
        base.OnCorrectMatch(wordId, slot);

        // change soundCard on textCard
        if (soundCards.TryGetValue(wordId, out var soundCard))
        {
            // hide soundCard
            soundCard.gameObject.SetActive(false);

            // create textCard
            var textCard = Instantiate(textPrefab, slot.transform);
            textCard.Setup(wordId, this);

            // show textCard
            var slotComponent = slot as DropSlotV2;
            slotComponent.SetCurrentWord(textCard.gameObject);
        }
    }
}
