using Core;
using UnityEngine;

namespace Powerups
{
    public abstract class PowerupEffect : ScriptableObject
    {
        public abstract void Apply(GameCharacter character);
    }
}