#nullable enable
using System;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Applications;

namespace Microsoft.Maui.ApplicationModel
{
	partial class BrowserImplementation : IBrowser
	{
		public Task<bool> PlatformOpenAsync(Uri uri, BrowserLaunchOptions launchMode)
		{
			if (uri == null)
				throw new ArgumentNullException(nameof(uri));

			Permissions.EnsureDeclared<Permissions.LaunchApp>();

			var appControl = new AppControl
			{
				Operation = AppControlOperations.View,
				Uri = uri.AbsoluteUri
			};

			var hasMatches = AppControl.GetMatchedApplicationIds(appControl).Any();

			if (hasMatches)
				AppControl.SendLaunchRequest(appControl);

			return Task.FromResult(hasMatches);
		}
	}
}
