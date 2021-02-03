using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Legalpedia.Authorization
{
    public class LegalpediaAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var judgements = context.CreatePermission(PermissionNames.PagesJudgements, L("Judgements"));
            judgements.CreateChildPermission(PermissionNames.PagesJudgementsLatest, L("LatestJudgements"));
            judgements.CreateChildPermission(PermissionNames.PagesJudgementsDetails, L("JudgementDetails"));

            var lfn = context.CreatePermission(PermissionNames.PagesLfn, L("LawsOfFederation"));
            lfn.CreateChildPermission(PermissionNames.PagesLfnDetails, L("ViewLfnDetails"));
            
            var stateRules = context.CreatePermission(PermissionNames.PagesRulesOfCourtState, L("StateRulesOfCourt"));
            stateRules.CreateChildPermission(PermissionNames.PagesRulesOfCourtStateDetails, L("StateRulesOfCourtDetail"));
            
            var otherRules = context.CreatePermission(PermissionNames.PagesRulesOfCourtOthers, L("OtherRulesOfCourt"));
            otherRules.CreateChildPermission(PermissionNames.PagesRulesOfCourtOthersDetails, L("OtherRulesOfCourtDetail"));

            var formsAndPrecedents = context.CreatePermission(PermissionNames.FormsAndPrecedents, L("FormsAndPrecedents"));
            formsAndPrecedents.CreateChildPermission(PermissionNames.FormsAndPrecedentsDetails, L("FormsAndPrecedentsDetails"));

            context.CreatePermission(PermissionNames.LawDictionary, L("LawDictionary"));
            
            context.CreatePermission(PermissionNames.LegalMaxims, L("LegalMaxims"));
            
            context.CreatePermission(PermissionNames.ForeignLegalResources, L("ForeignLegalResources"));
            
            context.CreatePermission(PermissionNames.Articles, L("Articles"));

            var admin = context.CreatePermission(PermissionNames.PagesAdmin, L("AdminPages"));
            admin.CreateChildPermission(PermissionNames.PagesAdminJudgements, L("AdminJudgementsPage"));
            admin.CreateChildPermission(PermissionNames.PagesAdminSummaries, L("AdminSummariesPage"));
            admin.CreateChildPermission(PermissionNames.PagesAdminPackages, L("AdminPackagesPage"));
            admin.CreateChildPermission(PermissionNames.PagesAdminCustomers, L("AdminCustomersPage"));
            admin.CreateChildPermission(PermissionNames.PagesAdminLicenses, L("AdminLicensesPage"));

            admin.CreateChildPermission(PermissionNames.PagesAdminUsers, L("Users"));
            admin.CreateChildPermission(PermissionNames.PagesAdminRoles, L("Roles"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LegalpediaConsts.LocalizationSourceName);
        }
    }
}
