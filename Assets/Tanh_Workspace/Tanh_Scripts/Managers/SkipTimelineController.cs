using UnityEngine;
using UnityEngine.Playables;

public class TimeBasedSkipController : MonoBehaviour
{
    // Tạo một biến static để lưu trạng thái toàn cục (không bị reset khi load lại scene)
    public static bool IsIntroWatched = false; 

    [Header("Cấu hình Timeline")]
    public PlayableDirector playableDirector;
    public double timeThreshold = 3.0;

    [Header("Giai đoạn 1 (Object 1)")]
    public GameObject firstObjectToDeactivate; 
    public double firstTimeToSkipTo;           

    [Header("Giai đoạn 2 (Object 2)")]
    public GameObject secondObjectToDeactivate; 
    public double secondTimeToSkipTo = 2.0;     

    private bool isFirstSkipDone = false;

    void Start()
    {
        // KIỂM TRA: Nếu người chơi ĐÃ TỪNG XEM INTRO RỒI (tức là quay về từ màn chơi)
        if (IsIntroWatched)
        {
            SkipEverythingImmediately();
        }
    }

    // Hàm tự động bỏ qua toàn bộ Credit nếu quay lại intro
    void SkipEverythingImmediately()
    {
        if (playableDirector == null) return;

        if (firstObjectToDeactivate != null) firstObjectToDeactivate.SetActive(false);
        if (secondObjectToDeactivate != null) secondObjectToDeactivate.SetActive(false);

        // Nhảy thẳng về giây thứ 2 (hoặc mốc bạn muốn sau khi kết thúc credit)
        playableDirector.time = secondTimeToSkipTo; 
        playableDirector.Evaluate();

        // Tắt luôn script để người chơi không bấm skip được nữa
        this.enabled = false; 
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            HandleTimeBasedSkip();
        }
    }

    void HandleTimeBasedSkip()
    {
        if (playableDirector == null) return;
        double currentTime = playableDirector.time;

        if (currentTime < timeThreshold && !isFirstSkipDone)
        {
            if (firstObjectToDeactivate != null) firstObjectToDeactivate.SetActive(false);
            playableDirector.time = firstTimeToSkipTo;
            playableDirector.Evaluate();
            isFirstSkipDone = true;
        }
        else
        {
            if (firstObjectToDeactivate != null) firstObjectToDeactivate.SetActive(false);
            if (secondObjectToDeactivate != null) secondObjectToDeactivate.SetActive(false);
            
            playableDirector.time = secondTimeToSkipTo;
            playableDirector.Evaluate();

            // ĐÁNH DẤU: Người chơi đã xem xong intro hoàn chỉnh bằng cách bấm skip lần cuối
            IsIntroWatched = true; 

            this.enabled = false; 
        }
    }
}
