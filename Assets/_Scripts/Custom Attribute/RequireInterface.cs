using System;
using UnityEngine;

namespace Separated.CustomAttribute
{
    public class RequireInterface : PropertyAttribute
    {
        public Type RequiredType { get; }

        public RequireInterface(Type type)
        {
            RequiredType = type;
        }
    }
}