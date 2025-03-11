using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggedItemUI : MonoBehaviour
{
    [SerializeField]
    private Image m_dragImage;

    private Canvas m_owningCanvas;

    private void Awake()
    {
        m_owningCanvas = GetComponentInParent<Canvas>();
    }

    private void OnDisable()
    {
        EndDrag();
    }

    public void StartDrag(Vector3 startPos, Sprite dragSprite)
    {
        m_dragImage.transform.position = startPos;
        m_dragImage.sprite = dragSprite;

        gameObject.SetActive(true);
    }

    public void DragUpdate(PointerEventData eventData)
    {
        m_dragImage.rectTransform.anchoredPosition += eventData.delta / m_owningCanvas.scaleFactor;
    }

    public void EndDrag()
    {
        gameObject.SetActive(false);
    }
}
