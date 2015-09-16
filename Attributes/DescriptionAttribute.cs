#region
using System;

#endregion

namespace bscheiman.Common.Attributes {
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute {
        public string Description { get; set; }

        public DescriptionAttribute(string description) {
            Description = description;
        }
    }
}
