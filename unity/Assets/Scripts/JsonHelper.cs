using System;
using UnityEngine;
using System.Collections.Generic;

public static class JsonHelper
{
    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }

    public static List<T> FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return new List<T>(wrapper.array);
    }
}
