using Core;
using Player;
using UnityEngine;
using Weapons;

namespace Powerups
{
    public class PowerupEffect : ScriptableObject
    {
        public virtual void Apply(GameCharacter character)
        {
        }

        public virtual void Apply(PlayerWeaponController playerWeaponController)
        {
        }
    }
}