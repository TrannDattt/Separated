using System.Collections;
using Separated.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Separated.Views
{
    public class SkillSlot : GameUI
    {
        [SerializeField] private CanvasGroup _parentCanvasGroup;
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Image _cooldownImg;

        public SkillNodeData SkillData { get; private set; }

        public override void Hide()
        {

        }

        public void Initialize(SkillNodeData skillData)
        {
            SkillData = skillData;

            _skillIcon.sprite = skillData?.SkillIcon;
        }

        public override void Show()
        {

        }

        public void UpdateSkillView(SkillNodeData skillData)
        {
            SkillData = skillData;
            _skillIcon.sprite = skillData?.SkillIcon;
        }

        public void CooldownSkill()
        {
            if (!SkillData)
            {
                return;
            }

            StartCoroutine(CooldownCoroutine(SkillData.NodeSkillData as SkillStateData));
        }

        public IEnumerator CooldownCoroutine(SkillStateData skillData)
        {
            float time = skillData.Cooldown;
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                _cooldownImg.fillAmount = 1 - elapsedTime / time;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _cooldownImg.fillAmount = 0;
        }
    }
}