using Separated.Data;
using Separated.Enums;
using Separated.Player;
using UnityEngine;

namespace Separated.Unit
{
    public class UnitNavigator
    {
        private SkillStateData _skillData;
        private GameObject _unit;
        private Vector2 _triggerRange;
        private Vector2 _attackRange => _skillData.Range;

        public BaseUnit Target { get; private set; }

        public UnitNavigator(GameObject unit, Vector2 triggerRange, EUnitType targetType)
        {
            _unit = unit;
            _triggerRange = triggerRange;

            switch (targetType)
            {
                case EUnitType.Player:
                    Target = PlayerControl.Instance;
                    break;

                case EUnitType.Enemy:
                    GameObject.FindGameObjectWithTag("Enemy");
                    // TODO: Find all enemy in scene and get the nearest one
                    break;

                default:
                    break;
            }
        }

        public void SetSkillData(SkillStateData skillData)
        {
            _skillData = skillData;
        }

        public Vector2 GetMoveDirection()
        {
            if (Target == null) return Vector2.zero;

            var direction = Target.transform.position - _unit.transform.position;
            return direction.normalized;
        }

        public bool CheckInTriggerRange()
        {
            if (Target == null) return false;

            var distance = Target.transform.position - _unit.transform.position;
            return Mathf.Abs(distance.x) <= _triggerRange.x && Mathf.Abs(distance.y) <= _triggerRange.y;
        }
        
        public bool CheckInAttackRange()
        {
            if (Target == null) return false;

            var distance = Target.transform.position - _unit.transform.position;
            return Mathf.Abs(distance.x) <= _attackRange.x && Mathf.Abs(distance.y) <= _attackRange.y;
        }
    }
}
