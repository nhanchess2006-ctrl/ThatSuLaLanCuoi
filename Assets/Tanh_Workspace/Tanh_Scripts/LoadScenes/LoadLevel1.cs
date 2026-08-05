using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1 : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string sceneName = "Level1";

    public void OnClickLoadScene()
    {
        Time.timeScale = 1f;

        if (Tanh_AudioManager.instance != null)
        {
            Tanh_AudioManager.instance.StopMusic();
        }

        SceneManager.LoadScene(sceneName);
    }
}