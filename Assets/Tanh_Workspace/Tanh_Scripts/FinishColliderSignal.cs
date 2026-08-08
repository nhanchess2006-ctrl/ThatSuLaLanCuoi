using UnityEngine;
using UnityEngine.Playables; // Bắt buộc để điều khiển hệ thống Timeline

public class FinishColliderSignal : MonoBehaviour
{
    [Header("Cấu hình Timeline")]
    [Tooltip("Kéo cái Component Playable Director của màn chơi vào đây")]
    [SerializeField] private PlayableDirector playableDirector;

    [Header("Cấu hình Thẻ Tag")]
    [Tooltip("Nhập chính xác tên Tag của vật thể chạm vào vạch đích (Mặc định: Player)")]
    [SerializeField] private string targetTag = "Player";

    [Header("Cấu hình Quét Dự Phòng (Sửa lỗi hụt va chạm 2D của Unity)")]
    [Tooltip("Bán kính vùng quét dự phòng xung quanh tâm vạch đích này")]
    [SerializeField] private float scanRadius = 1.5f;
    [Tooltip("Chọn chính xác Layer của Player (Ví dụ: Player) để cảm biến bắt trúng")]
    [SerializeField] private LayerMask playerLayer;

    private bool hasTriggered = false; // Biến chặn để chỉ kích hoạt Timeline đúng 1 lần duy nhất

    private void Update()
    {
        // Hệ thống quét dự phòng chủ động liên tục mỗi khung hình
        if (!hasTriggered)
        {
            // Kiểm tra xem có Collider nào thuộc Layer Player đang nằm đè lên vùng này không
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, scanRadius, playerLayer);
            
            if (playerCollider != null && playerCollider.CompareTag(targetTag))
            {
                hasTriggered = true; // Khóa lệnh lập tức
                TriggerResumeTimeline();
            }
        }
    }

    // Hàm tự động kích hoạt khi có một vật thể thể 2D chạm vào vùng Collider (Dành cho game 2D)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) && !hasTriggered)
        {
            hasTriggered = true; // Khóa lệnh lại để không bị kích hoạt trùng lặp nhiều lần
            TriggerResumeTimeline();
        }
    }

    // Hàm tự động kích hoạt khi có một vật thể 3D chạm vào vùng Collider (Dành cho game 3D phòng hờ)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !hasTriggered)
        {
            hasTriggered = true;
            TriggerResumeTimeline();
        }
    }

    // Hàm ra lệnh giải thoát và rã băng hoàn toàn cho Timeline
    private void TriggerResumeTimeline()
    {
        if (playableDirector != null)
        {
            // 1. Đảm bảo Component Timeline đã được kích hoạt hoàn toàn ngoài Hierarchy
            if (playableDirector.enabled == false)
            {
                playableDirector.enabled = true;
            }

            // 2. 🔥 CHÍ MẠNG TẠI ĐÂY: Sửa lỗi cú pháp Unity 6 (Xóa lỗi gạch đỏ CS0315)
            // Gọi SetSpeed thông qua Root Playable của Graph để rã băng bộ đếm tốc độ phim (tốc độ = 1)
            if (playableDirector.playableGraph.IsValid() && playableDirector.playableGraph.GetRootPlayableCount() > 0)
            {
                playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d); // Trả về tốc độ 1 chuẩn double
            }

            // 3. Trả Timeline về chế độ cập nhật thời gian tự động theo thời gian thực của game
            playableDirector.timeUpdateMode = DirectorUpdateMode.GameTime;
            
            // 4. Ép đồng bộ hình ảnh và chính thức ra lệnh phát tiếp mạch phim kết thúc màn
            playableDirector.Evaluate();
            playableDirector.Play(); 
            
            Debug.Log($"🎉 SUCCESS! Vật thể có Tag '{targetTag}' đã kích hoạt vạch đích. Timeline rã băng chạy tiếp mượt mà ổn định 100%!");
        }
        else
        {
            Debug.LogError("❌ LỖI: Bạn quên chưa kéo thả 'Playable Director' vào ô cấu hình trong Inspector!");
        }
    }

    // Vẽ vùng cảm biến vật lý màu xanh lá cây trong cửa sổ Scene để bạn dễ căn chỉnh độ to nhỏ của vạch đích
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
