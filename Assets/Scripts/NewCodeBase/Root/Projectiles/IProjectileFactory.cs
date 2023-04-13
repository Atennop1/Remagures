using Remagures.Model.Projectiles;

namespace Remagures.Root
{
    public interface IProjectileFactory
    {
        IProjectile Create();
    }
}