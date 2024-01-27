using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IAgeServicePolicyService
    {

        List<AgeServicePolicyModel> GetAgeServicePolicies(int ps, int pn, string qs, out int total);
        Task<AgeServicePolicyModel> GetAgeServicePolicy(int id);
        Task<AgeServicePolicyModel> SaveAgeServicePolicy(int v, AgeServicePolicyModel model);
        Task<bool> DeleteAgeServicePolicy(int id);
        List<SelectModel> GetEarlyStatusSelectModels();


	    Task<DateTime?> GetLprServiceDate(EmployeeGeneralModel id);
    }
}