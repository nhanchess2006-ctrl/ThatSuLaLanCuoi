using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic; // Bắt buộc phải có để dùng IEnumerator

public class TimelineInteractionManager : MonoBehaviour
{
    [Header("Cấu hình Timeline")]
    public PlayableDirector playableDirector;

    [Header("Cấu hình Đối Tượng Tương Tác")]
    public GameObject targetButton;         
    public GameObject objectToActivate;     

    [Header("Cấu hình Hoạt Họa NPC")]
    [SerializeField] private Animator npcAnimator; 
    [SerializeField] private string animationName; 

    [Tooltip("Tích chọn nếu bạn dùng Trigger trong Animator, bỏ tích nếu gọi trực tiếp tên Animation")]
    [SerializeField] private bool useTrigger = true; 

    public void PauseAndShowButton()
    {
        if (playableDirector == null) return;
        playableDirector.Pause();

        if (targetButton != null)
        {
            targetButton.SetActive(true);
        }
    }

    // Hàm này vẫn gán vào Sự kiện OnClick của Button như cũ
    public void OnPlayerClickButton()
    {
        // 1. Tắt nút bấm đi ngay lập tức khi click
        if (targetButton != null)
        {
            targetButton.SetActive(false);
        }

        // 2. Kích hoạt chạy Animation cho NPC trước
        if (npcAnimator != null && !string.IsNullOrEmpty(animationName))
        {
            if (useTrigger)
            {
                npcAnimator.SetTrigger(animationName);
            }
            else
            {
                npcAnimator.Play(animationName);
            }

            // 3. Khởi động hàm chờ: Đợi NPC diễn xong rồi mới bật Object và chạy tiếp Timeline
            StartCoroutine(WaitForAnimationAndActivate());
        }
        else
        {
            // Phòng trường hợp bạn quên kéo Animator, game vẫn chạy tiếp không bị kẹt
            DirectActivate();
        }
    }

    // Hàm xử lý chờ đợi ngầm của hệ thống Unity
    private System.Collections.IEnumerator WaitForAnimationAndActivate()
    {
        // Chờ 1 khung hình để Unity nạp trạng thái Animation mới vào bộ nhớ
        yield return null;

        // Tự động lấy ra độ dài thời gian thực tế (tính bằng giây) của clip Animation đang chạy
        float animationLength = npcAnimator.GetCurrentAnimatorStateInfo(0).length;

        // Debug.Log($"Đang đợi NPC diễn hoạt họa trong {animationLength} giây...");

        // Chờ đúng bằng số giây của clip ném kiếm
        yield return new WaitForSeconds(animationLength);

        // Sau khi chờ xong, thực hiện các bước còn lại
        DirectActivate();
    }

    // Hàm thực thi bật Object và chạy tiếp dòng phim
    void DirectActivate()
    {
        // Bật Object thiêu xác lên
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // Cho Timeline tiếp tục chạy mạch phim
        if (playableDirector != null)
        {
            playableDirector.Play();
        }

        // Debug.Log("NPC đã diễn xong! Đã bật Object và tiếp tục Timeline.");
    }
}
