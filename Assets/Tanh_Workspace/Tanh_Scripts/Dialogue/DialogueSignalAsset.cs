using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "NewDialogueSignal", menuName = "Timeline/Custom Dialogue Signal")]
public class DialogueSignalAsset : SignalAsset
{
    [Header("Thông tin nhân vật")]
    public string characterName;   // Nhập tên nhân vật vào đây (Ví dụ: Lôi Đội Trưởng)
    public Sprite characterAvatar; // Ảnh đại diện nhân vật

    [Header("Nội dung lời thoại dành riêng cho ghim này")]
    [TextArea(3, 5)]
    public string combinedLines; 
}
