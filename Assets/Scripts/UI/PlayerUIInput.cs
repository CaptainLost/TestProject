using System.Threading.Tasks;
using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    [SerializeField]
    private GameObject m_targetObject;
    [SerializeField]
    private CanvasGroup m_targetCanvasGroup;

    [SerializeField]
    private float m_fadeDuration = 0.5f;

    private bool m_isVisible;
    private bool m_isAnimating;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleUI();
        }
    }

    private async void ToggleUI()
    {
        if (m_isAnimating)
            return;

        m_isAnimating = true;

        if (!m_isVisible)
        {
            m_targetObject.SetActive(true);
            await FadeCanvasGroup(m_targetCanvasGroup, 0f, 1f, m_fadeDuration);
        }
        else
        {
            await FadeCanvasGroup(m_targetCanvasGroup, 1f, 0f, m_fadeDuration);
            m_targetObject.SetActive(false);
        }

        m_isVisible = !m_isVisible;
        m_isAnimating = false;
    }

    private async Task FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float fadeDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float time = elapsedTime / fadeDuration;
            canvasGroup.alpha = UtilsEasing.EaseInOutQuad(startAlpha, endAlpha, time);

            await Task.Yield();
        }

        canvasGroup.alpha = endAlpha;
    }
}
