using UnityEngine;

public class QuestManager : MonoBehaviour
{
[SerializeField] private GameObject completePanel;
[SerializeField] private float completeShowTime = 2f;
[SerializeField] private Object_Portal portal;
[SerializeField] private Transform portalSpawnPoint;
[Header("Boss")]
[SerializeField] private Enemy_Health questBoss;
public bool bossDefeated = false;
public static QuestManager Instance;
private bool doorSpawned = false;

[SerializeField] private QuestUI questUI;

public int currentFlower = 0;
public int targetFlower = 20;


private void Awake()
{
    Instance = this;
}

private void Start()
{
    questUI.UpdateProgress(currentFlower, targetFlower);

    completePanel.SetActive(false);

}
public bool IsQuestCompleted()
{
  return currentFlower >=  targetFlower ;
}

public void CollectFlower()
{
    currentFlower++;

    questUI.UpdateProgress(currentFlower, targetFlower);

    if (currentFlower >= targetFlower && !doorSpawned)
    {
      doorSpawned = true;

      Debug.Log("Hoàn thành nhiệm vụ!");
      completePanel.SetActive(true);

      Invoke(nameof(HideCompletePanel), completeShowTime);

       portal.ActivatePortal(portalSpawnPoint.position);
    }
}
 public void NotifyEnemyDeath(Enemy_Health enemy)
{
    if (enemy == questBoss)
    {
        bossDefeated = true;
        Debug.Log("Boss nhiệm vụ đã bị tiêu diệt!");
    }
}
private void HideCompletePanel()
{
    completePanel.SetActive(false);
}

}