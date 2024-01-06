using ThemJammers.Player;

namespace ThemJammers.Interfaces
{
    public interface ICollectible
    {
        public void Collect(PlayerController playerController);
        public void Dispose();
    }
}
