using UnityEngine;

namespace CoreGame
{
    public abstract class UnityView : MonoBehaviour
    {
        public abstract void Initialize(bool culled);
    }
}