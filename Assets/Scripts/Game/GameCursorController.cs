using System;
using UnityEngine;
using Zenject;

public class GameCursorController : IInitializable, IDisposable
{
    public int CursorsBlocks { get; private set; } = 0;

    private readonly SignalBus m_signalBus;

    public GameCursorController(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    public void Initialize()
    {
        m_signalBus.Subscribe<GameLockCursor>(OnCursorBlocked);
        m_signalBus.Subscribe<GameUnlockCursor>(OnCursorUnblocked);
    }

    public void Dispose()
    {
        m_signalBus.Unsubscribe<GameLockCursor>(OnCursorBlocked);
        m_signalBus.Unsubscribe<GameUnlockCursor>(OnCursorUnblocked);
    }

    public void LockCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnCursorBlocked(GameLockCursor blockCursorSignal)
    {
        LockCursor(true);
    }

    private void OnCursorUnblocked(GameUnlockCursor unlockCursorSignal)
    {
        LockCursor(false);
    }
}
