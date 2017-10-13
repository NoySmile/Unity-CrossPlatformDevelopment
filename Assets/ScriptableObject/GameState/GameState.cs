using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName ="Scriptables/GameState")]
public class GameState : ScriptableSingleton<GameState>
{
    public class PlayerStatChangeEvent : UnityEvent<Stat> { }

    public PlayerStatChangeEvent EVENT_PLAYERSTATCHANGE = new PlayerStatChangeEvent();
    
}

