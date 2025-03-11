using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings", menuName = "Project/Player Settings")]
public class PlayerSettingsSO : ScriptableObject
{
    [field: SerializeField]
    public PlayerAttributes BaseAttributes { get; private set; }

    [field: SerializeField]
    public float MouseSensivity { get; private set; }
}
