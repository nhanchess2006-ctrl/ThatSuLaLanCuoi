using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour, IPointerDownHandler
{
    // ================= CONFIG =================
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("UI Visuals - Images")]
    [SerializeField] private Image buttonImage;       
    [SerializeField] private Sprite musicOnSprite;     
    [SerializeField] private Sprite musicOffSprite;    

    [Header("UI Visuals - Các Object Chữ (Theo Hierarchy)")]
    [SerializeField] private GameObject textOnObject;  // Kéo Object 'Text_MusicOn' vào đây
    [SerializeField] private GameObject textOffObject; // Kéo Object 'Text_MusicOff' vào đây

    [Header("Scene Settings")]
    [SerializeField] private string introSceneName = "IntroScene";

    // ================= STATE =================
    private bool isMusicPlaying = true;
   
    // ================= UNITY =================
    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (buttonImage == null)
            buttonImage = GetComponent<Image>();
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
            StopMusic();
    }

    // ================= INPUT =================
    public void OnPointerDown(PointerEventData eventData)
    {
        ToggleMusic();
    }

    // ================= CONTROL =================
    private void ToggleMusic()
    {
        isMusicPlaying = !isMusicPlaying;
        ApplyMusicState();
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }

    private void ApplyMusicState()
    {
        if (isMusicPlaying)
        {
            if (audioSource != null && !audioSource.isPlaying)
                audioSource.Play();
            
            // 1. Đổi ảnh nút thành Loa Bật
            if (buttonImage != null && musicOnSprite != null)
                buttonImage.sprite = musicOnSprite;

            // 2. Hiện chữ ON, ẩn chữ OFF
            if (textOnObject != null) textOnObject.SetActive(true);
            if (textOffObject != null) textOffObject.SetActive(false);
        }
        else
        {
            StopMusic();

            // 1. Đổi ảnh nút thành Loa Tắt
            if (buttonImage != null && musicOffSprite != null)
                buttonImage.sprite = musicOffSprite;

            // 2. Ẩn chữ ON, hiện chữ OFF
            if (textOnObject != null) textOnObject.SetActive(false);
            if (textOffObject != null) textOffObject.SetActive(true);
        }
    }
}
