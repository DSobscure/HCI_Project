using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    public void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
