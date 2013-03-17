using System;
using System.Linq;

namespace MvcAssetManager
{
	/// <summary>
	/// Allows for appending version information to the url of a web resource, during transforms.
	/// </summary>
	/// <remarks>
	/// This can be very useful, for example, to register all the necessary scripts and styles without worrying about 
	/// browser cache, while creating the view.
	/// </remarks>
	public sealed class VersionTransform : BaseResourceTransform
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VersionTransform"/> class.
		/// </summary>
		/// <param name="resourceType">Type of the resource.</param>
		/// <param name="resourceScope">The resource scope.</param>
		/// <param name="versionValue">The version value to append to the end of each web resource.</param>
		/// <exception cref="System.ArgumentNullException">versionValue</exception>
		public VersionTransform(ResourceType resourceType, ResourceScope resourceScope, string versionValue)
			: base(resourceType, resourceScope)
		{
			if (versionValue == null) throw new ArgumentNullException("versionValue");

			VersionValue = versionValue;
			VersionName = "_";
		}

		/// <summary>Gets or sets the version value.</summary>
		/// <remarks>?VersionName=VersionValue</remarks>
		/// <value>The version value.</value>
		public string VersionValue { get; set; }
		/// <summary>Gets or sets the name of the version.</summary>
		/// <remarks>?VersionName=VersionValue</remarks>
		/// <value>The name of the version.</value>
		public string VersionName { get; set; }

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

			if (0 < resource.IndexOf(VersionName + "=", StringComparison.InvariantCultureIgnoreCase))
				return resource;

			var prepend = (resource.Contains('?'))
				? "&" : "?";

			return resource + prepend + VersionName + "=" + VersionValue;
		}
	}
}
