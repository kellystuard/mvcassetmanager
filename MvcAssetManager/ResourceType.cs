using System;

namespace MvcAssetManager
{
	/// <summary>
	/// Enum for determining what type of web resources the <see cref="IResourceTransform"/> is going to run against.
	/// </summary>
	[Flags]
	public enum ResourceType
	{
		/// <summary>No resources.</summary>
		None = 0,
		/// <summary>Script resources.</summary>
		Scripts = 1,
		/// <summary>Style resources.</summary>
		Styles = 2,
		/// <summary>Script and style resources.</summary>
		All = Scripts | Styles,
	}
}
