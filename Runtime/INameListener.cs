namespace RishUI
{
    /// <summary>
    /// Visual name attribute event listener.
    /// </summary>
    public interface INameListener
    {
        /// <summary>
        /// Gets called right after a VisualElement gets its name set. 
        /// </summary>
        void NameSet(RishString name);
    }
}