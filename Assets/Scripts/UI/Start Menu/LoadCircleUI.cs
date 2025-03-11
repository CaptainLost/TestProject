using UnityEngine;
using UnityEngine.UI;

public class LoadCircleUI : MonoBehaviour
{
    [SerializeField]
    private float m_animationSpeed = 1f;

    [SerializeField]
    private Transform m_rotatedTransform;
    [SerializeField]
    private Image m_circleImage;

    private float m_animationTime;

    private void OnEnable()
    {
        m_animationTime = 0f;
    }

    private void Update()
    {
        m_animationTime += Time.deltaTime;

        float time = (Mathf.Sin(m_animationTime * m_animationSpeed) + 1f) * 0.5f;
        float easedTime = UtilsEasing.EaseInOutQuad(0f, 1f, time);

        float zRotation = easedTime * 360f;
        m_rotatedTransform.eulerAngles = new Vector3(0f, 0f, zRotation);

        m_circleImage.fillAmount = easedTime;
    }
}
