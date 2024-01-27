using System;
using System.Collections.Generic;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class OfficeModel
    {
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public string NameBangla { get; set; }
        public string ShortName { get; set; }
        public string ShortNameBangla { get; set; }
        public string SecurityName { get; set; }
        public Nullable<int> PatternId { get; set; }
        public Nullable<int> ShipCategoryId { get; set; }
        public Nullable<int> ShipType { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> ShipOriginId { get; set; }
        public string AddressInfo { get; set; }
        public Nullable<int> BornOfficeId { get; set; }
        public bool BornOffice { get; set; }
        public bool Establishment { get; set; }
        public bool GovtApproved { get; set; }
        public bool CoastGuard { get; set; }
        public bool IsOutsideOrg { get; set; }
        public int ObjType { get; set; }
        public bool IndCmd { get; set; }
        public bool SeaServiceCnt { get; set; }
        public bool IsDocyardCount { get; set; }
        public bool IsIsoCount { get; set; }
        public bool IsDeputationCount { get; set; }
        public bool IsIntelligenceServiceCount { get; set; }
        public bool CmdService { get; set; }
        public bool Commissioned { get; set; }
        public Nullable<System.DateTime> CommDate { get; set; }
        public string CommissionBy { get; set; }
        public bool DeCommissioned { get; set; }
        public Nullable<System.DateTime> DeComDate { get; set; }
        public string DeComPurpose { get; set; }
        public int ParentId { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public Nullable<int> AdminAuthorityId { get; set; }
        public Nullable<System.DateTime> SeServStartDate { get; set; }
        public Nullable<System.DateTime> SeServEndDate { get; set; }
        public Nullable<System.DateTime> CommandServStartDate { get; set; }
        public Nullable<System.DateTime> CommandServEndDate { get; set; }
        public bool IsMoved { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public Nullable<System.DateTime> ShipDate { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual CountryModel Country1 { get; set; }
        public virtual PatternModel Pattern { get; set; }
        public virtual ShipCategoryModel ShipCategory { get; set; }
        public virtual ZoneModel Zone { get; set; }

        public List<OfficeModel> Items { get; set; }

        public List<OfficeModel> Children { get; set; }
        public string Count { get; set; }

    }
}