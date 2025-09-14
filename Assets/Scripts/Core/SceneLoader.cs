using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class SceneLoader
{
    public static IEnumerator LoadAsync(string sceneName, System.Action<float> onProgress = null)
    {
        var op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            float p = Mathf.Clamp01(op.progress / 0.9f);
            onProgress?.Invoke(p);

            if (op.progress >= 0.9f) 
                op.allowSceneActivation = true;

            yield return null;
        }
    }
}
