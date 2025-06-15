using System;
using NUnit.Framework;
using Separated.Enums;
using Separated.Unit;
using SerializeReferenceEditor;
using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Skill")]
    public class SkillStateData : StateDataSO
    {
        [SerializeReference]
        [SR]
        public SkillPhase[] SkillPhases;

        public Vector2 Range;
        public float Cooldown;
    }

    [Serializable]
    public abstract class SkillPhase
    {
        public ESkillPhaseType SkillType;
        public float PhaseDuration;

        public abstract void Do();
        public abstract void Exit();
    }

    [SRName("Skill Phase/Attack")]
    public class AttackPhase : SkillPhase
    {
        public float Damage;
        public float PoiseDamage;

        public Vector2 KnockbackDir;
        public float KnockbackForce;

        private UnitHitbox _hitbox;

        public void Init(UnitHitbox hitbox)
        {
            _hitbox = hitbox;
            _hitbox.SetAttackData(this);
        }

        public override void Do()
        {
        }

        public override void Exit()
        {
        }
    }

    [SRName("Skill Phase/Buff")]
    public class BuffPhase : SkillPhase
    {
        public override void Do()
        {
        }

        public override void Exit()
        {
        }
    }

    [SRName("Skill Phase/Move")]
    public class MovePhase : SkillPhase
    {
        public float Speed;

        private Rigidbody2D _body;
        private int _faceDir;

        public void Init(Rigidbody2D body, int faceDir)
        {
            _body = body;
            _faceDir = faceDir;
        }

        public override void Do()
        {
            _body.linearVelocityX = Speed * _faceDir;
        }

        public override void Exit()
        {
            _body.linearVelocityX = 0;
        }
    }

    [SRName("Skill Phase/Summon")]
    public class SummonPhase : SkillPhase
    {
        [Serializable]
        public class SummonObject
        {
            public GameObject Prefabs;
            public int Amount;
        }

        public SummonObject[] SummonedObjects;

        public override void Do()
        {
        }

        public override void Exit()
        {
        }
    }
}
