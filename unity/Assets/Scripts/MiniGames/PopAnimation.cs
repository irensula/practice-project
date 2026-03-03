using UnityEngine;
using System.Collections;

public class PopAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float overshootScale = 1.15f;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;
        gameObject.SetActive(false);    
    }

    public void Play()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true); // turn on the object before animation

        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Pop());
    }

    private IEnumerator Pop()
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / (duration * 0.6f);
            float scale = Mathf.Lerp(overshootScale, 1f, t);
            rect.localScale = Vector3.one * scale;
            yield return null;
        }

        time = 0f;
        while (time < duration * 0.6f)
        {
            time += Time.deltaTime;
            float t = time / (duration * 0.6f);
            float scale = Mathf.Lerp(overshootScale, 1f, t);
            rect.localScale = Vector3.one * scale;   
            yield return null;
        }

        rect.localScale = Vector3.one;

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
