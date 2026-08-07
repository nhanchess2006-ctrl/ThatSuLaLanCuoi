using System.Collections;
using UnityEngine;

public class QuestPopupUI : MonoBehaviour
{
[SerializeField] private GameObject popupPanel;
[SerializeField] private GameObject hudPanel;

[SerializeField] private CanvasGroup canvasGroup;
[SerializeField] private float fadeDuration = 0.5f;

private void Start()
{
    popupPanel.SetActive(true);
    hudPanel.SetActive(false);

    canvasGroup.alpha = 0;
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;

    StartCoroutine(FadeIn());
}

IEnumerator FadeIn()
{
    while (canvasGroup.alpha < 1)
    {
        canvasGroup.alpha += Time.deltaTime / fadeDuration;
        yield return null;
    }

    canvasGroup.alpha = 1;
    canvasGroup.interactable = true;
    canvasGroup.blocksRaycasts = true;
}

public void ConfirmQuest()
{
    StartCoroutine(FadeOut());
}

IEnumerator FadeOut()
{
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;

    while (canvasGroup.alpha > 0)
    {
        canvasGroup.alpha -= Time.deltaTime / fadeDuration;
        yield return null;
    }

    canvasGroup.alpha = 0;

    popupPanel.SetActive(false);
    hudPanel.SetActive(true);
}


}