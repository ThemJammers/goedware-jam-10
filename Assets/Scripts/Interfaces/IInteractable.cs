using JetBrains.Annotations;

namespace Interfaces
{
    public interface IInteractable
    {
        public void Interact();

        [CanBeNull] public InteractionHint Hint { get; }
    }
}