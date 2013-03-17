using System;
using System.Collections.Generic;

namespace MvcAssetManager
{
	/// <summary>
	/// Allows for a simple list of source-to-destination transforms.
	/// </summary>
	/// <remarks>
	/// This can be very useful, for example, to map script names to CDN locations.
	/// </remarks>
	public sealed class PathOverrideTransform : BaseResourceTransform
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PathOverrideTransform"/> class.
		/// </summary>
		/// <param name="resourceType">Type of the resource.</param>
		/// <param name="resourceScope">The resource scope.</param>
		public PathOverrideTransform(ResourceType resourceType, ResourceScope resourceScope)
			: base(resourceType, resourceScope)
		{
			Overrides = new Dictionary<string, string>();
		}

		/// <summary>Gets the overrides, where the key is the resource and the value is the new resource that should 
		/// be returned.</summary>
		/// <value>The overrides.</value>
		public IDictionary<string, string> Overrides { get; private set; }

		/// <summary>
		/// Adds the override where any <paramref name="originalLocation"/> passed through <see cref="ProcessResource"/> 
		/// will instead return <paramref name="overrideLocation"/>.
		/// </summary>
		/// <remarks>Adds to <see cref="Overrides"/>.</remarks>
		/// <param name="originalLocation">The original location.</param>
		/// <param name="overrideLocation">The override location.</param>
		/// <returns>Fluent result (this).</returns>
		/// <exception cref="System.ArgumentNullException">
		/// originalLocation
		/// or
		/// overrideLocation
		/// </exception>
		public PathOverrideTransform AddOverride(string originalLocation, string overrideLocation)
		{
			if (originalLocation == null) throw new ArgumentNullException("originalLocation");
			if (overrideLocation == null) throw new ArgumentNullException("overrideLocation");

			Overrides.Add(originalLocation, overrideLocation);

			return this;
		}

		/// <summary>
		/// Performs any transforms on the web <paramref name="resource" /> passed in and returns the new resource.
		/// </summary>
		/// <param name="resource">The web resource to process.</param>
		/// <returns>
		/// Transformed web resource.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">resource</exception>
		protected override string ProcessResource(string resource)
		{
			if (resource == null) throw new ArgumentNullException("resource");

			string transformedResource;
			return (Overrides.TryGetValue(resource, out transformedResource))
				? transformedResource : resource;
		}
	}
}
