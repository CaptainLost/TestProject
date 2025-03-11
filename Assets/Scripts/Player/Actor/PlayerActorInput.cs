using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerActorInput : ITickable
{
    private readonly PlayerActorMovement m_playerMovement;
    private readonly PlayerActorCamera m_playerCamera;
    private readonly PlayerShooter m_playerShooter;

    public PlayerActorInput(PlayerActorMovement playerMovement, PlayerActorCamera playerCamera, PlayerShooter playerShooter)
    {
        m_playerMovement = playerMovement;
        m_playerCamera = playerCamera;
        m_playerShooter = playerShooter;
    }

    public void Tick()
    {
        MovementInput();
        CameraInput();
        ShootInput();
    }

    private void MovementInput()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        m_playerMovement.SetInput(mouseInput);
    }

    private void CameraInput()
    {
        if (IsMouseOverUI())
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector2 lookInput = new Vector2(mouseX, mouseY);
        m_playerCamera.RotateCamera(lookInput);
    }

    private void ShootInput()
    {
        if (IsMouseOverUI())
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        m_playerShooter.Fire();
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
