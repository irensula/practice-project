using UnityEngine;
using System;

[System.Serializable]
public class LessonData
{
    public int lessonID;
    public int courseID;
    public string title;

    public VisualNovelData[] visualNovel;
    public ExerciseData[] exercises;
    public MiniGameData[] miniGames;
}

[System.Serializable]
public class VisualNovelData
{
    public int contentID;
    public string text;
}

[System.Serializable]
public class ExerciseData
{
    public int exerciseID;
    public string type;
    public string question;
    public string answer;
}

[System.Serializable]
public class MiniGameData
{
    public int miniGameID;
    public string title;
}