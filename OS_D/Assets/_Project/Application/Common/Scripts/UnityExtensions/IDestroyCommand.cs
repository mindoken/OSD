using UnityEngine;

namespace Common
{
    public interface IDestroyCommand<T>
        where T : Component
    {
        void Execute(T destroyedItem, bool culled = false);
    }
}