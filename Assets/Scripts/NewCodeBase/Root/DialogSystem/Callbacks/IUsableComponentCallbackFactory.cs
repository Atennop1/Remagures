using Remagures.Model.DialogSystem;

namespace Remagures.Root.DialogSystem
{
    public interface IUsableComponentCallbackFactory //TODO make realization for this thing
    {
        void Create(IUsableComponent component);
    }
}