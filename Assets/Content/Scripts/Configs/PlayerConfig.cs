using System;
using GameCore.Configs;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : Config
    {
        [field: SerializeField] public MoveData MoveData { get; private set; }
    }

    [Serializable]
    public class MoveData
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
    }
}