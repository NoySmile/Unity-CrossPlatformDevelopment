
using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
[CreateAssetMenu(menuName = "Items/KevlarVest")]
public class KevlarVest : Armor
{
    [SerializeField]
    private Modifier modifier;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        _itemID = GetHashCode();
        name = _itemName;
        DisplayName = _itemName;
        if (obj == null)
            return;
    
    }


    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override void Equip()
    {
        base.Equip();
        var characterBehaviour = _owner.GetComponent<CharacterBehaviour>();
        modifier.Initialize(null);
        if (characterBehaviour == null)
            Debug.Log("should equip on a characterBehaviour");
        else
        {
            Assert.IsFalse(_itemID == 0);
            characterBehaviour.ModifyStat(_itemID, modifier.EffectedStat, modifier.mod);
        }
        
    }

    public override void UnEquip()
    {
        base.Equip();
        var characterBehaviour = _owner.GetComponent<CharacterBehaviour>();
        if(characterBehaviour == null)
            Debug.Log("should equip on a characterBehaviour");
        else
        {
            Assert.IsFalse(_itemID == 0);
            characterBehaviour.RemoveModifier(_itemID);
        }
    }

}