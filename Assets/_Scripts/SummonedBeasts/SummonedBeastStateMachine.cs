using Separated.Data;
using Separated.Helpers;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class SummonedBeastStateMachine : BaseStateMachine<SummonedBeastStateMachine.EBeastState>
    {
        public enum EBeastState
        {
            Idle,
            Run,
            Skill,
        }

        [SerializeField] private IdleStateData _idleData;
        [SerializeField] private RunStateData _runData;
        [SerializeField] private SkillStateData[] _skillDatas;

        private SummonedBeastControl _beast;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public void InitSM()
        {
            _stateDict.Clear();

            // _stateDict.Add(EBeastState.Idle, new Idle(EBeastState.Idle, _animator, _beast));
            // _stateDict.Add(EBeastState.Run, new Run(EBeastState.Run, _animator, _beast));
            // _stateDict.Add(EBeastState.Attack, new Attack(EBeastState.Attack, _animator, _beast));
        }

        void Start()
        {
            _beast = GetComponent<SummonedBeastControl>();
            _animator = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}
