using System.Linq;
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

        public GameObject Target { get; private set; }

        public UnitNavigator(GameObject unit, Vector2 triggerRange, EUnitType targetType)
        {
            _unit = unit;
            _triggerRange = triggerRange;

            switch (targetType)
            {
                case EUnitType.Player:
                    Target = PlayerControl.Instance.gameObject;
                    break;

                case EUnitType.Enemy:
                    Target = GetNearestEnemy();
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

        private GameObject GetNearestEnemy()
        {
            var player = PlayerControl.Instance; 
            var allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            var nearestEnemy = allEnemies
                .OrderBy(enemy => Vector3.Distance(player.transform.position, enemy.transform.position))
                .FirstOrDefault();

            if (nearestEnemy == null)
            {
                return null;
            }

            float distance = Vector3.Distance(player.transform.position, nearestEnemy.transform.position);
            return distance <= _triggerRange.magnitude ? nearestEnemy : null;
        }
    }
}
