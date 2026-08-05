using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public int currentFlower = 0;

    public int targetFlower = 20;

    private void Awake()
    {
        Instance = this;
    }

    public void CollectFlower()
    {
        currentFlower++;

        Debug.Log(currentFlower + "/" + targetFlower);

        if(currentFlower >= targetFlower)
        {
            Debug.Log("Hoàn thành nhiệm vụ!");
        }
    }
}