using Infrastructure;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "HintPopup_Pipeline", menuName = "UI/HintPopup/New Pipeline")]
    public sealed class HintPopup_Pipeline : ScriptableObject
    {
        public PopupName PopupName;
        public HintPopup Prefab;
    }
}