using UnityEngine;
using System;
using Unity.VisualScripting;

[System.Serializable]
public class Database
{
    public UserData[] users;
    public LanguageData[] languages;
    public CourseData[] courses;
    public LessonData[] lessons;
}
