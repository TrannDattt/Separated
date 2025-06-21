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

        public override void Hide()
        {

        }

        public void Initialize()
        {

        }

        public override void Show()
        {

        }

        // public void ChangeSkill(SkillDescriptionData skillDescription)
        // {
        //     _skillIcon.sprite = skillDescription.SkillIcon;
        // }

        public void CooldownSkill(SkillStateData skillData)
        {
            StartCoroutine(CooldownCoroutine(skillData));
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