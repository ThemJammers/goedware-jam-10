using System;
using Core;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Powerups/HealthBuff")]
    public class DamageBuff : PowerupEffect
    {
        public int amount;

        public override void Apply(GameCharacter character)
        {
            character.BaseDamage += amount;
        }
    }
}
