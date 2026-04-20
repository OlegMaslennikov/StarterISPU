using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger
{
    public bool ChangeScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError($"Scene with index {sceneIndex} is not included in Build Settings!");
            return false;
        }

        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            Debug.LogWarning($"Already in scene with index {sceneIndex}");
            return false;
        }
        SceneManager.LoadScene(sceneIndex);
        return true;
    }

    public bool ChangeScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name cannot be null or empty!");
            return false;
        }
        SceneManager.LoadScene(sceneName);
        return true;
    }
}
