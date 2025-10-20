using UnityEngine;
using Zenject;

namespace MetaGame
{
    [CreateAssetMenu(fileName = "ValueServiceUpgrade", menuName = "MetaGame/UpgradesManager/New ValueServiceUpgrade")]
    public sealed class ValueService_UpgradeConfig : UpgradeConfig
    {
        [Space(SPACE_HEIGHT)]
        [SerializeReference] public IValueTable ValueTable;

        [Space(SPACE_HEIGHT)]
        [SerializeField] private FloatServiceName _targetService;

        [SerializeField] private ValueOperation _operation;
        [SerializeField] private string _buffId;
        [SerializeField] private int _operationOrder;

        public override Upgrade InstantiateUpgrade()
        {
            return new ValueServiceUpgrade(this, _targetService, _buffId, _operation, _operationOrder);
        }

        protected override void Validate()
        {
            base.Validate();
            ValueTable.OnValidate(maxLevel);
        }
    }
}