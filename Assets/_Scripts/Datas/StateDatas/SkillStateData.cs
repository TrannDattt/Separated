using System;
using Separated.CustomAttribute;
using Separated.Enums;
using Separated.Interfaces;
using Separated.Player;
using Separated.Poolings;
using Separated.Skills;
using Separated.SummonedBeasts;
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
            [RequireInterface(typeof(ISummonable))]
            public GameObject Prefabs;
            public int Amount;
        }

        public SummonObject[] SummonedObjects;

        public override void Do()
        {
        }

        public override void Exit()
        {
            var beastDatas = BeastDictionary.Instance.ActiveBeasts;
            foreach (var so in SummonedObjects)
            {
                for (int i = 0; i < so.Amount; i++)
                {
                    if (so.Prefabs.TryGetComponent(out BeastControl beast))
                    {
                        var offset = new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1, 1));
                        Debug.Log(beastDatas[i % 4]);
                        SummonedObjectPooling.GetBeastFromPool(beastDatas[i % 4], beast, offset);
                    }
                }
            }
        }
    }
}
