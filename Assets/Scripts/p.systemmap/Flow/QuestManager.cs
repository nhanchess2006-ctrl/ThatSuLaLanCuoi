using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public bool bossDefeated = false;
    public static QuestManager Instance;

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
    }
    public bool IsQuestCompleted()
    {
      return currentFlower >= 10;
   }

    public void CollectFlower()
    {
        currentFlower++;

        questUI.UpdateProgress(currentFlower, targetFlower);

        if(currentFlower >= targetFlower)
        {
            Debug.Log("Hoàn thành nhiệm vụ!");
        }
    }
}