using UnityEngine;

public class PlayerActorCamera
{
    private readonly PlayerActorComponents m_actorComponents;
    private readonly PlayerSettingsSO m_playerSettings;

    private float m_xRotation = 0f;

    public PlayerActorCamera(PlayerActorComponents actorComponents, PlayerSettingsSO playerSettings)
    {
        m_actorComponents = actorComponents;
        m_playerSettings = playerSettings;
    }

    public void RotateCamera(Vector2 input)
    {
        float mouseX = input.x * Time.deltaTime * m_playerSettings.MouseSensivity;
        float mouseY = input.y * Time.deltaTime * m_playerSettings.MouseSensivity;

        m_actorComponents.CharacterController.transform.Rotate(Vector3.up * mouseX);

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        m_actorComponents.CharacterCamera.transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
    }
}
