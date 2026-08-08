using UnityEngine;

public class PlayerInputKickstarter : MonoBehaviour
{
    [Header("Player Reference")]
    [Tooltip("Kéo nhân vật Player ngoài Hierarchy vào đây")]
    [SerializeField] private Player player;

    [Header("Enemy Reference")]
    [Tooltip("Kéo Boss Enemy_Skeleton ngoài Hierarchy vào đây")]
    [SerializeField] private Enemy_Skeleton skeletonEnemy;

    // Hàm này được gọi từ chiếc Ghim Tín hiệu (Signal) độc lập trên Timeline khi Player được bật lên
    public void KickstartPlayerSpaceInput()
    {
        // ==========================================
        // 1. LOGIC RÃ BĂNG HOẠT HỌA CHO PLAYER
        // ==========================================
        if (player != null)
        {
            // Reset lại component để đồng bộ trạng thái vòng đời vật lý
            player.enabled = false;
            player.enabled = true;

            // Ép Máy trạng thái chuyển giao sang trạng thái đứng im tự do
            if (player.stateMachine != null && player.idleState != null)
            {
                player.stateMachine.ChangeState(player.idleState);
            }

            // Ép Animator nhận diện đúng tham số để thoát đơ
            if (player.anim != null)
            {
                player.anim.SetFloat("xVelocity", 0f);
                player.anim.SetFloat("yVelocity", 0f);
                
                // 🔥 CHÍ MẠNG: Bật biến Parameter kiểu Bool tên là "idle" thành TRUE
                // Dòng này bẻ gãy điều kiện mũi tên khóa Transition ra Exit của Animator!
                player.anim.SetBool("idle", true); 
                
                // Ép đồ họa Animator cập nhật và vẽ ngay khung hình Idle ban đầu
                player.anim.Update(0); 
                Debug.Log("👤 [Rã băng] Player đã thức tỉnh hoạt họa Idle đứng thở tự nhiên.");
            }
        }

        // ==========================================
        // 2. LOGIC RÃ BĂNG HOẠT HỌA CHO BOSS SKELETON
        // ==========================================
        if (skeletonEnemy != null)
        {
            // Reset lại component để giải phóng các lực đè cũ của Timeline lên quái
            skeletonEnemy.enabled = false;
            skeletonEnemy.enabled = true;

            // Ép quái chuyển sang trạng thái chiến đấu (battleState) ngay lập tức
            if (skeletonEnemy.stateMachine != null && skeletonEnemy.battleState != null)
            {
                skeletonEnemy.stateMachine.ChangeState(skeletonEnemy.battleState);
            }

            // Ép Animator của quái nhận diện đúng tham số để thoát đơ
            if (skeletonEnemy.anim != null)
            {
                skeletonEnemy.anim.SetFloat("xVelocity", 0f);

                // 🔥 CHÍ MẠNG: Bật biến Parameter kiểu Bool tên là "battle" thành TRUE
                // Chuỗi ký tự "battle" viết thường toàn bộ để khớp với bảng Parameters của Skeleton!
                skeletonEnemy.anim.SetBool("battle", true);

                // Ép Animator của Skeleton chạy và vẽ ngay khung hình chiến đấu thủ thế ban đầu
                skeletonEnemy.anim.Update(0);
                Debug.Log("💀 [Rã băng] Skeleton đã thức tỉnh hoạt họa Battle thủ thế chiến đấu.");
            }
        }
    }
}
