using System;

namespace CrossPlatformDevelopment.ScriptableObject.Inventory
{
    [Serializable]
    public abstract class Weapon : Equipment
    {
        public int Damage;
    }
}