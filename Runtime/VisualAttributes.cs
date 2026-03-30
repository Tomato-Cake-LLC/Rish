using RishUI.MemoryManagement;

namespace RishUI
{
    /// <summary>
    /// Styling provider. Holds a name, a list of class names and inline style.
    /// </summary>
    [RishValueType]
    public struct VisualAttributes
    {
        /// <summary>
        /// Used for identification and styling.
        /// </summary>
        public RishString name;
        /// <summary>
        /// USS class names.
        /// </summary>
        public ClassName className;
        /// <summary>
        /// Inline styling.
        /// </summary>
        public Style style;
    
        public static VisualAttributes Default => new();
        
        public VisualAttributes(VisualAttributes other)
        {
            name = other.name;
            className = other.className;
            style = other.style;
        }
        public VisualAttributes(RishString name, ClassName className, Style style)
        {
            this.name = name;
            this.className = className;
            this.style = style;
        }

        public static VisualAttributes operator +(VisualAttributes left, VisualAttributes right) => new()
        {
            name = right.name.IsEmpty ? left.name : right.name,
            className = left.className + right.className,
            style = left.style + right.style
        };

        public static VisualAttributes operator +(VisualAttributes left, ClassName right) => new()
        {
            name = left.name,
            className = left.className + right,
            style = left.style
        };

        public static VisualAttributes operator +(VisualAttributes left, Style right) => new()
        {
            name = left.name,
            className = left.className,
            style = left.style + right
        };
        
        public static implicit operator VisualAttributes(RishString name) => new()
        {
            name = name
        };
        public static implicit operator VisualAttributes(ClassName className) => new()
        {
            className = className
        };
        public static implicit operator VisualAttributes(Style style) => new()
        {
            style = style
        };
        
        public struct Overridable : IOverridable<VisualAttributes>
        {
            private readonly bool _custom;
            private readonly VisualAttributes _value;

            public Overridable(VisualAttributes value)
            {
                _custom = true;
                _value = value;
            }

            public static implicit operator Overridable(VisualAttributes value) => new(value);
            
            [RequiresManagedContext]
            public static implicit operator Overridable(RishString name) => (VisualAttributes)name;
            [RequiresManagedContext]
            public static implicit operator Overridable(ClassName className) => (VisualAttributes)className;
            [RequiresManagedContext]
            public static implicit operator Overridable(Style style) => (VisualAttributes)style;

            public VisualAttributes GetValue(VisualAttributes defaultValue) => _custom ? _value : defaultValue;
        }
    }
}
