
namespace MvcAssetManager
{
	/// <summary>
	/// Stores the resource transformation engines for the application.
	/// </summary>
	public static class ResourceTransforms
	{
		/// <summary>Gets a collection of the web resource transformations that derive from
		/// <see cref="IResourceTransform"/>.</summary>
		/// <returns>The web resource transformations.</returns>
		public static ResourceTransformCollection Transforms { get { return _transforms; } }

		private static readonly ResourceTransformCollection _transforms = new ResourceTransformCollection();
	}
}
