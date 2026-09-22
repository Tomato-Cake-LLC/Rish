#nullable enable
using System;
using UnityEngine;

namespace RishUI
{
    // Declarative navigation descriptor passed to any visual element's Create() factory.
    // The nearest ancestor INavigationRegistrar (NavigationGroup) auto-registers the element.
    //
    // We can't use [RishValueType] here because Action? is a nullable reference type and
    // crashes ComparersGenerator. Manual [ComparersProvider]/[Comparer] is used instead.
    // The comparer skips delegate fields — same rule as [RishValueType]-generated comparers.
    [ComparersProvider]
    public struct Navigable
    {
        public Action action;
        // Fired when the user holds the button and releases — null if not a hold button.
        public Action? holdEndAction;
        // Fired by INavigationRegistrar when gamepad focus changes — null if no side-effect needed.
        public Action<bool>? onFocusChanged;
        // Directional input while focused, +X right and +Y up. Return true to eat it so focus stays
        // put: that is how a slider takes Left/Right instead of hopping to the next control.
        public Func<Vector2, bool>? onAxis;
        // Fired when the direction is released, so onAxis can drive a hold-to-repeat.
        public Action? onAxisEnd;
        public bool interactable;
        public bool isDefault;
        public bool isBackButton;

        // Delegate fields intentionally skipped — they change every render (new lambdas) but are
        // functionally equivalent. Only structural fields affect registration behaviour.
        [Comparer]
        public static bool Equals(Navigable a, Navigable b) =>
            a.interactable == b.interactable
            && a.isDefault == b.isDefault
            && a.isBackButton == b.isBackButton;
    }

    // Implemented by NavigationGroup. Bridge discovers it via GetFirstAncestorOfType
    // and calls Register/Unregister/Update automatically when Navigable? is set on an element.
    public interface INavigationRegistrar
    {
        void RegisterNavigable(IBridge bridge, Navigable nav);
        void UnregisterNavigable(IBridge bridge);
        void UpdateNavigable(IBridge bridge, Navigable nav);
        // Called by NavigationGroup to push/pop focus pseudo-state on the element's VisualElement.
        void SetFocused(IBridge bridge, bool focused);
    }
}
