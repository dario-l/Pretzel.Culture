using NDesk.Options;
using Pretzel.Logic.Extensions;
using System.Globalization;
using System.Threading;
using Pretzel.Logic.Extensibility;
using Pretzel.Logic.Templating.Context;

namespace Pretzel.Culture
{
    public class BakeCultureSupport : IBeforeProcessingTransform, IHaveCommandLineArgs
    {
        private string _culture;

        public void UpdateOptions(OptionSet options)
        {
            options.Add("culture=", "Enables set thread culture", v => _culture = v);
        }

        public string[] GetArguments(string command)
        {
            if (command == "bake" || command == "taste")
                return new[] { "-culture" };

            return new string[0];
        }

        public void Transform(SiteContext context)
        {
            Tracing.Debug("Setting thread culture to " + _culture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(_culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_culture);
        }
    }
}
