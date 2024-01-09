using JetBrains.Annotations;
using UI;

namespace Interfaces
{
    public interface IInteractable
    {
        public void Interact();

        [CanBeNull] public InteractionHint Hint { get; }
    }
}