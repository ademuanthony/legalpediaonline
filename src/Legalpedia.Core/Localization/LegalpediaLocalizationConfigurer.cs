using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Legalpedia.Localization
{
    public static class LegalpediaLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(LegalpediaConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(LegalpediaLocalizationConfigurer).GetAssembly(),
                        "Legalpedia.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
