using halbautomaten.UnityTools.UiTools;
using Sacrifices;
using UnityEngine;

namespace UI.Sacrifices
{
    public class SacrificeToggle : AnimatedToggle
    {
        public SacrificeEffect effect;
        protected override void OnToggleValueChanged(bool toggleState)
        {
            base.OnToggleValueChanged(toggleState);
            if(toggleState) SacrificeController.Instance.SetSacrificeEffect(effect);
        }
    }
}
