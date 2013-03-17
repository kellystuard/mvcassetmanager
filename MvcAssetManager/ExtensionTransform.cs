using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcAssetManager
{
	/// <summary>
	/// Allows for specifying default extension for web resources, during transforms.
	/// </summary>
	/// <remarks>
	/// This can be very useful, for example, to register scripts and styles without specifying file extensions.
	/// </remarks>
	public sealed class ExtensionTransform : BaseResourceTransform
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExtensionTransform"/> class.
		/// </summary>
		/// <param name="fromExtension">Extension to match on.</param>
		/// <param name="toExtension">Extension to change to.</param>
		/// <param name="resourceType">Type of the resource.</param>
		/// <param name="resourceScope">The resource scope.</param>
		/// <exception cref="System.ArgumentNullException">
		/// fromExtension
		/// or
		/// toExtension
		/// </exception>
		public ExtensionTransform(string fromExtension, string toExtension, ResourceType resourceType, ResourceScope resourceScope)
			: base(resourceType, resourceScope)
		{
			if (fromExtension == null) throw new ArgumentNullException("fromExtension");
			if (toExtension == null) throw new ArgumentNullException("toExtension");

			FromExtension = fromExtension;
			ToExtension = toExtension;
			IgnoreExtensions = new List<string>();
		}

		/// <summary>Gets extension to match on, during <see cref="ProcessResource"/>.</summary>
		/// <value>Extension to match on.</value>
		public string FromExtension { get; private set; }
		/// <summary>Gets extension to change to, during <see cref="ProcessResource"/>.</summary>
		/// <value>Extension to change to.</value>
		public string ToExtension { get; private set; }
		/// <summary>Gets extensions to ignore, during <see cref="ProcessResource"/>.</summary>
		/// <value>Extension to ignore.</value>
		public ICollection<string> IgnoreExtensions { get; private set; }

		/// <summary>
		/// Adds an extension to ignore.
		/// </summary>
		/// <remarks>Adds to <see cref="IgnoreExtensions"/>.</remarks>
		/// <param name="ignoreExtension">The extension to ignore.</param>
		/// <returns>Fluent result (this).</returns>
		/// <exception cref="System.ArgumentNullException">ignoreExtension</exception>
		public ExtensionTransform AddIgnore(string ignoreExtension)
		{
			if (ignoreExtension == null) throw new ArgumentNullException("ignoreExtension");

			IgnoreExtensions.Add(ignoreExtension);

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

			if (!resource.EndsWith(FromExtension, StringComparison.InvariantCultureIgnoreCase))
				return resource;
			if (resource.EndsWith(ToExtension, StringComparison.InvariantCultureIgnoreCase))
				return resource;
			if (IgnoreExtensions.Any(e => resource.EndsWith(e, StringComparison.InvariantCultureIgnoreCase)))
				return resource;

			return resource + ToExtension;
		}
	}
}
