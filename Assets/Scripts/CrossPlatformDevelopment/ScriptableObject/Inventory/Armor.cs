using System;

namespace CrossPlatformDevelopment.ScriptableObject.Inventory
{
    [Serializable]
    public abstract class Armor : Equipment
    {
        public int ArmorRating;
    }
}