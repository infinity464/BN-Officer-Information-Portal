using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IAddressService
    {
        Task<AddressModel> GetAddress(int addressId);
        Task<AddressModel> SaveAddress(int v, AddressModel model);
        List<AddressModel> GetAddresses(int employeeId);
        List<SelectModel> GetAddressTypeSelectModels();
    }
}
