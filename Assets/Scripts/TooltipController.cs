using UnityEngine;

public class TooltipController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_tooltipObject;

    [SerializeField]
    private TooltipUI m_tooltipUI;

    public void Show(string headerText, string contentText)
    {
        m_tooltipObject.SetActive(true);

        m_tooltipUI.SetHeader(headerText);
        m_tooltipUI.SetContent(contentText);
    }

    public void Hide()
    {
        m_tooltipObject.SetActive(false);
    }
}
