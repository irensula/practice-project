using UnityEngine;
using System.IO;

public class DatabaseService
{
    private static string dbPath;

    public static void Init()
    {
        dbPath = Path.Combine(Application.streamingAssetsPath, "db.json");

        if (!File.Exists(dbPath))
        {
            Database emptyDb = CreateEmptyDatabase();
            Save(emptyDb);
        }
    }

    public static Database Load()
    {
        if (!File.Exists(dbPath))
        {
            Debug.LogWarning("db.json not found, creating empty database.");
            return CreateEmptyDatabase();
        }
            
        string json = File.ReadAllText(dbPath);
        Debug.Log("JSON loaded" + json);

        Database db = JsonUtility.FromJson<Database>(json);
        return Normalize(db);
    }
    
    public static void Save(Database db)
    {
        string newJson = JsonUtility.ToJson(db, true);
        File.WriteAllText(dbPath, newJson);
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

    private static Database Normalize(Database db)
    {
        if (db == null) return CreateEmptyDatabase();

        db.users ??= new UserData[0];
        db.languages ??= new LanguageData[0];
        db.courses ??= new CourseData[0];
        db.lessons ??= new LessonData[0];

        return db;
    }
}
