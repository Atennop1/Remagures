using System.Numerics;
using Vector3 = UnityEngine.Vector3;

namespace Remagures.Model.Knockback
{
    public interface IKnocker
    {
        void Knock(IKnockable knockable, Vector3 direction);
    }
}