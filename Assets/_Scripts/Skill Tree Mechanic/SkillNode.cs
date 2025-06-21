using System.Linq;
using Separated.Data;
using UnityEngine.Events;

namespace Separated.SkillTrees
{
    public class SkillNode
    {
        private SkillNodeData _nodeData;
        public UnityEvent<SkillNode> OnNodeActivated {get; private set;}

        public SkillNode(SkillNodeData nodeData)
        {
            _nodeData = nodeData;
            OnNodeActivated = new();
        }

        public bool CheckCanActivate() => _nodeData.ParentNodes.All(n => n.IsActive);

        public void ActivateNode()
        {
            if (CheckCanActivate())
            {
                _nodeData.IsActive = true;
                OnNodeActivated?.Invoke(this);
            }
        }
    }
}