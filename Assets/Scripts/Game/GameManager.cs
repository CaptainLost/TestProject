using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameManager : IDisposable
{
    private readonly SignalBus m_signalBus;
    private readonly ItemSystem m_itemSystem;

    private CancellationTokenSource m_gameStartCTS;

    public GameManager(SignalBus signalBus, ItemSystem itemSystem)
    {
        m_signalBus = signalBus;
        m_itemSystem = itemSystem;
    }

    public void Dispose()
    {
        m_gameStartCTS?.Cancel();
    }

    // We consider the case of multiple calls
    public async void StartGame()
    {
        m_gameStartCTS?.Cancel();
        m_gameStartCTS = new CancellationTokenSource();

        if (!await PrepareGameAsync(m_gameStartCTS.Token))
        {
            Debug.LogError("Failed to prepare game");

            return;
        }

        m_signalBus.Fire(new GameStartedSignal());
    }

    public void FinishGame()
    {
        m_signalBus.Fire(new GameFinishedSignal());
    }

    private async Task<bool> PrepareGameAsync(CancellationToken cancellationToken = default)
    {
        m_signalBus.Fire(new GamePrepareStartedSignal());

        if (!await m_itemSystem.FetchItemsAsync(cancellationToken))
        {
            m_signalBus.Fire(new GamePrepareFailedSignal());
            Debug.LogError("Failed to fetch items");

            return false;
        }

        return true;
    }
}
