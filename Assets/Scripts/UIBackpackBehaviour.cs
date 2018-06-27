using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CrossPlatformDevelopment
{
    public class UIBackpackBehaviour : MonoBehaviour//this will update based on the state of the backpack
    {
        public BackpackObject backPack;
        public List<GameObject> uiitems;
        public Transform Container;//the parent container
        public GameObject Prefab;

        private void Start()
        {
            uiitems = new List<GameObject>();
            Refresh();

            backPack.onListChanged += Refresh;
        }

        private void Refresh()
        {
            uiitems.ForEach(obj => Destroy(obj));

            foreach (var item in backPack.Items)
            {
                var go = new GameObject();
                go.transform.parent = transform;
                uiitems.Add(go);
                var img = go.AddComponent<UnityEngine.UI.Image>();
                img.sprite = item.Image;
            }
        }
    }
}