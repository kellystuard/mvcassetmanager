using System;
using MvcAssetManager;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof($rootnamespace$.App_Start.ResourceConfig), "PostStart")]

namespace $rootnamespace$.App_Start
{
	public static class ResourceConfig
	{
		public static void PostStart()
		{
			Register(ResourceTransforms.Transforms, !System.Web.HttpContext.Current.IsDebuggingEnabled);
		}

		public static void Register(ResourceTransformCollection resourceTransforms, bool releaseMode)
		{
			if (resourceTransforms == null) throw new ArgumentNullException("resourceTransforms");

			// the following transforms change the script/css file names into full references
			// e.g. (for a script reference) --
			// Debug:   jquery -> ~/Content/jquery.js
			// Release: jquery -> ~/Content/jquery.min.js

			resourceTransforms.Add(new LocalRebaseTransform(ResourceType.Styles, "~/Content/{0}"));
			resourceTransforms.Add(new LocalRebaseTransform(ResourceType.Scripts, "~/Scripts/{0}"));

			var stylesExtension = (releaseMode) ? ".min.css" : ".css";
			resourceTransforms.Add(new ExtensionTransform(".css", stylesExtension, ResourceType.Styles, ResourceScope.All));
			resourceTransforms.Add(new ExtensionTransform("", stylesExtension, ResourceType.Styles, ResourceScope.All)
				.AddIgnore(".css")
			);
			var scriptsExtension = (releaseMode) ? ".min.js" : ".js";
			resourceTransforms.Add(new ExtensionTransform(".js", scriptsExtension, ResourceType.Scripts, ResourceScope.All));
			resourceTransforms.Add(new ExtensionTransform("", scriptsExtension, ResourceType.Scripts, ResourceScope.All)
				.AddIgnore(".js")
			);

			// this removes any duplicate script references
			resourceTransforms.Add(new UniqueTransform(ResourceType.All));
		}
	}
}
