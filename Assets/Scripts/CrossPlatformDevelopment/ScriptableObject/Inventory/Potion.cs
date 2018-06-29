using System;
using UnityEngine;

namespace CrossPlatformDevelopment.ScriptableObject.Inventory
{
    [Serializable]
    public abstract class Potion : Equipment, IConsumable
    {
        public abstract void Consume(GameObject owner);
    }
}