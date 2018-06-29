using UnityEngine;

namespace CrossPlatformDevelopment.ScriptableObject.Inventory
{
    public interface IConsumable
    {
        void Consume(GameObject owner);
    }
}