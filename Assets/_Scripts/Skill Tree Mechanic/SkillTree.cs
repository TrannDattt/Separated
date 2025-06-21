using System.Linq;
using Separated.Data;
using UnityEngine;

namespace Separated.SkillTrees
{
    public class SkillTree : MonoBehaviour
    {
        private SkillNode[] _skillNodes;

        public void InitSkillTree(SkillNodeData[] nodeDatas)
        {
            _skillNodes = new SkillNode[nodeDatas.Length];

            foreach (var data in nodeDatas)
            {
                var newNode = new SkillNode(data);
                _skillNodes.Append(newNode);
            }
        }
    }
}