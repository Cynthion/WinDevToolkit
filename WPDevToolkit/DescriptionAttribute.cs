using System;

namespace WPDevToolkit
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
