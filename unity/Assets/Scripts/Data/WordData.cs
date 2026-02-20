using System;

[System.Serializable]
public class Translation
{
    public int languageId;
    public string text;
    public string audio;
}

[System.Serializable]
public class WordData
{
    public int id;
    public string image;
    public Translation[] translations;
}