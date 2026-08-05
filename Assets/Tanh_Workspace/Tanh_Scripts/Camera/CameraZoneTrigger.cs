using UnityEngine;
using Unity.Cinemachine; // Không dùng namespace cũ nữa

// [RequireComponent(typeof(PolygonCollider2D))] // Ép buộc phải dùng Polygon trên Unity 6
public class CameraZoneUnity6 : MonoBehaviour
{
    [Header("Cinemachine Settings")]
    [SerializeField] private CinemachineCamera zoneVirtualCamera; // Unity 6 đổi tên từ CinemachineVirtualCamera thành CinemachineCamera
    
    [Header("Priority Configurations")]
    [SerializeField] private int activePriority = 20;
    [SerializeField] private int inactivePriority = 10;

    [Header("Target Tag")]
    [SerializeField] private string playerTag = "Player";

    private PolygonCollider2D zoneCollider;

    private System.Collections.IEnumerator Start()
    {
        // Đợi 1 khung hình để ma trận vật lý Unity 6 ổn định
        yield return null; 

        zoneCollider = GetComponent<PolygonCollider2D>();
        zoneCollider.isTrigger = true;

        if (zoneVirtualCamera != null)
        {
            zoneVirtualCamera.Priority = inactivePriority;

            // Kiểm tra component Confiner 2D
            if (zoneVirtualCamera.TryGetComponent<CinemachineConfiner2D>(out var confiner))
            {
                // Gán quyền giới hạn camera bằng Collider vùng này
                confiner.BoundingShape2D = zoneCollider;
                
                // HÀM MỚI TRÊN UNITY 6: Ép buộc cập nhật lại bộ nhớ đệm biên giới
                confiner.InvalidateBoundingShapeCache(); 
            }
            else
            {
                Debug.LogWarning($"[Unity6-Camera] Camera {zoneVirtualCamera.name} đang thiếu CinemachineConfiner2D!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && zoneVirtualCamera != null)
        {
            zoneVirtualCamera.Priority = activePriority;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && zoneVirtualCamera != null)
        {
            zoneVirtualCamera.Priority = inactivePriority;
        }
    }
}
