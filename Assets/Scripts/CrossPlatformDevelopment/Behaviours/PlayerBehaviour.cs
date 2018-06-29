using CrossPlatformDevelopment.ScriptableObject.GameState;
using CrossPlatformDevelopment.ScriptableObject.Stats;
using UnityEngine;
using Modifier = CrossPlatformDevelopment.Behaviours.Import.RPGStats.Modifier;

namespace CrossPlatformDevelopment.Behaviours
{
    public class PlayerBehaviour : CharacterBehaviour
    {

        //TODO : ADD THE TIMERFOR DEBUFFS AND DAMAGE OVER TIME, MAKE AN OBJECT TO HOLD THE ID OF THE MOD AND THE TIME TILL IT NEEDS TO GO AWAY

        void Awake()
        {
            if (CharacterStats == null)
            {
                Debug.LogWarning("You have not assigned a stats reference object... creating.");
                CharacterStats = Resources.Load<Stats>(@"Stats\PlayerStats");
            }
        
            GameState.Instance._player = new GameState.PlayerInfo(CharacterStats) { Name = name };
        }

        void Start()
        {
            onStatModify.Invoke("");
        }
    
        public override void ModifyStat(Stat stat, Modifier mod)
        {
            if (!this.CharacterStats.GetStat(stat.Name))
            {
                Debug.LogWarningFormat("stat:: {0}, is not a valid stat to modify", stat);
                return;
            }

            CharacterStats.AddModifier(modcount++, mod);

            if (stat.Name == "Health")
                onHealthChange.Invoke(CharacterStats[stat.Name].Value);
            onStatModify.Invoke(stat.Name);
        }

        public void ClearAll()
        {
            CharacterStats.ClearModifiers();
            onHealthChange.Invoke(CharacterStats["Health"].Value);
        }
    
    }
}