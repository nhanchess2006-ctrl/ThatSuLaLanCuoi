using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel2 : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string sceneName = "Level2";

    public void OnClickLoadScene()
    {
        Time.timeScale = 1f;

        // Gọi MusicManager (nếu muốn stop ngay lập tức)
        if (Tanh_AudioManager.instance != null)
        {
            Tanh_AudioManager.instance.StopMusic();
        }

        SceneManager.LoadScene(sceneName);
    }
}