using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossPlatformDevelopment
{
    [CreateAssetMenu(menuName = "Scriptables/BackPackObject")]
    public class BackpackObject : ScriptableObject
    {
        [SerializeField]
        private List<Item> _items;

        public delegate void Callback();

        public Callback onListChanged;

        public List<Item> Items//this must be exposed so you can loop through the list
        {
            get { return _items; }
        }

        public void Add(Item item)
        {
            if (_items.Contains(item))
                return;
            _items.Add(item);
            onListChanged.Invoke();//callback to the ui when this list changes
        }

        public void Remove(Item item)
        {
            if (!_items.Contains(item))
                return;
            _items.Remove(item);
            onListChanged.Invoke();//callback to the ui when this list changes
        }
    }
}
