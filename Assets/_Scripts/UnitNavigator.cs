using Separated.Data;
using UnityEngine;

namespace Separated.Unit
{
    public class UnitNavigator
    {
        private AttackSkillData _attackData;

        public void SetAttackData(AttackSkillData attackData)
        {
            _attackData = attackData;
        }

        public Vector2 GetMoveDirection(Transform target, Transform self)
        {
            if (target == null) return Vector2.zero;

            var direction = target.position - self.position;
            return direction.normalized;
        }

        public bool CheckInTriggerRange(Transform target, Transform self, Vector2 range)
        {
            if (target == null) return false;

            var distance = target.position - self.position;
            return distance.x <= range.x && distance.y <= range.y;
        }
        
        public bool CheckInAttackRange(Transform target, Transform self)
        {
            if (target == null) return false;

            var range = _attackData.Range;
            var distance = target.position - self.position;
            return distance.x <= range.x && distance.y <= range.y;
        }
    }
}
