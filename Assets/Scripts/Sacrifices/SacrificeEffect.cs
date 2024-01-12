using UnityEngine;

namespace Sacrifices
{
    [System.Serializable]
    public class SacrificeEffect : ScriptableObject
    {
        public string positiveTooltipKey;
        public string negativeTooltipKey;
        public float positiveModifier;
        public float negativeModifier;
        public BodyPartSacrifice bodyPartSacrifice;

        public virtual void Apply()
        {
            //Apply actual effect on SacrificeController
            SacrificeController.Instance.SacrificeBodyPart(bodyPartSacrifice);
        }
    }
}