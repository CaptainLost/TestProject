using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItemUI : MonoBehaviour
{
    [SerializeField]
    protected Image m_slotImage;

    protected virtual void SetImage(Sprite sprite)
    {
        m_slotImage.sprite = sprite;
    }
}
