using System;
using System.Runtime.Serialization;

namespace CrossPlatformDevelopment.Behaviours.Import.Combat
{
    [DataContract]
    public class Dagger : Weapon
    {
        public Dagger()
        {
            seed = new Random();
        }

        [DataMember]
        public string Name { get; set; }

        //1d4 damages
        //then add the stat modifier
        //dmg is rolling the weapons dice and adding the attackers stat
        public override int Roll()
        {
            return seed.Next(1, 5);
        }
    }
}