using System;
using System.Runtime.Serialization;

namespace CrossPlatformDevelopment.Behaviours.Import.Combat
{
    [DataContract]
    public class Club : Weapon
    {
        public Club()
        {
            seed = new Random();
        }

        //1d6 damages
        public override int Roll()
        {
            return seed.Next(1, 6);
        }
    }
}