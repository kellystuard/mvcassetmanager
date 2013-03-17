using System;
using System.Web;

namespace MvcAssetManager
{
	/// <summary>
	/// Allows for specifying resources relative to the <see cref="RelativeRebasePath" />.
	/// </summary>
	/// <remarks>
	/// This can be very useful, for example, to add scripts as "foo.js" and have them map to "~/Scripts/foo.js", by
	/// setting <see cref="RelativeRebasePath" /> to "~/Scripts/{0}".
	/// </remarks>
	public sealed class LocalRebaseTransform : BaseResourceTransform
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LocalRebaseTransform"/> class.
		/// </summary>
		/// <param name="resourceType">Type of the resource.</param>
		/// <param name="relativeRebasePath">The relative rebase path for local resources.</param>
		/// <exception cref="System.ArgumentNullException">relativeRebasePath</exception>
		public LocalRebaseTransform(ResourceType resourceType, string relativeRebasePath)
			: base(resourceType, ResourceScope.Local)
		{
			if (relativeRebasePath == null) throw new ArgumentNullException("relativeRebasePath");

			ResourceType = resourceType;
			RelativeRebasePath = relativeRebasePath;
		}

		/// <summary>Gets the relative rebase path for local resources.</summary>
		/// <value>The relative rebase path.</value>
		public string RelativeRebasePath { get; private set; }

		/// <summary>
		/// Performs any transforms on the web <paramref name="resource" /> passed in and returns the new resource.
		/// </summary>
		/// <param name="resource">The web resource to process.</param>
		/// <returns>
		/// Transformed web resource.
		/// </returns>
		/// <exception cref="System.InvalidOperationException">Cannot rebase relative path when there is no HttpContext.Current</exception>
		protected override string ProcessResource(string resource)
		{
			if (!resource.StartsWith("~/") && !resource.StartsWith("/"))
				resource = string.Format(RelativeRebasePath, resource);

			var context = HttpContext.Current;
			if (context == null)
				throw new InvalidOperationException("Cannot rebase relative path when there is no HttpContext.Current");

			return VirtualPathUtility.ToAbsolute(resource, context.Request.ApplicationPath);
		}
	}
}
