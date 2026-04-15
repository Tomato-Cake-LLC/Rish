// Unity 6.0 compatibility — UnityEngine.UIElements.SliceType was added in Unity 6.3.
// This stub lets Rish 3.0.0 compile on Unity 6.0.x without any behaviour change:
// the unitySliceType style property simply has no visual effect on 6.0.
// Remove this file once the project upgrades to Unity 6.3+.
#if !UNITY_6000_3_OR_NEWER
namespace UnityEngine.UIElements {
    public enum SliceType { Sliced = 0, Tiled = 1 }
}
#endif
