using System;

namespace MvcAssetManager
{
	/// <summary>
	/// Enum for determining what scope of web resources the <see cref="BaseResourceTransform"/> is going to run 
	/// against.
	/// </summary>
	/// <remarks>
	/// Local web resources either start with ~/ or have a relative path.
	/// </remarks>
	/// <example>
	/// Local: ~/foo.js
	/// Local: foo.js
	/// Local: /foo.js
	/// Local: ../foo.js
	/// Remote: http://www.example.com/foo.js
	/// Remote: //www.example.com/foo.js
	/// </example>
	[Flags]
	public enum ResourceScope
	{
		/// <summary>No resources.</summary>
		None = 0,
		/// <summary>Local resources.</summary>
		Local = 1,
		/// <summary>Remote resources.</summary>
		Remote = 2,
		/// <summary>Local and remote resources.</summary>
		All = Local | Remote,
	}
}
