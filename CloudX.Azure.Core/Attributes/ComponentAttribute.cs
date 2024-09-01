﻿using NUnit.Framework;

namespace CloudX.Azure.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ComponentAttribute : PropertyAttribute
    {
        public string ComponentName { get; }

        public ComponentAttribute(string componentName) : base(componentName)
        {
            ComponentName = componentName;
        }
    }
}