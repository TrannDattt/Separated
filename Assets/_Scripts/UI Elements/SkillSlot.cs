using System.Collections;
using Separated.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Separated.UIElements
{
    public class SkillSlot : GameUI
    {
        [SerializeField] private CanvasGroup _parentCanvasGroup;
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Image _cooldownImg;

        public override void Hide()
        {

        }

        public override void Initialize()
        {

        }

        public override void Show()
        {

        }

        public void ChangeSkill(SkillDescriptionData skillDescription)
        {
            _skillIcon.sprite = skillDescription.SkillIcon;
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