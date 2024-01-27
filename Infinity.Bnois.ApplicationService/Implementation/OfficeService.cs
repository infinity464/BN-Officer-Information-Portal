using Infinity.Bnois.ApplicationService.Implementation;
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
using Office = Infinity.Bnois.Data.Office;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class OfficeService : IOfficeService
    {
        private readonly IBnoisRepository<Office> officeRepository;
        private readonly IOfficeShipmentService officeShipmentService;


        public OfficeService(IBnoisRepository<Office> officeRepository, IOfficeShipmentService officeShipmentService)
        {
            this.officeRepository = officeRepository;
            this.officeShipmentService = officeShipmentService;
        }


        public async Task<OfficeModel> GetOffice(int id)
        {
            if (id <= 0)
            {
                return new OfficeModel();
            }
            Office office = await officeRepository.FindOneAsync(x => x.OfficeId == id);
            if (office == null)
            {
                throw new InfinityNotFoundException("Office not found");
            }
            OfficeModel model = ObjectConverter<Office, OfficeModel>.Convert(office);
            return model;
        }


        //public async Task<OfficeModel> SaveOffice(int id, OfficeModel model)
        //{
        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Office data missing");
        //    }
        //    Office office = ObjectConverter<OfficeModel, Office>.Convert(model);
        //    if (id > 0)
        //    {
        //        office = await officeRepository.FindOneAsync(x => x.OfficeId == id);
        //        if (office == null)
        //        {
        //            throw new InfinityNotFoundException("Office not found !");
        //        }

        //        office.ModifiedDate = DateTime.Now;
        //        office.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        office.CreatedDate = DateTime.Now;
        //        office.CreatedBy = userId;
        //        office.IsActive = true;
        //    }
        //    office.Name = model.Name;
        //    office.NameBangla = model.NameBangla;
        //    office.ShortName = model.ShortName;
        //    office.ShortNameBangla = model.ShortNameBangla;
        //    office.PatternId = model.PatternId;
        //    office.ShipCategoryId = model.ShipCategoryId;
        //    office.ShipType = model.ShipType;
        //    office.CountryId = model.CountryId;
        //    office.ShipOriginId = model.ShipOriginId;
        //    office.AddressInfo = model.AddressInfo;
        //    office.BornOffice = model.BornOffice;
        //    office.BornOfficeId = model.BornOfficeId;
        //    office.Establishment = model.Establishment;
        //    office.GovtApproved = model.GovtApproved;
        //    office.CoastGuard = model.CoastGuard;
        //    office.IsOutsideOrg = model.IsOutsideOrg;
        //    office.ObjType = model.ObjType;
        //    office.IndCmd = model.IndCmd;
        //    office.SeaServiceCnt = model.SeaServiceCnt;
        //    office.IsDocyardCount = model.IsDocyardCount;
        //    office.IsIntelligenceServiceCount = model.IsIntelligenceServiceCount;
        //    office.IsDeputationCount = model.IsDeputationCount;
        //    office.IsIsoCount = model.IsIsoCount;
        //    office.CmdService = model.CmdService;
        //    office.Commissioned = model.Commissioned;
        //    office.CommDate = model.CommDate;
        //    office.CommissionBy = model.CommissionBy;
        //    office.DeCommissioned = model.DeCommissioned;
        //    office.DeComDate = model.DeComDate;
        //    office.DeComPurpose = model.DeComPurpose;
        //    office.ParentId = model.ParentId;
        //    office.AdminAuthorityId = model.AdminAuthorityId;
        //    office.ZoneId = model.ZoneId;
        //    office.SeServStartDate = model.SeServStartDate;
        //    office.SeServEndDate = model.SeServEndDate;
        //    office.CommandServEndDate = model.CommandServEndDate;
        //    office.CommandServStartDate = model.CommandServStartDate;
        //    office.IsMoved = false;
        //    office.SecurityName = model.SecurityName;

        //    await officeRepository.SaveAsync(office);
        //    model.OfficeId = office.OfficeId;
        //    return model;
        //}

        //public async Task<List<SelectModel>> GetAdminAuthoritySelectModel()
        //{

        //    ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.Pattern.Name == "Admin Area");
        //    List<Office> query = offices.OrderBy(x => x.ShortName).ToList();

        //    List<SelectModel> selectModels = query.Select(x => new SelectModel
        //    {
        //        Text = x.ShortName,
        //        Value = x.OfficeId
        //    }).ToList();
        //    return selectModels;
        //}
        public async Task<List<SelectModel>> GetOfficeSelectModelByShip(int ship)
        {

            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.ShipType==ship);
            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();

            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }


     




        public async Task<List<SelectModel>> GetBornOfficeSelectModel()
        {

            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.BornOffice);
            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetBornOfficeFullNameSelectModel()
        {

            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.BornOffice);
            List<Office> query = offices.OrderBy(x => x.Name).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }


        public async Task<List<SelectModel>> GetParentOfficeSelectModel()
        {

            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive);
            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }


      

        public async Task<List<SelectModel>> GetMinistryOfficeSelectModel()
        {

            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.PatternId == 23);
            List<Office> query = offices.OrderBy(x => x.Name).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetChildOfficeSelectModel(int parentId)
        {
            ICollection<Office> offices;
            if (parentId == -404)
            {
                offices = await officeRepository.FilterAsync(x => x.IsActive && x.CoastGuard == true);
            }
            else
            {
               offices = await officeRepository.FilterAsync(x => x.IsActive && x.ParentId == parentId);
            }
            
            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }



        public async Task<List<SelectModel>> GetOfficeSelectModel(int type)
        {
            ICollection<Office> offices;

            if (type == (int)TransferType.CostGuard)
            {
                offices = await officeRepository.FilterAsync(x => x.IsActive && x.CoastGuard);
            }
            else if (type == (int)TransferType.Outside)
            {
                offices = await officeRepository.FilterAsync(x => x.IsActive && x.IsOutsideOrg);
            }
            else
            {
                offices = await officeRepository.FilterAsync(x => x.IsActive && x.CoastGuard == false && x.IsOutsideOrg == false);
            }

            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetOrganizationSelectModel()
        {

            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.IsOutsideOrg);
            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }

        public List<SelectModel> GetObjectiveSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(Objective)).Cast<Objective>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }


        public List<SelectModel> GetShipTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(ShipType)).Cast<ShipType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }


        public async Task<bool> DeleteOffice(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Office office = await officeRepository.FindOneAsync(x => x.OfficeId == id);
            if (office == null)
            {
                throw new InfinityNotFoundException("Office not found");
            }

            ICollection<Office> officeList = await officeRepository.FilterAsync(x => x.ParentId == id);

            if (officeList.Count > 0)
            {
                throw new InfinityNotFoundException("Remove Child Office first");
            }
            return await officeRepository.DeleteAsync(office);


        }


        public async Task<List<SelectModel>> GetOfficeWithoutShipSelectModels()
        {
            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive  && x.IsOutsideOrg==false);
            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }

        //public async Task<List<SelectModel>> GetShipSelectModels()
        //{
        //    ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.Pattern.IsMoveAble == true);
        //    List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
        //    List<SelectModel> selectModels = query.Select(x => new SelectModel
        //    {
        //        Text = x.ShortName,
        //        Value = x.OfficeId
        //    }).ToList();
        //    return selectModels;
        //}

        public async Task<List<SelectModel>> GetShipOfficeSelectModels()
        {
            ICollection<Office> offices = await officeRepository.FilterAsync(x => x.IsActive && x.PatternId==7);
            List<Office> query = offices.OrderBy(x => x.ShortName).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ShortName,
                Value = x.OfficeId
            }).ToList();
            return selectModels;
        }



        public List<OfficeModel> GetOfficeStructures()
        {

            List<Office> list;

            list = officeRepository.Where(a => a.IsActive).OrderBy(a => a.ShortName).ToList();


            List<Office> treeList = new List<Office>();
            if (list.Count > 0)
            {
                treeList = BuildStructure(list);
            }

            List<OfficeModel> models = ObjectConverter<Office, OfficeModel>.ConvertList(treeList.ToList()).ToList();
            return models;
        }


        public List<Office> BuildStructure(List<Office> list)
        {
            List<Office> returnList = new List<Office>();
            var topLevels = list.Where(a => a.ParentId == list.OrderBy(b => b.ParentId).FirstOrDefault().ParentId);
            returnList.AddRange(topLevels);
            foreach (var i in topLevels)
            {
                GetStructureView(list, i, ref returnList);
            }
            return returnList;
        }

        public void GetStructureView(List<Office> list, Office current, ref List<Office> returnList)
        {
            var childs = list.Where(a => a.ParentId == current.OfficeId).ToList();
            current.Count ="Ship/Establishment: "+ childs.Count;
            current.Children = new List<Office>();
            current.Children.AddRange(childs);
            foreach (var i in childs)
            {
                GetStructureView(list, i, ref returnList);
            }
        }

        public List<OfficeModel> GetOffices()
        {
            List<Office> list;

            list = officeRepository.Where(a => a.IsActive).OrderBy(a => a.ShortName).ToList();


            List<Office> treeList = new List<Office>();
            if (list.Count > 0)
            {
                treeList = BuildTree(list);
            }

            List<OfficeModel> models = ObjectConverter<Office, OfficeModel>.ConvertList(treeList.ToList()).ToList();
            return models;
        }

        public List<Office> BuildTree(List<Office> list)
        {
            List<Office> returnList = new List<Office>();
            var topLevels = list.Where(a => a.ParentId == list.OrderBy(b => b.ParentId).FirstOrDefault().ParentId);
            returnList.AddRange(topLevels);
            foreach (var i in topLevels)
            {
                GetTreeView(list, i, ref returnList);
            }
            return returnList;
        }

        public void GetTreeView(List<Office> list, Office current, ref List<Office> returnList)
        {
            var childs = list.Where(a => a.ParentId == current.OfficeId).ToList();
            current.Items = new List<Office>();
            current.Items.AddRange(childs);
            foreach (var i in childs)
            {
                GetTreeView(list, i, ref returnList);
            }
        }

     


        public async Task<OfficeModel> ShipMovement(int officeId, OfficeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Office data missing");
            }

            if (model.ShipDate==null)
            {
                throw new InfinityArgumentMissingException("Date is required.");
            }

            Office office = ObjectConverter<OfficeModel, Office>.Convert(model);

            office = await officeRepository.FindOneAsync(x => x.OfficeId == officeId);
            if (office == null)
            {
                throw new InfinityNotFoundException("Office not found !");
            }


            OfficeShipmentModel officeShipment = new OfficeShipmentModel();
            officeShipment.OfficeId = officeId;
            officeShipment.PreviousParentId = office.ParentId;
            officeShipment.ShipDate = model.ShipDate;
            officeShipment.PreZoneId = office.ZoneId;
            officeShipment.PreAdminAuthorityId = office.AdminAuthorityId;
            officeShipment.PreAddress = office.AddressInfo;

           await officeShipmentService.SaveOfficeShipment(officeShipment);

            var officeDetails = await officeRepository.FindOneAsync(x => x.OfficeId == model.ParentId);

            bool isAdminArea =  officeRepository.Exists(x => x.OfficeId == model.ParentId && x.PatternId==1);

            if (isAdminArea)
            {
                office.AdminAuthorityId = model.ParentId;
            }
            else
            {
                office.AdminAuthorityId = officeDetails.AdminAuthorityId;
            }
            office.ParentId = model.ParentId;
            office.ZoneId = officeDetails.ZoneId;

            office.IsMoved = true;




            await officeRepository.SaveAsync(office);
            model.OfficeId = office.OfficeId;
            return model;
        }


        public List<object> GetAppointedOfficer(int officeId)
        {
            System.Data.DataTable dataTable = officeRepository.ExecWithSqlQuery(String.Format("exec [spGetAppointedOfficer] {0} ", officeId));

            return dataTable.ToJson().ToList();
        }


        public List<object> GetVacantAppointment(int officeId)
        {
            System.Data.DataTable dataTable = officeRepository.ExecWithSqlQuery(String.Format("exec [spGetVacantAppointment] {0} ", officeId));

            return dataTable.ToJson().ToList();
        }

        public Task<OfficeModel> SaveOffice(int id, OfficeModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetAdminAuthoritySelectModel()
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetShipSelectModels()
        {
            throw new NotImplementedException();
        }
    }
}
