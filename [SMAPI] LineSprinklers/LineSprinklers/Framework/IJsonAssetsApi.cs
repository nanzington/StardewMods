namespace LineSprinklers.Framework
{
    /// <summary>The API methods provided by Json Assets used by this mod.</summary>
    public interface IJsonAssetsApi
    {
        /// <summary>Get the object ID for a given big craftable name.</summary>
        /// <param name="name">The item name.</param>
        int GetBigCraftableId(string name);
    }
}
