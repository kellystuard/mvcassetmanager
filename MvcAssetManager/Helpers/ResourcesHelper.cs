using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcAssetManager.Helpers
{
	/// <summary>
	/// Manages the registering and output of script and style sheet resources.
	/// </summary>
	/// <remarks>
	/// Returned from <see cref="HtmlHelper"/>, via <see cref="HtmlHelperExtensions.Resources"/>.
	/// </remarks>
	public sealed class ResourcesHelper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResourcesHelper"/> class.
		/// </summary>
		/// <param name="engines">The engines that will be used to transform the added script and style sheet 
		/// resources.</param>
		/// <exception cref="System.ArgumentNullException">engines</exception>
		public ResourcesHelper(ResourceTransformCollection engines)
		{
			if (engines == null) throw new ArgumentNullException("engines");

			_engines = engines;
		}

		/// <summary>
		/// Registers the layout scripts. These scripts are output before the scripts registered by 
		/// <see cref="RegisterScripts"/>.
		/// </summary>
		/// <remarks>
		/// Before being output, the scripts are run through the transform engines.
		/// </remarks>
		/// <param name="scripts">The scripts to register.</param>
		/// <returns>Fluent result (this).</returns>
		/// <exception cref="System.ArgumentNullException">scripts</exception>
		/// <seealso cref="RegisterScripts"/>
		/// <seealso cref="OutputScripts"/>
		public ResourcesHelper RegisterLayoutScripts(params string[] scripts)
		{
			if (scripts == null) throw new ArgumentNullException("scripts");

			_layoutScripts.AddRange(scripts);
			return this;
		}

		/// <summary>
		/// Registers the page scripts. These scripts are output after the scripts registered by 
		/// <seealso cref="RegisterLayoutScripts"/>.
		/// </summary>
		/// <remarks>
		/// Before being output, the scripts are run through the transform engines.
		/// </remarks>
		/// <param name="scripts">The scripts to register.</param>
		/// <returns>Fluent result (this).</returns>
		/// <exception cref="System.ArgumentNullException">scripts</exception>
		/// <seealso cref="RegisterLayoutScripts"/>
		/// <seealso cref="OutputScripts"/>
		public ResourcesHelper RegisterScripts(params string[] scripts)
		{
			if (scripts == null) throw new ArgumentNullException("scripts");

			_scripts.AddRange(scripts);
			return this;
		}

		/// <summary>
		/// Registers the layout styles. These styles are output before the styles registered by 
		/// <see cref="RegisterStyles"/>.
		/// </summary>
		/// <remarks>
		/// Before being output, the styles are run through the transform engines.
		/// </remarks>
		/// <param name="styles">The styles to register.</param>
		/// <returns>Fluent result (this).</returns>
		/// <exception cref="System.ArgumentNullException">styles</exception>
		/// <seealso cref="RegisterStyles"/>
		/// <seealso cref="OutputStyles"/>
		public ResourcesHelper RegisterLayoutStyles(params string[] styles)
		{
			if (styles == null) throw new ArgumentNullException("styles");

			_layoutStyles.AddRange(styles);
			return this;
		}

		/// <summary>
		/// Registers the page styles. These styles are output after the styles registered by 
		/// <seealso cref="RegisterLayoutStyles"/>.
		/// </summary>
		/// <remarks>
		/// Before being output, the styles are run through the transform engines.
		/// </remarks>
		/// <param name="styles">The styles to register.</param>
		/// <returns>Fluent result (this).</returns>
		/// <exception cref="System.ArgumentNullException">styles</exception>
		/// <seealso cref="RegisterLayoutStyles"/>
		/// <seealso cref="OutputStyles"/>
		public ResourcesHelper RegisterStyles(params string[] styles)
		{
			if (styles == null) throw new ArgumentNullException("styles");

			_styles.AddRange(styles);
			return this;
		}

		/// <summary>
		/// Outputs the scripts registered by <see cref="RegisterLayoutScripts"/> followed by the scripts registered 
		/// by <see cref="RegisterScripts"/>.
		/// </summary>
		/// <remarks>
		/// Before being output, the scripts are run through the transform engines.
		/// </remarks>
		/// <returns>HTML representation of all scripts that had previously been registered.</returns>
		/// <exception cref="System.InvalidOperationException">Scripts should only be written once</exception>
		public IHtmlString OutputScripts()
		{
			if (_scriptsWritten) throw new InvalidOperationException("Scripts should only be written once");
			_scriptsWritten = true;

			var output = new StringBuilder();

			var scripts = _engines.ProcessResources(_layoutScripts.Concat(_scripts), ResourceType.Scripts);
			foreach (var script in scripts)
				AppendScriptTag(script, output);

			return new HtmlString(output.ToString());
		}

		/// <summary>
		/// Outputs the styles registered by <see cref="RegisterLayoutStyles"/> followed by the styles registered 
		/// by <see cref="RegisterStyles"/>.
		/// </summary>
		/// <remarks>
		/// Before being output, the styles are run through the transform engines.
		/// </remarks>
		/// <returns>HTML representation of all styles that had previously been registered.</returns>
		/// <exception cref="System.InvalidOperationException">Styles should only be written once</exception>
		public IHtmlString OutputStyles()
		{
			if (_stylesWritten) throw new InvalidOperationException("Styles should only be written once");
			_stylesWritten = true;

			var output = new StringBuilder();

			var styles = _engines.ProcessResources(_layoutStyles.Concat(_styles), ResourceType.Styles);
			foreach (var style in styles)
				AppendStyleTag(style, output);

			return new HtmlString(output.ToString());
		}

		private void AppendScriptTag(string script, StringBuilder builder)
		{
			if (script == null) throw new ArgumentNullException("script");
			if (builder == null) throw new ArgumentNullException("builder");

			var tagBuilder = new TagBuilder("script");
			tagBuilder.MergeAttribute("type", "text/javascript");
			tagBuilder.MergeAttribute("src", script);
			var result = tagBuilder.ToString(TagRenderMode.Normal);

			builder.AppendLine(result);
		}

		private void AppendStyleTag(string style, StringBuilder builder)
		{
			if (style == null) throw new ArgumentNullException("style");
			if (builder == null) throw new ArgumentNullException("builder");

			var tagBuilder = new TagBuilder("link");
			tagBuilder.MergeAttribute("rel", "stylesheet");
			tagBuilder.MergeAttribute("href", style);
			var result = tagBuilder.ToString(TagRenderMode.SelfClosing);

			builder.AppendLine(result);
		}

		private readonly List<string> _layoutScripts = new List<string>();
		private readonly List<string> _layoutStyles = new List<string>();
		private readonly List<string> _scripts = new List<string>();
		private readonly List<string> _styles = new List<string>();
		private readonly ResourceTransformCollection _engines;

		private bool _scriptsWritten;
		private bool _stylesWritten;
	}
}
