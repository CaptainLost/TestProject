using System;
using UnityEngine;
using Zenject;

public class PlayerActorMovement : IInitializable, IDisposable, ILateTickable
{
    private readonly PlayerActorComponents m_actorComponents;
    private readonly PlayerStatistics m_playerStatistics;
    private readonly SignalBus m_signalBus;

    private Vector2 m_playerMovementInput;

    public PlayerActorMovement(PlayerActorComponents actorComponents, PlayerStatistics playerStatistics, SignalBus signalBus)
    {
        m_actorComponents = actorComponents;
        m_playerStatistics = playerStatistics;
        m_signalBus = signalBus;
    }

    public void Initialize()
    {
        m_signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
    }

    public void Dispose()
    {
        m_signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
    }

    public void LateTick()
    {
        Vector3 movement = m_actorComponents.CharacterController.transform.forward * m_playerMovementInput.y +
                   m_actorComponents.CharacterController.transform.right * m_playerMovementInput.x;

        movement *= m_playerStatistics.Attributes.MoveSpeed;

        m_actorComponents.CharacterController.Move(movement * Time.deltaTime);
    }

    public void SetInput(Vector2 input)
    {
        m_playerMovementInput = input;
    }

    private void OnGameStarted(GameStartedSignal gameStartedSignal)
    {
        m_actorComponents.CharacterController.transform.position = m_actorComponents.CharacterStartPosition.position;
        Physics.SyncTransforms();
    }
}
