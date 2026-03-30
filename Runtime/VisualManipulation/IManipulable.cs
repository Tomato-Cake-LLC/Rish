using RishUI.MemoryManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace RishUI
{
    public interface IManipulable /*: IResolvedStyle, ITransform*/
    {
        int ID { get; }
        internal bool IsBubblingUp { get; }
        // public delegate void ClassNameAction(ref ClassName value);
        // public delegate void StyleAction(ref Style value);

        VisualElement element { get; }
        VisualElement parent => element.parent;

        string name { get; set; }
        
        [RequiresManagedContext]
        ClassName CloneClassName();
        [RequiresManagedContext]
        bool AddClassName(string className);
        [RequiresManagedContext]
        bool AddClassName(RishString className);
        [RequiresManagedContext]
        int AddClassName(ClassName className);
        [RequiresManagedContext]
        bool RemoveClassName(string className);
        [RequiresManagedContext]
        bool RemoveClassName(RishString className);
        [RequiresManagedContext]
        int RemoveClassName(ClassName className);
        void SetClassName(ClassName className);
        ManipulableStyle style { get; }

        internal void ApplyInlineStyle();

        // void SetClassName(ClassNameAction action)
        // {
        //     if (action == null) return;
        //     var className = CloneClassName();
        //     action(ref className);
        //     SetClassName(className);
        // }
        // void SetStyle(StyleAction action)
        // {
        //     if (action == null) return;
        //     var style = CloneStyle();
        //     action(ref style);
        //     SetStyle(style);
        // }

        Rect layout => element.layout;
        Rect contentRect => element.contentRect;
        Rect worldBound => element.worldBound;
        Rect localBound => element.localBound;
        Matrix4x4 worldTransform => element.worldTransform;
        PickingMode pickingMode => element.pickingMode;
        
        // IResolvedStyle resolvedStyle => element.resolvedStyle;
        // Align IResolvedStyle.alignContent => element.resolvedStyle.alignContent;
        // Align IResolvedStyle.alignItems => element.resolvedStyle.alignItems;
        // Align IResolvedStyle.alignSelf => element.resolvedStyle.alignSelf;
        // Color IResolvedStyle.backgroundColor => element.resolvedStyle.backgroundColor;
        // Background IResolvedStyle.backgroundImage => element.resolvedStyle.backgroundImage;
        // BackgroundPosition IResolvedStyle.backgroundPositionX => element.resolvedStyle.backgroundPositionX;
        // BackgroundPosition IResolvedStyle.backgroundPositionY => element.resolvedStyle.backgroundPositionY;
        // BackgroundRepeat IResolvedStyle.backgroundRepeat => element.resolvedStyle.backgroundRepeat;
        // BackgroundSize IResolvedStyle.backgroundSize => element.resolvedStyle.backgroundSize;
        // Color IResolvedStyle.borderBottomColor => element.resolvedStyle.borderBottomColor;
        // float IResolvedStyle.borderBottomLeftRadius => element.resolvedStyle.borderBottomLeftRadius;
        // float IResolvedStyle.borderBottomRightRadius => element.resolvedStyle.borderBottomRightRadius;
        // float IResolvedStyle.borderBottomWidth => element.resolvedStyle.borderBottomWidth;
        // Color IResolvedStyle.borderLeftColor => element.resolvedStyle.borderLeftColor;
        // float IResolvedStyle.borderLeftWidth => element.resolvedStyle.borderLeftWidth;
        // Color IResolvedStyle.borderRightColor => element.resolvedStyle.borderRightColor;
        // float IResolvedStyle.borderRightWidth => element.resolvedStyle.borderRightWidth;
        // Color IResolvedStyle.borderTopColor => element.resolvedStyle.borderTopColor;
        // float IResolvedStyle.borderTopLeftRadius => element.resolvedStyle.borderTopLeftRadius;
        // float IResolvedStyle.borderTopRightRadius => element.resolvedStyle.borderTopRightRadius;
        // float IResolvedStyle.borderTopWidth => element.resolvedStyle.borderTopWidth;
        // float IResolvedStyle.bottom => element.resolvedStyle.bottom;
        // Color IResolvedStyle.color => element.resolvedStyle.color;
        // DisplayStyle IResolvedStyle.display => element.resolvedStyle.display;
        // UnityEngine.UIElements.StyleFloat IResolvedStyle.flexBasis => element.resolvedStyle.flexBasis;
        // FlexDirection IResolvedStyle.flexDirection => element.resolvedStyle.flexDirection;
        // float IResolvedStyle.flexGrow => element.resolvedStyle.flexGrow;
        // float IResolvedStyle.flexShrink => element.resolvedStyle.flexShrink;
        // Wrap IResolvedStyle.flexWrap => element.resolvedStyle.flexWrap;
        // float IResolvedStyle.fontSize => element.resolvedStyle.fontSize;
        // float IResolvedStyle.height => element.resolvedStyle.height;
        // Justify IResolvedStyle.justifyContent => element.resolvedStyle.justifyContent;
        // float IResolvedStyle.left => element.resolvedStyle.left;
        // float IResolvedStyle.letterSpacing => element.resolvedStyle.letterSpacing;
        // float IResolvedStyle.marginBottom => element.resolvedStyle.marginBottom;
        // float IResolvedStyle.marginLeft => element.resolvedStyle.marginLeft;
        // float IResolvedStyle.marginRight => element.resolvedStyle.marginRight;
        // float IResolvedStyle.marginTop => element.resolvedStyle.marginTop;
        // UnityEngine.UIElements.StyleFloat IResolvedStyle.maxHeight => element.resolvedStyle.maxHeight;
        // UnityEngine.UIElements.StyleFloat IResolvedStyle.maxWidth => element.resolvedStyle.maxWidth;
        // UnityEngine.UIElements.StyleFloat IResolvedStyle.minHeight => element.resolvedStyle.minHeight;
        // UnityEngine.UIElements.StyleFloat IResolvedStyle.minWidth => element.resolvedStyle.minWidth;
        // float IResolvedStyle.opacity => element.resolvedStyle.opacity;
        // float IResolvedStyle.paddingBottom => element.resolvedStyle.paddingBottom;
        // float IResolvedStyle.paddingLeft => element.resolvedStyle.paddingLeft;
        // float IResolvedStyle.paddingRight => element.resolvedStyle.paddingRight;
        // float IResolvedStyle.paddingTop => element.resolvedStyle.paddingTop;
        // Position IResolvedStyle.position => element.resolvedStyle.position;
        // float IResolvedStyle.right => element.resolvedStyle.right;
        // Rotate IResolvedStyle.rotate => element.resolvedStyle.rotate;
        // Scale IResolvedStyle.scale => element.resolvedStyle.scale;
        // TextOverflow IResolvedStyle.textOverflow => element.resolvedStyle.textOverflow;
        // float IResolvedStyle.top => element.resolvedStyle.top;
        // Vector3 IResolvedStyle.transformOrigin => element.resolvedStyle.transformOrigin;
        // IEnumerable<TimeValue> IResolvedStyle.transitionDelay => element.resolvedStyle.transitionDelay;
        // IEnumerable<TimeValue> IResolvedStyle.transitionDuration => element.resolvedStyle.transitionDuration;
        // IEnumerable<StylePropertyName> IResolvedStyle.transitionProperty => element.resolvedStyle.transitionProperty;
        // IEnumerable<EasingFunction> IResolvedStyle.transitionTimingFunction => element.resolvedStyle.transitionTimingFunction;
        // Vector3 IResolvedStyle.translate => element.resolvedStyle.translate;
        // Color IResolvedStyle.unityBackgroundImageTintColor => element.resolvedStyle.unityBackgroundImageTintColor;
        // Font IResolvedStyle.unityFont => element.resolvedStyle.unityFont;
        // FontDefinition IResolvedStyle.unityFontDefinition => element.resolvedStyle.unityFontDefinition;
        // FontStyle IResolvedStyle.unityFontStyleAndWeight => element.resolvedStyle.unityFontStyleAndWeight;
        // float IResolvedStyle.unityParagraphSpacing => element.resolvedStyle.unityParagraphSpacing;
        // int IResolvedStyle.unitySliceBottom => element.resolvedStyle.unitySliceBottom;
        // int IResolvedStyle.unitySliceLeft => element.resolvedStyle.unitySliceLeft;
        // int IResolvedStyle.unitySliceRight => element.resolvedStyle.unitySliceRight;
        // float IResolvedStyle.unitySliceScale => element.resolvedStyle.unitySliceScale;
        // int IResolvedStyle.unitySliceTop => element.resolvedStyle.unitySliceTop;
        // TextAnchor IResolvedStyle.unityTextAlign => element.resolvedStyle.unityTextAlign;
        // Color IResolvedStyle.unityTextOutlineColor => element.resolvedStyle.unityTextOutlineColor;
        // float IResolvedStyle.unityTextOutlineWidth => element.resolvedStyle.unityTextOutlineWidth;
        // TextOverflowPosition IResolvedStyle.unityTextOverflowPosition => element.resolvedStyle.unityTextOverflowPosition;
        // Visibility IResolvedStyle.visibility => element.resolvedStyle.visibility;
        // WhiteSpace IResolvedStyle.whiteSpace => element.resolvedStyle.whiteSpace;
        // float IResolvedStyle.width => element.resolvedStyle.width;
        // float IResolvedStyle.wordSpacing => element.resolvedStyle.wordSpacing;
        // UnityEngine.UIElements.StyleEnum<ScaleMode> IResolvedStyle.unityBackgroundScaleMode => element.resolvedStyle.unityBackgroundScaleMode;

        // ITransform transform => element.transform;
        // Vector3 ITransform.position
        // {
        //     get => element.transform.position;
        //     set => element.transform.position = value;
        // }
        // Quaternion ITransform.rotation
        // {
        //     get => element.transform.rotation;
        //     set => element.transform.rotation = value;
        // }
        // Vector3 ITransform.scale
        // {
        //     get => element.transform.scale;
        //     set => element.transform.scale = value;
        // }
        // Matrix4x4 ITransform.matrix => element.transform.matrix;
    }
}