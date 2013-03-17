using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcAssetManager
{
	/// <summary>
	/// Allows for substituting a concatenated web resource, in pace of the original, during transforms.
	/// </summary>
	/// <remarks>
	/// This can be very useful, for example, to register all the necessary scripts and styles without worrying about 
	/// concatenation, while creating the view.
	/// </remarks>
	public sealed class ConcatenationTransform : BaseResourceTransform
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConcatenationTransform"/> class.
		/// </summary>
		/// <param name="resourceType">Type of the resource.</param>
		/// <param name="resourceScope">The resource scope.</param>
		public ConcatenationTransform(ResourceType resourceType, ResourceScope resourceScope)
			: base(resourceType, resourceScope)
		{
			Groups = new Dictionary<string, string>();
		}

		/// <summary>
		/// Gets the mappings, where the key is the resource and the value is the group name that should be returned.
		/// </summary>
		/// <value>
		/// The group mappings.
		/// </value>
		public IDictionary<string, string> Groups { get; private set; }

		/// <summary>
		/// Adds the mapping where any <paramref name="resourceNames"/> passed through <see cref="ProcessResource"/> 
		/// will instead return <paramref name="groupName"/> (but only once).
		/// </summary>
		/// <remarks>Adds to <see cref="Groups"/>.</remarks>
		/// <param name="groupName">Name of the group.</param>
		/// <param name="resourceNames">The resource names.</param>
		/// <returns>Fluent result (this).</returns>
		/// <exception cref="System.ArgumentNullException">
		/// groupName
		/// or
		/// resourceNames
		/// </exception>
		public IResourceTransform AddGroup(string groupName, IEnumerable<string> resourceNames)
		{
			if (groupName == null) throw new ArgumentNullException("groupName");
			if (resourceNames == null) throw new ArgumentNullException("resourceNames");

			foreach (var resource in resourceNames)
				Groups.Add(resource, groupName);

			return this;
		}

		/// <summary>
		/// Performs any transforms on the web <paramref name="resources" /> passed in and returns the new list.
		/// </summary>
		/// <param name="resources">List of web resources to process.</param>
		/// <param name="resourceType">Web resource types being processed.</param>
		/// <returns>
		/// Transformed list of web resources.
		/// </returns>
		public override IEnumerable<string> ProcessResources(IEnumerable<string> resources, ResourceType resourceType)
		{
			var result = base.ProcessResources(resources, resourceType);

			return ResourceType.HasFlag(resourceType)
				? result.Distinct() : result;
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

			string result;
			return Groups.TryGetValue(resource, out result)
				? result : resource;
		}
	}
}
