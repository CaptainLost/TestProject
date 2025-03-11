using UnityEngine;
using Zenject;

public class EnemyActor : MonoBehaviour
{
    public class Factory : PlaceholderFactory<EnemyActor>
    {

    }
}
