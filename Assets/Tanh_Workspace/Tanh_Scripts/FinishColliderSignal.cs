using UnityEngine;
using UnityEngine.Playables; // Bắt buộc để điều khiển Timeline

public class FinishColliderSignal : MonoBehaviour
{
    [Header("Cấu hình Timeline")]
    [Tooltip("Kéo cái Timeline của màn chơi vào đây")]
    [SerializeField] private PlayableDirector playableDirector;

    [Header("Cấu hình Thẻ Tag")]
    [Tooltip("Nhập chính xác tên Tag của vật thể chạm vào (Ví dụ: Player)")]
    [SerializeField] private string targetTag = "Player";

    private bool hasTriggered = false; // Biến chặn để chỉ kích hoạt Timeline đúng 1 lần duy nhất

    // Hàm tự động kích hoạt khi có một vật thể thể 2D chạm vào vùng Collider (Dành cho game 2D)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem vật thể vừa chạm vào có đúng cái Tag chỉ định và hệ thống chưa bị kích hoạt lần nào không
        if (other.CompareTag(targetTag) && !hasTriggered)
        {
            hasTriggered = true; // Khóa lệnh lại để không bị kích hoạt trùng lặp nhiều lần
            TriggerResumeTimeline();
        }
    }

    // Hàm tự động kích hoạt khi có một vật thể 3D chạm vào vùng Collider (Dành cho game 3D)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !hasTriggered)
        {
            hasTriggered = true;
            TriggerResumeTimeline();
        }
    }

    // Hàm ra lệnh giải thoát cho Timeline
    private void TriggerResumeTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Play(); // Cho Timeline tiếp tục chạy mạch phim
            Debug.Log($"Vật thể có Tag '{targetTag}' đã chạm vào vùng Finish! Timeline rã băng chạy tiếp.");
        }
        else
        {
            Debug.LogError("LỖI: Bạn quên chưa kéo thả 'Playable Director' vào ô cấu hình trong Inspector!");
        }
    }
}
