using System.Web.Mvc;

namespace MvcAssetManager.Helpers
{
	/// <summary>
	/// Adds an ability to specify Html.Resources() in MVC Views.
	/// </summary>
	public static class HtmlHelperExtensions
	{
		/// <summary>
		/// Resources convenience helper for the HTML helper.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <returns>Convenience helper for the HTML helper.</returns>
		public static ResourcesHelper Resources(this HtmlHelper htmlHelper)
		{
			object resourcesHelper;
			if (!htmlHelper.ViewData.TryGetValue(ResourcesHelperKey, out resourcesHelper))
			{
				resourcesHelper = new ResourcesHelper(ResourceTransforms.Transforms);
				htmlHelper.ViewData.Add(ResourcesHelperKey, resourcesHelper);
			}
			return (ResourcesHelper)resourcesHelper;
		}

		private static readonly string ResourcesHelperKey = typeof(ResourcesHelper).FullName;
	}
}
