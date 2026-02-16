from gtts import gTTS

words = [
  'food',         
  'menu',         
  'table',        
  'waiter',       
  'customer',     
  'order',        
  'restaurant',   
  'coffee shop',  
  'tray',         
  'plate'         
];

language = "en-GB"
for word in words:
    filename = f"{word}.mp3"
    tts = gTTS(text=word, lang=language)
    tts.save(filename)
    print(f"Saved {filename}")