using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using UnityEngine;
using UnityEngine.Video;

namespace Separated.SkillTrees
{
    public class SkillTree : MonoBehaviour
    {
        private BeastData _beastData;
        private SkillNode _rootPassive;
        private SkillNode _rootAction;
        private SortedDictionary<SkillNode, List<SkillNode>> _tree;

        public void InitSkillTree(BeastData beastData)
        {
            _beastData = beastData;
            _tree = new();

            foreach (var data in beastData.SkillNodes)
            {
                AddNode(data);
            }

            _rootPassive = GetNode(_beastData.DefaultPassiveSkill);
            _rootAction = GetNode(_beastData.DefaultActionSkill);
        }

        public void AddNode(SkillNodeData data)
        {
            var newNode = new SkillNode(data);
            if (!_tree.ContainsKey(newNode))
            {
                _tree.Add(newNode, new());
            }

            foreach (var parent in newNode.Data.ParentDatas)
            {
                var parentNode = new SkillNode(parent);

                if (!_tree.ContainsKey(parentNode))
                {
                    _tree.Add(parentNode, new());
                    _tree[newNode].Add(parentNode);
                }
                else
                {
                    var existedNode = _tree.Keys.FirstOrDefault(n => n.Equals(parentNode));
                    _tree[newNode].Add(existedNode);
                }
            }
        }

        public SkillNode GetNode(SkillNodeData data) => _tree.Keys.First(n => n.Data.Equals(data)) ?? new SkillNode(data);

        public List<SkillNode> GetParents(SkillNode child) => _tree[child];

        public bool CheckCanActiveNode(SkillNode node) => GetParents(node).All(p => p.IsActive);

        public void ActivateNode(SkillNode node) => node.Activate();

        public void ResetTree()
        {
            // TODO: Return tree to default - 1 passive and 1 action skill is activated
        }
    }
}