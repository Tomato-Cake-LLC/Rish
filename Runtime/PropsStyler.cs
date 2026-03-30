using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace RishUI
{
    // TODO: We can source generate all of this with [Styled("--prop-name")] in the styled properties or something.
    public interface IStyledProps<P> where P : struct
    {
        void OnCustomStyle(IStyler styler, ref P props);
    }

    public interface IStyler
    {
        public bool TryGetValue(CustomStyleProperty<bool> customProperty, out bool customValue);
        public bool TryGetValue(CustomStyleProperty<int> customProperty, out int customValue);
        public bool TryGetValue(CustomStyleProperty<float> customProperty, out float customValue);
        public bool TryGetValue(CustomStyleProperty<Color> customProperty, out Color customValue);
        public bool TryGetValue(CustomStyleProperty<string> customProperty, out string customValue);
        public bool TryGetValue(CustomStyleProperty<Texture2D> customProperty, out Texture2D customValue);
        public bool TryGetValue(CustomStyleProperty<Sprite> customProperty, out Sprite customValue);
        public bool TryGetValue(CustomStyleProperty<VectorImage> customProperty, out VectorImage customValue);
        
        void SetValue(CustomStyleProperty<bool> customProperty, ref bool? propsValue, bool defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<int> customProperty, ref int? propsValue, int defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<float> customProperty, ref float? propsValue, float defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<Color> customProperty, ref Color? propsValue, Color defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<string> customProperty, ref FixedString32Bytes? propsValue, FixedString32Bytes defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<string> customProperty, ref FixedString64Bytes? propsValue, FixedString32Bytes defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<string> customProperty, ref FixedString128Bytes? propsValue, FixedString32Bytes defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<string> customProperty, ref FixedString512Bytes? propsValue, FixedString32Bytes defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<string> customProperty, ref FixedString4096Bytes? propsValue, FixedString32Bytes defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<string> customProperty, ref RishString? propsValue, RishString defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<string> customProperty, ref string propsValue, string defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<Texture2D> customProperty, ref Texture2D propsValue, Texture2D defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<Sprite> customProperty, ref Sprite propsValue, Sprite defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
        void SetValue(CustomStyleProperty<VectorImage> customProperty, ref VectorImage propsValue, VectorImage defaultValue = default)
        {
            propsValue ??= TryGetValue(customProperty, out var customValue) 
                ? customValue
                : defaultValue;
        }
    }
    
    public class PropsStyler<P> : IStyler where P : struct
    {
        private VisualElement Element { get; }
        private ICustomStyle CustomStyle => Element.customStyle;

        private P? _props;
        private P? Props
        {
            get => _props;
            set
            {
                _props = value;
                if (!value.HasValue) return;
                Setup();
            }
        }
        
        public PropsStyler(VisualElement element)
        {
            Element = element;
    
            element.RegisterCallback<DetachFromPanelEvent>(OnUnmounted);
            element.RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyle);
        }
        
        private void OnUnmounted(DetachFromPanelEvent evt) => Props = null;
        
        private void OnCustomStyle(CustomStyleResolvedEvent evt)
        {
            if (!Props.HasValue) return;
            Setup();
        }
    
        public void Setup(P props) => Props = props;

        private void Setup()
        {
            var props = Props.Value;
            if (Element is IStyledProps<P> styledPropsElement)
            {
                styledPropsElement.OnCustomStyle(this, ref props);
            }
#if UNITY_EDITOR
            else
            {
                throw new ArgumentException("Wrong type of IStyledProps.");
            }
#endif
            if (Element is IVisualElement<P> propsElement)
            {
                propsElement.Setup(props);
            }
#if UNITY_EDITOR
            else
            {
                throw new ArgumentException("Wrong type of IVisualElement.");
            }
#endif
        }
        
        bool IStyler.TryGetValue(CustomStyleProperty<bool> customProperty, out bool customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
        bool IStyler.TryGetValue(CustomStyleProperty<int> customProperty, out int customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
        bool IStyler.TryGetValue(CustomStyleProperty<float> customProperty, out float customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
        bool IStyler.TryGetValue(CustomStyleProperty<Color> customProperty, out Color customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
        bool IStyler.TryGetValue(CustomStyleProperty<string> customProperty, out string customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
        bool IStyler.TryGetValue(CustomStyleProperty<Texture2D> customProperty, out Texture2D customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
        bool IStyler.TryGetValue(CustomStyleProperty<Sprite> customProperty, out Sprite customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
        bool IStyler.TryGetValue(CustomStyleProperty<VectorImage> customProperty, out VectorImage customValue)
        {
            if (_props.HasValue) return CustomStyle.TryGetValue(customProperty, out customValue);
            
            customValue = default;
            return false;
        }
    }
}