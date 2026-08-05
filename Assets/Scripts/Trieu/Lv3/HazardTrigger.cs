using UnityEngine;

public class HazardTrigger : MonoBehaviour
{
    [Header("Settings for falling object")]
    [Tooltip("Kéo thả Prefab của vật thể gây sát thương vào đây")]
    public GameObject fallingObjectPrefab; 
    
    [Tooltip("Vị trí mà vật thể sẽ bắt đầu rơi xuống (nên đặt cao hơn nhân vật)")]
    public Transform dropPoint; 
    
    [Header("Trigger settings")]
    public string playerTag = "Player"; // Tag của nhân vật
    public bool triggerOnce = true; // Đánh dấu true nếu bẫy chỉ rơi 1 lần
    
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng chạm vào có phải là Player không
        if (other.CompareTag(playerTag) && !hasTriggered)
        {
            if (triggerOnce)
            {
                hasTriggered = true;
            }
            
            // Tạo vật thể rơi tại vị trí dropPoint
            if (fallingObjectPrefab != null && dropPoint != null)
            {
                Instantiate(fallingObjectPrefab, dropPoint.position, dropPoint.rotation);
            }
            else
            {
                Debug.LogWarning("Chưa gán Prefab vật thể rơi hoặc Vị trí rơi trong Inspector của HazardTrigger!");
            }
        }
    }
}