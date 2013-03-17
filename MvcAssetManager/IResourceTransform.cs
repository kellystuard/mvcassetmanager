using System.Collections.Generic;

namespace MvcAssetManager
{
	/// <summary>
	/// Marks the class as a transformer of web resource paths.
	/// </summary>
	/// <seealso cref="ResourceTransforms"/>
	public interface IResourceTransform
	{
		/// <summary>
		/// Performs any transforms on the web <paramref name="resources"/> passed in and returns the new list.
		/// </summary>
		/// <param name="resources">List of web resources to process.</param>
		/// <param name="resourceType">Web resource types being processed.</param>
		/// <returns>Transformed list of web resources.</returns>
		IEnumerable<string> ProcessResources(IEnumerable<string> resources, ResourceType resourceType);
	}
}
