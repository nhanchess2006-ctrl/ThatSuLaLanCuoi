using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TMP_Text progressText;

    public void UpdateProgress(int current, int target)
    {
        progressText.text = $"🌼 {current}/{target}";
    }
}