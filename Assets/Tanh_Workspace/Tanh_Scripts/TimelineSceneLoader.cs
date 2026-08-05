using UnityEngine;
using UnityEngine.SceneManagement; // Bắt buộc phải có thư viện này để chuyển cảnh

public class TimelineSceneLoader : MonoBehaviour
{
    [Header("Cấu hình Chuyển Màn Chơi")]
    [Tooltip("Nhập chính xác tên Scene tiếp theo bạn muốn chuyển tới")]
    [SerializeField] private string sceneToLoad;

    // HÀM CHUẨN: Không tham số, sẽ xuất hiện rực rỡ trên bảng chọn của Ghim Signal
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Debug.Log($"Timeline chạm mốc chỉ định! Đang tự động chuyển sang Scene: {sceneToLoad}");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Debug.LogError("LỖI: Bạn chưa nhập tên Scene tiếp theo vào ô 'Scene To Load' ở bảng Inspector!");
        }
    }
}
