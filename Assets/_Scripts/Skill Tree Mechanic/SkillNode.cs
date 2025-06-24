using System.Linq;
using Separated.Data;
using UnityEngine.Events;

namespace Separated.SkillTrees
{
    public class SkillNode
    {
        public SkillNodeData Data { get; private set; }
        public string Id => Data.Id;
        public bool IsActive { get; private set; }
        public UnityEvent<SkillNode> OnActivated { get; private set; }

        private SkillNode[] _parents;

        public SkillNode(SkillNodeData nodeData)
        {
            Data = nodeData;
            IsActive = false;
            OnActivated = new();
        }

        public void SetParentNodes(SkillNode[] parentNodes)
        {
            _parents = parentNodes;
        }

        public bool CheckCanActivate() => _parents.Length == 0 || _parents.All(parent => parent.IsActive);

        public void Activate()
        {
            if (CheckCanActivate())
            {
                IsActive = true;
                OnActivated?.Invoke(this);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is SkillNode other && Data.Equals(other.Data);
        }

        public override int GetHashCode()
        {
            return Data.Id.GetHashCode();
        }
    }
}