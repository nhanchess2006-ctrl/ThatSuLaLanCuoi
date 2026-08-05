using UnityEngine;
using UnityEngine.Playables; // Bắt buộc để điều khiển Timeline

public class BossController : MonoBehaviour
{
    [Header("Cấu hình Timeline Cắt Cảnh")]
    [SerializeField] private PlayableDirector playableDirector; // Kéo Timeline của bạn vào đây

    // Hàm này được gọi khi Boss bị hết máu
    public void OnBossDeath()
    {
        Debug.Log("Boss đã bị hạ gục! Đang chuẩn bị chạy tiếp Cutscene kết thúc...");

        // 1. Thực hiện các hiệu ứng nổ tung, rơi đồ của Boss tại đây...
        
        // 2. CÂU LỆNH THẦN KỲ: Ra lệnh cho Timeline rã băng và tiếp tục chạy các giây sau đó
        if (playableDirector != null)
        {
            playableDirector.Play(); 
        }

        // 3. Ẩn hoặc hủy Object Boss sau khi đã kích hoạt Timeline thành công
        this.gameObject.SetActive(false); 
    }
}
