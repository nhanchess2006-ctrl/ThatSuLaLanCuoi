using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class FallingObjectDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 200f;
    public float elementalDamageAmount = 0f; 
    
    public string playerTag = "Player";
    
    [Header("Impact Effect (Optional)")]
    public GameObject impactEffect;


    private void OnCollisionEnter2D(Collision2D collision)
    {
    // Kiểm tra ĐỒNG THỜI cả Tag và Layer
    bool isPlayerTag = collision.gameObject.CompareTag(playerTag);
    bool isPlayerLayer = collision.gameObject.layer == LayerMask.NameToLayer("Player");

    if (isPlayerTag && isPlayerLayer)
    {
        Entity_Health health = collision.gameObject.GetComponent<Entity_Health>();

        if (health != null)
        {
            health.TakeDamage(
                damageAmount,
                elementalDamageAmount,
                ElementType.None,
                transform
            );
        }

        DestroyHazard();
    }
    else
    {
        // Chạm vào đất, tường, quái vật khác, hoặc vật có Tag Player nhưng sai Layer -> Đều vỡ
        DestroyHazard();
    }
    }

    private void DestroyHazard()
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}