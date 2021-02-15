using System;

namespace Legalpedia.Shared.Dto
{
    public class LicensePayload
    {
        public string Key { get; internal set; }
        public DateTime UnlockDate { get; set; }
        public int LicensedDays { get; set; }
        public int UpdateLicensedDays { get; set; }
        public string SystemId { get; set; }
        public string Package { get; set; }
        
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime ExpDate => UnlockDate.AddDays(LicensedDays);
        public DateTime UpdateExpDate => UnlockDate.AddDays(UpdateLicensedDays);

        public string LifeUpdate { get; set; }

        public int DaysLeft => (ExpDate - DateTime.Now).Days;
        public int UpdateDaysLeft => (UpdateExpDate - DateTime.Now).Days;

        public string MobileSystemId { get; set; }
    }
}
