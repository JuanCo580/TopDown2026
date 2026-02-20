using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.8f;
    public static SceneTransition instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(Fade(1));
    }
    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(FadeAndLoad(sceneIndex));
    }
    private IEnumerator FadeAndLoad(int sceneIndex)
    {
        yield return StartCoroutine(Fade(1));
        SceneManager.LoadScene(sceneIndex);
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(Fade(0));
    }
    private IEnumerator Fade(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, Time.deltaTime / fadeDuration);
            yield return null;
        }
    }
}