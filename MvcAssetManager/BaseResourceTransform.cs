using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcAssetManager
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class BaseResourceTransform : IResourceTransform
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseResourceTransform"/> class.
		/// </summary>
		/// <param name="resourceType">Type of the resource.</param>
		/// <param name="resourceScope">The resource scope.</param>
		protected BaseResourceTransform(ResourceType resourceType, ResourceScope resourceScope)
		{
			ResourceType = resourceType;
			ResourceScope = resourceScope;
		}

		public ResourceType ResourceType { get; protected set; }
		public ResourceScope ResourceScope { get; protected set; }

		/// <summary>
		/// Performs any transforms on the web <paramref name="resources" /> passed in and returns the new list.
		/// </summary>
		/// <param name="resources">List of web resources to process.</param>
		/// <param name="resourceType">Web resource types being processed.</param>
		/// <returns>
		/// Transformed list of web resources.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">resources</exception>
		public virtual IEnumerable<string> ProcessResources(IEnumerable<string> resources, ResourceType resourceType)
		{
			if (resources == null) throw new ArgumentNullException("resources");

			if (!ResourceType.HasFlag(resourceType))
				return resources;

			return
				from resource in resources
				let transform = ResourceScope.HasFlag(GetResourceScope(resource))
					? ProcessResource(resource) : resource
				where transform != null
				select transform;
		}

		/// <summary>
		/// Performs any transforms on the web <paramref name="resource" /> passed in and returns the new resource.
		/// </summary>
		/// <param name="resource">The web resource to process.</param>
		/// <returns>
		/// Transformed web resource.
		/// </returns>
		protected abstract string ProcessResource(string resource);

		private static ResourceScope GetResourceScope(string resource)
		{
			return (resource.StartsWith("//") || new Uri(resource, UriKind.RelativeOrAbsolute).IsAbsoluteUri)
				? ResourceScope.Remote : ResourceScope.Local;
		}
	}
}
