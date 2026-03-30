using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;

public static class DatabaseService
{
    private static string dbPath;
    public static Database CurrentDatabase { get; private set; }

    // Инициализация
    public static void Init(MonoBehaviour coroutineOwner = null, Action onLoaded = null)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (coroutineOwner != null)
        {
            coroutineOwner.StartCoroutine(LoadWebGL("db.json", onLoaded));
        }
#else
        dbPath = Path.Combine(Application.streamingAssetsPath, "db.json");

        if (!File.Exists(dbPath))
        {
            Database emptyDb = CreateEmptyDatabase();
            Save(emptyDb);
        }

        string json = File.ReadAllText(dbPath);
        CurrentDatabase = JsonUtility.FromJson<Database>(json);
        if (CurrentDatabase == null)
        {
            Debug.LogWarning("Parsed DB is null, creating empty database");
            CurrentDatabase = CreateEmptyDatabase();
        }
        onLoaded?.Invoke();
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private static IEnumerator LoadWebGL(string fileName, Action onLoaded)
    {
        string url = Application.streamingAssetsPath + "/" + fileName;
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load database: " + www.error);
                CurrentDatabase = CreateEmptyDatabase();
            }
            else
            {
                string json = www.downloadHandler.text;
                Debug.Log("Database loaded:\n" + json);
                CurrentDatabase = JsonUtility.FromJson<Database>(json);
            }
        }
        onLoaded?.Invoke();
    }
#endif

    public static void Save(Database db)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        string newJson = JsonUtility.ToJson(db, true);
        File.WriteAllText(dbPath, newJson);
        CurrentDatabase = db;
#else
        Debug.LogWarning("Save not supported in WebGL!");
#endif
    }

    // this method is for compatibility
    public static Database Load()
    {
        if (CurrentDatabase == null)
            CurrentDatabase = CreateEmptyDatabase();
        return CurrentDatabase;
    }

    private static Database CreateEmptyDatabase()
    {
        return new Database
        {
            users = new UserData[0],
            languages = new LanguageData[0],
            courses = new CourseData[0],
            lessons = new LessonData[0]
        };
    }
}