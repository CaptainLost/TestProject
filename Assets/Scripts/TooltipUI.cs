using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_offset;

    [SerializeField]
    private RectTransform m_tooltipRectTransform;

    [SerializeField]
    private TextMeshProUGUI m_headerText;

    [SerializeField]
    private TextMeshProUGUI m_contentText;

    private void OnEnable()
    {
        SetPositionToMouse();
    }

    private void Update()
    {
        SetPositionToMouse();
    }

    public void SetHeader(string headerText)
    {
        m_headerText.text = headerText;
    }

    public void SetContent(string contentText)
    {
        m_contentText.text = contentText;
    }

    private void SetPositionToMouse()
    {
        m_tooltipRectTransform.position = Input.mousePosition + (Vector3)m_offset;
    }
}
