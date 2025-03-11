using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ItemSystem
{
    public ItemRegistry ItemRegistry { get; private set; }

    public async Task<bool> FetchItemsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            GameServerMock gameServerMock = new GameServerMock();
            string itemsResult = await gameServerMock.GetItemsAsync(cancellationToken);

            ItemDatabase itemDatabase = JsonConvert.DeserializeObject<ItemDatabase>(itemsResult);

            ItemRegistry = new ItemRegistry(itemDatabase);

            return true;
        }
        catch (JsonException exception)
        {
            Debug.Log($"ItemSystem deserlization exception: {exception.Message}");
            return false;
        }
        catch (Exception exception)
        {
            Debug.LogError($"ItemSystem exception: {exception.Message}");
            return false;
        }
    }
}
