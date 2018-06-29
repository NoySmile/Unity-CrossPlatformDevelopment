using System;

namespace CrossPlatformDevelopment.ScriptableObject.Inventory
{
    [Serializable]
    public abstract class Equipment : Item, IEquippable
    {
        private void OnEnable()
        {
            _itemName = GetType().ToString();
        }
        public virtual void Equip() { }

        public virtual void UnEquip() { }
    }
}