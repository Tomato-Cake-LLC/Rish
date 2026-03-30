using System;
using RishUI.MemoryManagement;
using UnityEngine.UIElements;

namespace RishUI
{
    public static partial class Rish
    {
        // -------------------------------------------------------------------------------------------------------------
        // --- PRIMITIVE ELEMENTS --------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------------------------
        
        // 0/4 -> 1
        [RequiresManagedContext]
        public static Element Create<T>(Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, VisualAttributes.Default, children);
        // 1/4 -> 4
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, VisualAttributes.Default, children);
        [RequiresManagedContext]
        public static Element Create<T>(RishString name, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, new VisualAttributes(VisualAttributes.Default)
        {
            name = name
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(ClassName className, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, new VisualAttributes(VisualAttributes.Default)
        {
            className = className
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, new VisualAttributes(VisualAttributes.Default)
        {
            style = style
        }, children);
        // 2/4 -> 6
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, RishString name, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, new VisualAttributes(VisualAttributes.Default)
        {
            name = name
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, ClassName className, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, new VisualAttributes(VisualAttributes.Default)
        {
            className = className
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, new VisualAttributes(VisualAttributes.Default)
        {
            style = style
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(RishString name, ClassName className, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, new VisualAttributes(VisualAttributes.Default)
        {
            name = name,
            className = className,
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(RishString name, Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, new VisualAttributes(VisualAttributes.Default)
        {
            name = name,
            style = style
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(ClassName className, Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, new VisualAttributes(VisualAttributes.Default)
        {
            className = className,
            style = style
        }, children);
        // 3/4 -> 4
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, RishString name, ClassName className, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, new VisualAttributes(VisualAttributes.Default)
        {
            name = name,
            className = className
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, RishString name, Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, new VisualAttributes(VisualAttributes.Default)
        {
            name = name,
            style = style
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, ClassName className, Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, new VisualAttributes(VisualAttributes.Default)
        {
            className = className,
            style = style
        }, children);
        [RequiresManagedContext]
        public static Element Create<T>(RishString name, ClassName className, Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(0, new VisualAttributes(VisualAttributes.Default)
        {
            name = name,
            className = className,
            style = style
        }, children);
        // 4/4 -> 1
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, RishString name, ClassName className, Style style, Children? children = null) where T : VisualElement, IVisualElement, new() => Create<T>(key, new VisualAttributes(VisualAttributes.Default)
        {
            name = name,
            className = className,
            style = style
        }, children);
        // Attributes
        [RequiresManagedContext]
        public static Element Create<T>(VisualAttributes attributes, Children? children = null)
            where T : VisualElement, IVisualElement, new() => Create<T>(0, attributes, children);
        [RequiresManagedContext]
        public static Element Create<T>(ulong key, VisualAttributes attributes, Children? children = null) where T : VisualElement, IVisualElement, new()
        {
            var (id, element) = GetFree<VisualDefinition<T>>();
            element.Factory(key, attributes, default, children ?? Children.Null);
            
            return new Element(id);
        }
        
        [RequiresManagedContext]
        public static Element Create<T, P>(ulong key, P props, VisualAttributes attributes, Children? children = null) where T : VisualElement, IVisualElement<P>, new() where P : struct
        {
            var (id, element) = GetFree<VisualDefinition<T, P>>();
            element.Factory(key, attributes, props, children ?? Children.Null);

            return new Element(id);
        }

        private class VisualDefinition<T, P> : ManagedElement where T : VisualElement, IVisualElement<P>, new() where P : struct
        {
            public override Type Type => typeof(T);
            
            private VisualAttributes Attributes { get; set; }
            private P Props { get; set; }
            private Children Children { get; set; }

            public void Factory(ulong key, VisualAttributes attributes, P props, Children children)
            {
                Key = key;
                Attributes = attributes;
                Props = props;
                Children = children;
                
                OwnerContext.AddDependencies(Props);
                OwnerContext.AddDependencies(attributes.className);
                OwnerContext.AddDependencies(Children);
            }

#if UNITY_EDITOR
            internal override void Invoke(Node parent, bool chain, string debugPrefix)
#else
            internal override void Invoke(Node parent, bool chain)
#endif
            {
#if UNITY_EDITOR
                var node = parent.AddChild<T>(Key, debugPrefix);
#else
                var node = parent.AddChild<T>(Key);
#endif
                if (node is not { Element: T element }) return;
                
#if UNITY_EDITOR
                element.Bridge.Setup(Attributes, Children, Props, chain, debugPrefix);
#else
                element.Bridge.Setup(Attributes, Children, Props, chain);
#endif
            }

            public override bool Equals(ManagedElement other)
            {
                return other is VisualDefinition<T, P> otherDefinition && Key == otherDefinition.Key && RishUtils.SmartCompare(Props, otherDefinition.Props) && RishUtils.Compare(Attributes, otherDefinition.Attributes) && RishUtils.Compare(Children, otherDefinition.Children);
            }
            
            public override bool TryGetProps<P1>(out P1 props)
            {
                props = default;
                if (Props is not P1 p)
                {
                    return false;
                }

                props = p;
                return true;
            }
            // public override bool TrySetProps<P1>(P1 props)
            // {
            //     if (props is not P p)
            //     {
            //         return false;
            //     }
            //
            //     Factory(Key, Attributes, p, Children);
            //     return true;
            // }
        }

        private class VisualDefinition<T> : VisualDefinition<T, NoProps> where T : VisualElement, IVisualElement, new() { }
    } 
}