using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;

[System.Serializable]
public class Database
{
    public UserData[] users;
    public LanguageData[] languages;
    public CourseData[] courses;
    public LessonData[] lessons;
    public WordData[] words;
}
