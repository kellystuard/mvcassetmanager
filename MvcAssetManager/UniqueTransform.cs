using System;
using System.Collections.Generic;
using System.Linq;
using MvcAssetManager.Helpers;

namespace MvcAssetManager
{
	/// <summary>
	/// Allows for removal of duplicates during transforms.
	/// </summary>
	/// <remarks>
	/// This can be very useful, for example, when scripts are entered multiple times into 
	/// <see cref="ResourcesHelper"/> or when the transformations produce duplicates.
	/// </remarks>
	public sealed class UniqueTransform : IResourceTransform
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UniqueTransform"/> class.
		/// </summary>
		/// <param name="resourceType">Type of the resource.</param>
		public UniqueTransform(ResourceType resourceType)
		{
			ResourceType = resourceType;
		}

		/// <summary>Gets the type of the web resource.</summary>
		/// <value>The type of the web resource.</value>
		public ResourceType ResourceType { get; private set; }

		/// <summary>
		/// Performs any transforms on the web <paramref name="resources" /> passed in and returns the new list.
		/// </summary>
		/// <param name="resources">List of web resources to process.</param>
		/// <param name="resourceType">Web resource types being processed.</param>
		/// <returns>
		/// Transformed list of web resources.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">resources</exception>
		IEnumerable<string> IResourceTransform.ProcessResources(IEnumerable<string> resources, ResourceType resourceType)
		{
			if (resources == null) throw new ArgumentNullException("resources");

			if (!ResourceType.HasFlag(resourceType))
				return resources;

			return resources.Distinct();
		}
	}
}
