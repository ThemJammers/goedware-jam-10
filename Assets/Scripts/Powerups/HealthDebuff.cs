using Core;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Powerups/HealthDebuff")]
    public class HealthDebuff : PowerupEffect
    {
        public int amount;

        public override void Apply(GameCharacter character)
        {
            character.TakeTrueDamage(amount);
        }
    }
}