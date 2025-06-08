using UnityEngine;

namespace Separated.Data
{
    public abstract class BaseStatDataSO : ScriptableObject
    {
        public float Hp;
        public float Poise;

        public virtual void CopyData(BaseStatDataSO data)
        {
            Hp = data.Hp;
            Poise = data.Poise;
        }
    }
}