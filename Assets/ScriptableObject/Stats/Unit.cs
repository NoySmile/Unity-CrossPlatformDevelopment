using UnityEngine;

namespace ScriptableAssets
{
    [CreateAssetMenu(menuName = "Scriptables/Unit")]
    public class Unit : ScriptableObject
    {
        [HideInInspector] public Stats _stats;

        public string Name;


    }
}