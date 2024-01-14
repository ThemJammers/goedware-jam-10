using halbautomaten.UnityTools.UiTools;
using Sacrifices;

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
