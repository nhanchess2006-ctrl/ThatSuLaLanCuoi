using System.Collections;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Quest UI")]
    [SerializeField] private QuestUI questUI;
    [SerializeField] private GameObject completePanel;
    [SerializeField] private float completeShowTime = 2f;

    [Header("Flower Quest")]
    public int currentFlower = 0;
    public int targetFlower = 5;

    [Header("Wave 1")]
    [SerializeField] private GameObject[] firstWaveEnemies;

    [Header("Wave 2")]
    [SerializeField] private GameObject[] secondWaveEnemies;

    [Header("Portal")]
    [SerializeField] private Object_Portal portal;
    [SerializeField] private Transform portalSpawnPoint;

    // Giữ lại để Flower.cs / script cũ không bị lỗi
    public bool bossDefeated = false;

    private bool battleStarted = false;
    private bool questCompleted = false;
    private bool doorSpawned = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        if (questUI != null)
        {
            questUI.UpdateProgress(currentFlower, targetFlower);
        }

        if (completePanel != null)
        {
            completePanel.SetActive(false);
        }

        // Tắt 2 wave lúc đầu
        SetWaveActive(firstWaveEnemies, false);
        SetWaveActive(secondWaveEnemies, false);

        // Tắt Portal lúc đầu
        if (portal != null)
        {
            portal.gameObject.SetActive(false);
        }
    }


    // =====================================================
    // NHẶT HOA
    // =====================================================

    public void CollectFlower()
    {
        if (currentFlower >= targetFlower)
            return;

        currentFlower++;

        if (questUI != null)
        {
            questUI.UpdateProgress(currentFlower, targetFlower);
        }

        Debug.Log(
            "Flower: " +
            currentFlower +
            "/" +
            targetFlower
        );

        if (currentFlower >= targetFlower && !battleStarted)
        {
            StartBattle();
        }
    }


    // =====================================================
    // BẮT ĐẦU COMBAT
    // =====================================================

    private void StartBattle()
    {
        if (battleStarted)
            return;

        battleStarted = true;

        Debug.Log("Đã nhặt đủ hoa!");

        StartCoroutine(BattleSequence());
    }


    // =====================================================
    // TRÌNH TỰ WAVE
    // =====================================================

    private IEnumerator BattleSequence()
    {
        // WAVE 1
        Debug.Log("Wave 1 bắt đầu!");

        SetWaveActive(firstWaveEnemies, true);

        yield return new WaitUntil(
            () => IsWaveDead(firstWaveEnemies)
        );

        Debug.Log("Wave 1 đã chết hết!");

        // WAVE 2 bật ngay
        SetWaveActive(secondWaveEnemies, true);

        Debug.Log("Wave 2 bắt đầu!");

        yield return new WaitUntil(
            () => IsWaveDead(secondWaveEnemies)
        );

        Debug.Log("Wave 2 đã chết hết!");

        bossDefeated = true;

        CompleteQuest();
    }


    // =====================================================
    // BẬT / TẮT WAVE
    // =====================================================

    private void SetWaveActive(GameObject[] wave, bool active)
    {
        if (wave == null)
            return;

        foreach (GameObject enemy in wave)
        {
            if (enemy != null)
            {
                enemy.SetActive(active);
            }
        }
    }


    // =====================================================
    // KIỂM TRA WAVE
    // =====================================================

    private bool IsWaveDead(GameObject[] wave)
    {
        if (wave == null || wave.Length == 0)
            return true;

        foreach (GameObject enemy in wave)
        {
            /*
             * Destroy(gameObject):
             * enemy sẽ trở thành null.
             *
             * SetActive(false):
             * enemy vẫn tồn tại nhưng activeInHierarchy = false.
             *
             * Cả hai trường hợp đều được tính là đã chết.
             */
            if (enemy != null && enemy.activeInHierarchy)
            {
                return false;
            }
        }

        return true;
    }


    // =====================================================
    // HOÀN THÀNH QUEST
    // =====================================================

    private void CompleteQuest()
    {
        if (questCompleted)
            return;

        questCompleted = true;
        bossDefeated = true;

        Debug.Log("HOÀN THÀNH NHIỆM VỤ!");

        if (completePanel != null)
        {
            completePanel.SetActive(true);

            Invoke(
                nameof(HideCompletePanel),
                completeShowTime
            );
        }

        OpenPortal();
    }


    // =====================================================
    // PORTAL
    // =====================================================

    private void OpenPortal()
    {
        if (doorSpawned)
            return;

        doorSpawned = true;

        if (portal == null)
        {
            Debug.LogWarning("QuestManager: Chưa gán Portal!");
            return;
        }

        if (portalSpawnPoint == null)
        {
            Debug.LogWarning("QuestManager: Chưa gán Portal Spawn Point!");
            return;
        }

        // Đưa portal tới đúng vị trí
        portal.transform.position = portalSpawnPoint.position;

        // Bật portal
        portal.gameObject.SetActive(true);

        // Nếu Object_Portal có logic riêng thì gọi thêm
        portal.ActivatePortal(portalSpawnPoint.position);

        Debug.Log("Portal đã mở!");
    }


    // =====================================================
    // UI
    // =====================================================

    private void HideCompletePanel()
    {
        if (completePanel != null)
        {
            completePanel.SetActive(false);
        }
    }


    // =====================================================
    // GIỮ TƯƠNG THÍCH VỚI CODE CŨ
    // =====================================================

    public void NotifyEnemyDeath(Enemy_Health enemy)
    {
        // Giữ hàm này để Enemy_Health cũ gọi vào không báo CS1061.
        // Việc kiểm tra enemy chết giờ do BattleSequence xử lý.
    }


    public bool IsQuestCompleted()
    {
        return questCompleted;
    }


    public bool IsFlowerCompleted()
    {
        return currentFlower >= targetFlower;
    }


    public bool IsBattleStarted()
    {
        return battleStarted;
    }
}