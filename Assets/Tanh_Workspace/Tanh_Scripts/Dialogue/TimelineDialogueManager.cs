using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Timeline;

public class TimelineDialogueManager : MonoBehaviour
{
    [Header("Cấu hình Timeline")]
    public PlayableDirector playableDirector;

    [Header("Cấu hình UI Hội Thoại (Chữ nói)")]
    public GameObject dialoguePanel;     
    public TextMeshProUGUI textContent;  
    
    [Header("Cấu hình UI Ảnh & Khung Tên (Mới)")]
    [SerializeField] private GameObject avatarPanelObj;
    [SerializeField] private Image avatarImage;            // Kéo UI Image đại diện vào đây
    [SerializeField] private GameObject namePanelObj;     // Kéo NGUYÊN CÁI PANEL KHUNG CHỨA TÊN vào đây
    [SerializeField] private TextMeshProUGUI textNameObj;  // Kéo UI Text chữ Tên (nằm trong Panel) vào đây

    private string[] dynamicLines;           
    private int currentLineIndex = 0;    
    private bool isDialogueActive = false;

    void Update()
    {
        if (isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                DisplayNextLine();
            }
        }
    }

    public void StartMultiDialogueFromSignal(SignalAsset signal)
    {
        DialogueSignalAsset dialogueSignal = signal as DialogueSignalAsset;

        if (dialogueSignal != null)
        {
            // 1. XỬ LÝ KHUNG TÊN VÀ CHỮ TÊN NHÂN VẬT:
            if (namePanelObj != null)
            {
                if (!string.IsNullOrEmpty(dialogueSignal.characterName))
                {
                    // Nếu có gõ tên -> Bật cả cái Panel Khung Tên lên
                    namePanelObj.SetActive(true);
                    
                    // Điền chữ vào ô Text bên trong khung
                    if (textNameObj != null)
                    {
                        textNameObj.text = dialogueSignal.characterName;
                    }
                }
                else
                {
                    // Nếu để trống tên (Lời dẫn truyện) -> Tắt nguyên cái Panel Khung Tên đi
                    namePanelObj.SetActive(false);
                }
            }

            // 2. XỬ LÝ KHUNG AVATAR VÀ ẢNH ĐẠI DIỆN:
            if (avatarPanelObj != null)
            {
                if (dialogueSignal.characterAvatar != null)
                {
                    // Nếu ghim này có gắn ảnh nhân vật -> Bật nguyên cụm Khung Avatar lên
                    avatarPanelObj.SetActive(true);

                    // Gắn ảnh vào linh kiện UI Image bên trong khung
                    if (avatarImage != null)
                    {
                        avatarImage.sprite = dialogueSignal.characterAvatar;
                    }
                }
                else
                {
                    // Nếu ghim này để trống ảnh (Lời dẫn truyện) -> Tự động ẩn cả cụm Khung Avatar đi cho đẹp
                    avatarPanelObj.SetActive(false);
                }
            }

            // 3. XỬ LÝ NỘI DUNG CHỮ NÓI NHƯ CŨ:
            string combinedLines = dialogueSignal.combinedLines;
            if (!string.IsNullOrEmpty(combinedLines))
            {
                dynamicLines = combinedLines.Split('|');
                InitDialogue();
            }
        }
    }

    void InitDialogue()
    {
        if (playableDirector != null) playableDirector.Pause(); 
        if (dialoguePanel != null) dialoguePanel.SetActive(true); 
        
        isDialogueActive = true;
        currentLineIndex = 0;
        ShowLine(currentLineIndex);
    }

    void ShowLine(int index)
    {
        if (textContent != null) textContent.text = dynamicLines[index];
    }

    void DisplayNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dynamicLines.Length)
        {
            ShowLine(currentLineIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    public void SkipAllDialogues()
    {
        EndDialogue();
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        if (dialoguePanel != null) dialoguePanel.SetActive(false); 
        if (playableDirector != null) playableDirector.Play(); 
    }
}
