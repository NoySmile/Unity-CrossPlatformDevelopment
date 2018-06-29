using System;
using CrossPlatformDevelopment.ScriptableObject.Stats;
using UnityEngine;
using UnityEngine.Events;
using Modifier = CrossPlatformDevelopment.Behaviours.Import.RPGStats.Modifier;

namespace CrossPlatformDevelopment.Behaviours
{
    public abstract class CharacterBehaviour : MonoBehaviour
    {
        [Serializable]
        public class OnStatModify : UnityEvent<string> { }


        [Serializable]
        public class OnHealthChange : UnityEvent<int> { }

        protected int modcount;
        [SerializeField]
        protected Stats CharacterStats;
        public OnHealthChange onHealthChange = new OnHealthChange();
        public OnStatModify onStatModify = new OnStatModify();
    
        public virtual void ModifyStat(Stat stat, Modifier mod) { }
    }
}
