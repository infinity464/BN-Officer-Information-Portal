using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class AddressService : IAddressService
    {
        private readonly IBnoisRepository<Address> addressRepository;
        public AddressService(IBnoisRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<AddressModel> GetAddress(int addressId)
        {
            if (addressId <= 0)
            {
                return new AddressModel();
            }
            Address address = await addressRepository.FindOneAsync(x => x.AddressId == addressId);

            if (address == null)
            {
                throw new InfinityNotFoundException("Address not found!");
            }
            AddressModel model = ObjectConverter<Address, AddressModel>.Convert(address);
            return model;
        }

        public List<AddressModel> GetAddresses(int employeeId)
        {
            List<Address> addresses = addressRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "Employee", "Division", "District", "Upazila").ToList();
            List<AddressModel> models = ObjectConverter<Address, AddressModel>.ConvertList(addresses.ToList()).ToList();
            models = models.Select(x =>
            {
                x.AddressTypeName = Enum.GetName(typeof(AddressType), x.AddressType);
                return x;
            }).ToList();
            return models;
        }

        public List<SelectModel> GetAddressTypeSelectModels()
        {
            List<SelectModel> selectModels =
              Enum.GetValues(typeof(AddressType)).Cast<AddressType>()
                   .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                   .ToList();
            return selectModels;
        }

        public async Task<AddressModel> SaveAddress(int addressId, AddressModel model)
        {
            bool isExist = await addressRepository.ExistsAsync(x => x.AddressType == model.AddressType && x.EmployeeId == model.EmployeeId && x.AddressId != model.AddressId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Address already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Education data missing");
            }
            Address address = ObjectConverter<AddressModel, Address>.Convert(model);

            if (addressId > 0)
            {
                address = await addressRepository.FindOneAsync(x => x.AddressId == addressId);
                if (address == null)
                {
                    throw new InfinityNotFoundException("Education not found!");
                }
                address.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                address.ModifiedDate = DateTime.Now;
            }
            else
            {
                address.IsActive = true;
                address.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                address.CreatedDate = DateTime.Now;
            }
            address.EmployeeId = model.EmployeeId;
            address.AddressType = model.AddressType;
            address.CareOf = model.CareOf;
            if (model.DivisionId == 0)
            {
                address.DivisionId = null;
            }
            else
            {
                address.DivisionId = model.DivisionId;
            }

            if (model.DistrictId == 0)
            {
                address.DistrictId = null;
            }
            else
            {
                address.DistrictId = model.DistrictId;
            }
            if (model.UpazilaId == 0)
            {
                address.UpazilaId = null;
            }
            else
            {
                address.UpazilaId = model.UpazilaId;
            }
            address.AddressDetailBangla = model.AddressDetailBangla;
            address.AddressDetailEnglish = model.AddressDetailEnglish;
            address.EmailAddress = model.EmailAddress;
            address.Phone = model.Phone;
            address.PostOfficeName = model.PostOfficeName;
            address.PostCode = model.PostCode;
            await addressRepository.SaveAsync(address);
            model.AddressId = address.AddressId;
            return model;
        }
    }
}
