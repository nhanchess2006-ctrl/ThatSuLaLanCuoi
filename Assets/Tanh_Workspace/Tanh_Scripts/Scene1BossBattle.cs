using UnityEngine;
using UnityEngine.Playables; // Bắt buộc để điều khiển Timeline

public class Scene1BossBattleSignalController : MonoBehaviour
{
    [Header("Cấu hình Timeline")]
    [SerializeField] private PlayableDirector playableDirector;

    [Header("Cấu hình Kiểm Tra Máu Boss")]
    [SerializeField] private GameObject bossObject;      // GameObject Boss chiến đấu
    
    // NHỚ SỬA: Thay chữ "BossHealth" bằng tên chính xác của Script quản lý máu nằm trên Boss
    private MonoBehaviour bossHealthScript; 
    
    // NHỚ SỬA: Thay chữ "currentHealth" bằng tên chính xác của biến chứa Máu của Boss (ví dụ: hp, health...)
    [SerializeField] private string hpVariableName = "currentHealth"; 

    private bool isBattleActive = false;
    private bool isTimelineResumed = false;

    void Awake()
    {
        if (playableDirector == null) playableDirector = GetComponent<PlayableDirector>();
    }

    // ====================================================
    // HÀM CHÍNH: ĐƯỢC GỌI BỞI GHIM SIGNAL TRÊN TIMELINE
    // ====================================================
    public void StartBossBattle()
    {
        if (playableDirector == null) return;

        // 1. Đóng băng mạch phim Timeline lại để chờ người chơi đánh nhau
        playableDirector.Pause();

        // 2. Tìm kiếm script quản lý máu trên người Boss
        if (bossObject != null)
        {
            // Nhớ đổi chữ "BossHealth" cho đúng tên script tính máu của Boss của bạn
            bossHealthScript = bossObject.GetComponent("BossHealth") as MonoBehaviour;
        }

        isBattleActive = true;
        isTimelineResumed = false;
        
        Debug.Log("Timeline đã đóng băng! Bắt đầu kiểm tra ngầm lượng máu của Boss...");
    }

    void Update()
    {
        // Nếu trận đấu đang diễn ra và chưa ra lệnh chạy tiếp Timeline
        if (isBattleActive && !isTimelineResumed)
        {
            if (bossHealthScript != null)
            {
                // Sử dụng kỹ thuật Reflection để đọc giá trị biến máu từ script của Boss
                System.Reflection.FieldInfo field = bossHealthScript.GetType().GetField(hpVariableName, 
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (field != null)
                {
                    float currentHp = System.Convert.ToSingle(field.GetValue(bossHealthScript));

                    // NẾU MÁU BOSS VỀ 0 (HOẶC NHỎ HƠN 0) -> ĐÃ HẠ BOSS XONG!
                    if (currentHp <= 0)
                    {
                        ResumeTimeline();
                    }
                }
            }
            else
            {
                // Phòng hờ nếu Boss bị hủy (Destroy) hoặc tắt đi, game vẫn tự động chạy tiếp không bị kẹt
                if (bossObject == null || !bossObject.activeInHierarchy)
                {
                    ResumeTimeline();
                }
            }
        }
    }

    // Hàm ra lệnh giải thoát cho Timeline
    private void ResumeTimeline()
    {
        isTimelineResumed = true;
        isBattleActive = false;

        // Cho Timeline tiếp tục chạy mạch phim
        if (playableDirector != null)
        {
            playableDirector.Play();
        }

        Debug.Log("Boss đã bị hạ gục! Timeline tự động rã băng và chạy tiếp bình thường.");
    }
}
