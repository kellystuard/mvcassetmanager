using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcAssetManager
{
	/// <summary>
	/// Represents a collection of web resource transformations that are available to the application.
	/// </summary>
	/// <seealso cref="ResourceTransforms.Transforms"/>
	public sealed class ResourceTransformCollection : Collection<IResourceTransform>
	{
		/// <summary>
		/// Initializes a new instance of the System.Web.Mvc.ViewEngineCollection class.
		/// </summary>
		public ResourceTransformCollection() { }

		/// <summary>
		/// Initializes a new instance of the class by using the specified list of web resource transformations.
		/// </summary>
		/// <param name="list">The list that is wrapped by the new collection.</param>
		/// <exception cref="ArgumentNullException"><paramref name="list"/> is null.</exception>
		public ResourceTransformCollection(IEnumerable<IResourceTransform> list)
		{
			if (list == null) throw new ArgumentNullException("list");

			foreach (var item in list)
				Add(item);
		}

		/// <summary>
		/// Processes the resources.
		/// </summary>
		/// <param name="resources">The resources.</param>
		/// <param name="resourceType">Type of the resource.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">resources</exception>
		public IEnumerable<string> ProcessResources(IEnumerable<string> resources, ResourceType resourceType)
		{
			if (resources == null) throw new ArgumentNullException("resources");

			foreach (var transform in this)
				resources = transform.ProcessResources(resources, resourceType);

			return resources;
		}
	}
}
