namespace WPDevToolkit.Interfaces
{
    /// <summary>
    /// Interface for items that can be filled from DTO content.
    /// </summary>
    /// <typeparam name="T">The DTO type containing the content values.</typeparam>
    public interface IFromDtoItem<in T>
    {
        void FillFromDto(T dto);
    }
}
