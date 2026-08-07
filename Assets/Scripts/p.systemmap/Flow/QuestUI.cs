using TMPro;
using UnityEngine;
using System.Collections;

public class QuestUI : MonoBehaviour
{
[SerializeField] private TMP_Text progressText;


private void Awake()
{
   
}

public void UpdateProgress(int current, int target)
{
    progressText.text = $"🌼 {current}/{target}";
}

}