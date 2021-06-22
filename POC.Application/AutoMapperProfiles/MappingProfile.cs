using AutoMapper;
using POC.Application.Features.Garment.GetGarmentTypeList;
using POC.Application.Features.SMV.Command.GetSMVBreakDownVersion;
using POC.Application.Features.Styles.GetStyleList;
using POC.Application.Features.Users.Command.CreateUser;
using POC.Application.Features.Users.Command.UpdateUser;
using POC.Application.Features.Users.Queries.GetUserDetail;
using POC.Application.Features.Users.Queries.GetUserList;
using POC.Domain.Entitities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace POC.Application.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<POC.Domain.Entitities.User, UserViewModel>().ReverseMap();
            CreateMap<POC.Domain.Entitities.User, UserDetailViewModel>();
            CreateMap<POC.Domain.Entitities.User, CreateUserCommandResponse>();
            CreateMap<UpdateUserCommand, POC.Domain.Entitities.User>();


            // -------------------------- data table mapping ------------------------------------------------------

            CreateMap<IDataRecord, GarmentTypeListViewModel>()
                  .ForMember(des => des.GarmentTypeId, src => src.MapFrom(s => s["nIEGmtTypeID"]))
                  .ForMember(des => des.GarmentType, src => src.MapFrom(s => s["cGmtType"]));

            CreateMap<IDataRecord, StyleListViewModel>()
                  .ForMember(des => des.StyleNumber, src => src.MapFrom(s => s["Style"]));

            CreateMap<IDataRecord, SMVBreakDownVersionViewModel>()
                .ForMember(des => des.BreakDownNumber, src => src.MapFrom(s => s["nBdNumber"]))
                .ForMember(des => des.XMLSketch, src => src.MapFrom(s => s["xmSketchPath"]))
                .ForMember
                    (
                        des => des.ConfirmedOn,
                        src => src.MapFrom(s => !string.IsNullOrEmpty(s["dConfirmedOn"].ToString()) ? s["dConfirmedOn"].ToString() : null
                    ))
                .ForMember(des => des.IsNewOB, src => src.MapFrom(s => s["bNewOB"]))
                .ForPath(des => des.ConfirmedBy.UserName, src => src.MapFrom(s => s["cConfirmedBy"]))
                .ForPath(des => des.Account.AccountNo, src => src.MapFrom(s => s["cBdAccount"]))
                .ForPath(des => des.Style.StyleCode, src => src.MapFrom(s => s["nBdStyleCode"]))
                .ForPath(des => des.Style.StyleNumber, src => src.MapFrom(s => s["cBdStyle"]))
                    .ForPath(des => des.Style.GarmentType.GarmentTypeID, src => src.MapFrom(s => s["nIEGmtTypeID"]))
                    .ForPath(des => des.Style.GarmentType.MainGarmentType, src => src.MapFrom(s => s["cGmtType"]))
                .ForPath(des => des.GarmentTypes.GarmentTypeID, src => src.MapFrom(s => s["nIEGmtTypeID"]))
                .ForPath(des => des.GarmentTypes.MainGarmentType, src => src.MapFrom(s => s["cGmtType"]))
                .ForPath(des => des.FabContent.ContentID, src => src.MapFrom(s => s["nFiberContentID"]))
                .ForPath(des => des.FabContent.Content, src => src.MapFrom(s => s["cFabricContent"]))
                .ForMember(des => des.Remarks, src => src.MapFrom(s => s["versionRemark"]))
                .ForMember(des => des.SketchPath, src => src.MapFrom(s => s["cSketchPath"]))
                .ForMember(des => des.VersionHDID, src => src.MapFrom(s => s["nBdVersionHdID"]))
                .ForMember(des => des.Description, src => src.MapFrom(s => s["cBdDesc"]))
                .ForPath(des => des.SampleType.SampleTypeID, src => src.MapFrom(s => s["nSamTypeID"]));



            CreateMap<IDataRecord, SMVBreakDownDetails>()
                 .ForMember(des => des.IsSelected, src => src.MapFrom(s => false))
                 .ForMember(des => des.Remark, src => src.MapFrom(s => s["cRemark"]))
                 .ForMember(des => des.Remark, src => src.MapFrom(s => s["cOperationDesc"]))
                 .ForMember(des => des.OperationSequnce, src => src.MapFrom(s => s["nOPerationSeq"]))
                 .ForMember(des => des.IsNewlyAdded, src => src.MapFrom(s => s["newlyAdded"]))
                 .ForPath(des => des.SMVBreakDownDTGroupHD.HeaderID, src => src.MapFrom(s => s["nHeaderID"] ?? default(int)))
                 .ForPath(des => des.SMVBreakDownDTGroupHD.HeaderDes, src => src.MapFrom(s => s["cHeaderDes"]))
                 .ForPath(des => des.SMVBreakDownDTGroupHD.GarmentPartID, src => src.MapFrom(s => s["nGarmentID"]))
                 .ForPath(des => des.SMVBreakDownDTGroupHD.GarmentPartName, src => src.MapFrom(s => s["muText"]))
                 .ForPath(des => des.SMVBreakDownDTGroupHD.OrderID, src => src.MapFrom(s => s["nHOrder"]))
                 .ForPath(des => des.McCode.MCCodeID, src => src.MapFrom(s => s["nMcCodeID"] == null ? default(int) : s["nMcCodeID"]))
                 .ForPath(des => des.McCode.MCCode, src => src.MapFrom(s => s["McCode"] == null ? string.Empty : s["McCode"].ToString().Trim()))
                 .ForPath(des => des.McCode.MCDescription, src => src.MapFrom(s => s["McDesc"]))
                 .ForPath(des => des.OperationMaster.GSDReference, src => src.MapFrom(s => s["cGSD_Ref"]))
                 .ForPath(des => des.OperationMaster.GSDSMV, src => src.MapFrom(s => double.Parse(s["nGSD_SMV"].ToString())))
                 .ForPath(des => des.OperationMaster.NewGSDSMV, src => src.MapFrom(s => double.Parse(s["nGSD_SMV_New"].ToString())))
                 .ForPath(des => des.OperationMaster.NewGSDSMV, src => src.MapFrom(s => Int32.Parse(s["nTGT"].ToString())))
                 .ForPath(des => des.OperationMaster.SMVType, src => src.MapFrom(s => s["csmvType"].ToString().Trim()))
                 .ForPath(des => des.OperationMaster.OperationID, src => src.MapFrom(s => s["nOperationID"]))
                    .ForPath(des => des.OperationMaster.DeptCode.DepartmentCode, src => src.MapFrom(s => s["cDep_Code"].ToString().Trim()))
                    .ForPath(des => des.OperationMaster.DeptCode.DeptID, src => src.MapFrom(s => s["nDepartmentID"]))
                 .ForMember(des => des.NewMcCode, src => src.MapFrom(s => new IEData()))
                 .ForMember(des => des.NewDepCode, src => src.MapFrom(s => new SewingDepartment()))
                 .ForMember(des => des.Indicator, src => src.MapFrom(s => string.Empty))
                .ForMember(des => des.NewDeptId, src => src.MapFrom(s => s["nNewDepartmentID"]))
                 .ForMember(des => des.NewDeptCode, src => src.MapFrom(s => s["cNewDep_Code"]))
                 .ForMember(des => des.NewDeptDesc, src => src.MapFrom(s => s["cNewDep_Desc"]))
                 .ForMember(des => des.MCCodeID, src => src.MapFrom(s => s["nMcCodeID"] == null ? default(int) : s["nMcCodeID"]))
                 .ForMember(des => des.OperationID, src => src.MapFrom(s => s["nOperationID"]))
                 .ForPath(des => des.StringMcCode, src => src.MapFrom(s => s["McCode"]));



            CreateMap<IDataRecord, ReviewData>()
                 .ForMember(des => des.OperationID, src => src.MapFrom(s => s["nOperationID"]))
                 .ForMember(des => des.ReviewOperation, src => src.MapFrom(s => s["cReviewOperation"]))
                 .ForMember(des => des.MCCode, src => src.MapFrom(s => s["McCode"]))
                 .ForMember(des => des.DepartmentCode, src => src.MapFrom(s => s["cDep_Code"]))
                 .ForMember(des => des.DeptDescription, src => src.MapFrom(s => s["cDep_Desc"]))
                 .ForMember(des => des.DeptID, src => src.MapFrom(s => s["nDepartmentID"]));


            









        }
    }
}
