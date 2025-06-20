using Separated.Data;
using UnityEngine;

namespace Separated.Unit
{
    public class UnitNavigator
    {
        private SkillStateData _skillData;

        public void SetSkillData(SkillStateData skillData)
        {
            _skillData = skillData;
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
            return Mathf.Abs(distance.x) <= range.x && Mathf.Abs(distance.y) <= range.y;
        }
        
        public bool CheckInAttackRange(Transform target, Transform self)
        {
            if (target == null) return false;

            // Debug.Log(target.position);
            // Debug.Log(self.position);
            var range = _skillData.Range;
            var distance = target.position - self.position;
            // Debug.Log(distance.x <= range.x);
            // Debug.Log(distance.y <= range.y);
            return Mathf.Abs(distance.x) <= range.x && Mathf.Abs(distance.y) <= range.y;
        }
    }
}
