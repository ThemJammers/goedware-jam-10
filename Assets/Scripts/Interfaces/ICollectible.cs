using Player;

namespace Interfaces
{
    public interface ICollectible
    {
        public void Collect(PlayerController playerController);
        public void Dispose();
    }
}
