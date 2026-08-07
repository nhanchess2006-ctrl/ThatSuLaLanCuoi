using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class TilemapHazard : MonoBehaviour
{
    [Header("Cài đặt sát thương")]
    public float damageAmount = 15f;
    public float elementalDamageAmount = 0f;
    public string playerTag = "Player";

    [Header("Hồi chiêu sát thương")]
    [Tooltip("Khoảng thời gian (giây) giữa các lần bị trừ máu khi đứng liên tục trên gai")]
    public float damageCooldown = 0.5f;
    private float lastDamageTime;

    // Transform ẩn dùng để định vị chính xác vị trí ô gai va chạm (giúp tính Knockback chuẩn)
    private Transform contactPointMarker;

    private void Awake()
    {
        // Tạo một object con ẩn để làm mốc vị trí va chạm thực tế
        GameObject markerObj = new GameObject("SpikeContactMarker");
        markerObj.transform.SetParent(transform);
        contactPointMarker = markerObj.transform;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckAndApplyDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CheckAndApplyDamageTrigger(other);
    }

    private void CheckAndApplyDamage(Collision2D collision)
    {
        GameObject target = collision.gameObject;

        if (CanDamagePlayer(target))
        {
            if (collision.contactCount > 0)
            {
                // Cập nhật mốc vị trí về đúng điểm tiếp xúc giữa nhân vật và ô gai
                contactPointMarker.position = collision.GetContact(0).point;
            }
            else
            {
                contactPointMarker.position = target.transform.position;
            }

            ApplyDamage(target, contactPointMarker);
        }
    }

    private void CheckAndApplyDamageTrigger(Collider2D other)
    {
        GameObject target = other.gameObject;

        if (CanDamagePlayer(target))
        {
            contactPointMarker.position = other.bounds.center;
            ApplyDamage(target, contactPointMarker);
        }
    }

    private bool CanDamagePlayer(GameObject target)
    {
        bool isPlayerTag = target.CompareTag(playerTag);
        bool isPlayerLayer = target.layer == LayerMask.NameToLayer("Player");
        bool isCooldownReady = Time.time >= lastDamageTime + damageCooldown;

        return isPlayerTag && isPlayerLayer && isCooldownReady;
    }

    private void ApplyDamage(GameObject target, Transform damageSource)
    {
        Entity_Health health = target.GetComponent<Entity_Health>();

        if (health != null)
        {
            health.TakeDamage(
                damageAmount,
                elementalDamageAmount,
                ElementType.None,
                damageSource
            );

            lastDamageTime = Time.time;
        }
    }
}