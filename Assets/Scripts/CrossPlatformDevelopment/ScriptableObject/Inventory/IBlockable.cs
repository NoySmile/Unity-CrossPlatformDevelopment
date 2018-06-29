using UnityEngine;

namespace CrossPlatformDevelopment.ScriptableObject.Inventory
{
    public interface IBlockable
    {
        void Block(GameObject blockedObject);

        void StopBlock();
    }
}