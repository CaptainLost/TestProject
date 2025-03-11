using System;
using UnityEngine;

[Serializable]
public class PlayerActorComponents
{
    [field: SerializeField]
    public CharacterController CharacterController { get; private set; }
    [field: SerializeField]
    public Camera CharacterCamera { get; private set; }
    [field: SerializeField]
    public Transform CharacterStartPosition { get; private set; }
}
