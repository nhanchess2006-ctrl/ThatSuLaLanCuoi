using UnityEngine;
using UnityEngine.SceneManagement;

// [RequireComponent(typeof(AudioSource))]
public class Tanh_AudioManager : MonoBehaviour
{
    public static Tanh_AudioManager instance;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Scene Settings")]
    [SerializeField] private string introSceneName = "Intro";

    private bool isMusicOn = true;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ApplyMusicState();
    }

    // ================= SCENE =================
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != introSceneName)
        {
            StopMusic();
        }
    }

    // ================= CONTROL =================
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        ApplyMusicState();
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void ApplyMusicState()
    {
        if (isMusicOn)
        {
            if (audioSource != null && !audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            StopMusic();
        }
    }
}