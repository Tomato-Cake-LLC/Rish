namespace RishUI
{
    /// <summary>
    /// Visual class names attribute event listener.
    /// </summary>
    public interface IClassNameListener
    {
        /// <summary>
        /// Gets called right after a VisualElement gets its class names set. 
        /// </summary>
        void ClassNameSet(ClassName className);
    }
}