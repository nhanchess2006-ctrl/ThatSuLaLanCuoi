using UnityEngine;
using UnityEngine.Playables;
using System.Collections; // Bắt buộc phải có để chạy IEnumerator

public class CutsceneInputBlocker : MonoBehaviour
{
    [Header("Cấu hình Timeline")]
    [SerializeField] private PlayableDirector playableDirector;

    [Header("Cấu hình Script Di Chuyển Của Player")]
    [SerializeField] private GameObject playerObject; 
    
    // NHỚ SỬA: Thay chữ "PlayerController" bằng tên chính xác của Script di chuyển của bạn
    private MonoBehaviour playerMovementScript; 

    void Awake()
    {
        if (playableDirector == null)
            playableDirector = GetComponent<PlayableDirector>();

        if (playerObject != null)
        {
            playerMovementScript = playerObject.GetComponent("Player") as MonoBehaviour;
        }
    }

    void Start()
    {
        // 1. Cưỡng ép khóa chân Player ngay từ milli-giây đầu tiên vào game
        if (playableDirector != null && playableDirector.playOnAwake)
        {
            SetPlayerInput(false);
            
            // 2. Bắt đầu đếm ngược thời gian: Chờ phim chạy hết giây cuối cùng sẽ tự rã băng
            StartCoroutine(WaitForTimelineToFinish());
        }
    }

    // Hàm đếm ngược thông minh tự động mở khóa
    private IEnumerator WaitForTimelineToFinish()
    {
        if (playableDirector == null) yield break;

        // Lấy chính xác tổng số giây độ dài của đoạn phim Timeline
        float timelineDuration = (float)playableDirector.duration;

        Debug.Log($"Timeline bắt đầu chạy, thời lượng phim: {timelineDuration} giây. Bắt đầu đếm ngược mở khóa...");

        // Máy tính sẽ chờ đúng bằng số giây của bộ phim
        yield return new WaitForSeconds(timelineDuration);

        // Đã hết phim! Tự động giải phóng cho người chơi di chuyển
        ForceEnablePlayerInput();
        
        Debug.Log("Bộ phim kết thúc hoàn toàn! Đã tự động trả lại tự do cho người chơi.");
    }

    // Hàm mở khóa chủ động (vẫn giữ lại để gán vào ghim Signal gọi Boss)
    public void ForceEnablePlayerInput()
    {
        SetPlayerInput(true);
        Debug.Log("Đã kích hoạt mở khóa quyền điều khiển cho Player!");
    }

    // Hàm hỗ trợ bật/tắt script di chuyển
    private void SetPlayerInput(bool isEnable)
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = isEnable;
        }
    }
}
