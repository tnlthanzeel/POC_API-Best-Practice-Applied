using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POC.Application.Features.SMV.Command.GetSMVBreakDownVersion
{
    public class SMVBreakDownVersionViewModel
    {
        public int BreakDownNumber { get; set; }
        public string XMLSketch { get; set; }
        public DateTime? ConfirmedOn { get; set; }
        public bool IsNewOB { get; set; }
        public User ConfirmedBy { get; set; }
        //public MasterJobDetail JobDetails { get; set; }
        public Style Style { get; set; }
        public Accounts Account { get; set; }
        public GarmentTypes GarmentTypes { get; set; }
        public FabricContent FabContent { get; set; }
        public string Remarks { get; set; }
        public string SketchPath { get; set; }
        public int VersionHDID { get; set; }
        public string Description { get; set; }

        public SampleType SampleType { get; set; }

        public IEnumerable<SMVBreakDownDetails> SMVBreakDownDetail { get; set; }
    }

    public class SMVBreakDownDetails 
    {
        public bool IsSelected { get; set; }
        public SMVBreakDownDTGroupHD SMVBreakDownDTGroupHD { get; set; }
        public IEData McCode { get; set; }
        public IEData NewMcCode { get; set; }
        public OperationMaster OperationMaster { get; set; }
        public SewingDepartment NewDepCode { get; set; }
        public string Remark { get; set; }
        public string OPerationDescription { get; set; }
        public int OperationSequnce { get; set; }

        public string Indicator { get; set; }
        public bool IsNewlyAdded { get; set; } = false;
        public int NewDeptId { get; set; }
        public string NewDeptCode { get; set; }
        public string NewDeptDesc { get; set; }
        public int? MCCodeID { get; set; }
        public int OperationID { get; set; }
        public string StringMcCode { get; set; }
    }

    public class SMVBreakDownDTGroupHD
    {
        public int HeaderID { get; set; }
        public string HeaderDes { get; set; }
        public int GarmentPartID { get; set; }
        public string GarmentPartName { get; set; }
        public int OrderID { get; set; }
    }

    public class OperationMaster 
    {
        public int OperationID { get; set; }
        public string SMVType { get; set; }
        public string GSDReference { get; set; }
        public double GSDSMV { get; set; }
        public double NewGSDSMV { get; set; }

        public int GSDTarget { get; set; }

        public SMVDepartment DeptCode { get; set; }
    }


    public class IEData
    {
        
        public string MCCode { get; set; }
        public string MCDescription { get; set; }
        public int? MCCodeID { get; set; }
    }

    public class SMVDepartment
    {
        public int? DeptID { get; set; }

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool Breakdown { get; set; }

    }

    public class User
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string SessionID { get; set; }

        public int Department { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public List<UserRoleInfo> Roles { get; set; }

        public string FactoryCode { get; set; }

        public override string ToString()
        {
            return FullName;
        }
        public bool IsSuperUser { get { return Roles != null ? Roles.Where(r => r.RoleName == "Supper User").ToList().Count > 0 : false; } }

        public string HighestRoleName { get { return Roles != null ? Roles.OrderBy(o => o.Rank).FirstOrDefault().RoleName : ""; } }

        public string Email { get; set; }
    }

    public class UserRoleInfo
    {
        public int RoleID
        {
            get; set;
        }
        public string RoleName
        {
            get; set;
        }
        public int RoleGroupID
        {
            get; set;
        }
        public string RoleGroupName
        {
            get; set;
        }
        public string Category
        {
            get; set;
        }
        public int Rank
        {
            get; set;
        }
        public string RankName
        {
            get; set;
        }
    }

    public class JobTypes
    {
        public int JobTypeID { get; set; }
        public string JobType { get; set; }
        public string Nature { get; set; }
        public string SelectedField { get; set; }



        public override string ToString()
        {
            return JobType;
        }
    }

    public class SampleType
    {
        public int SampleTypeID { get; set; }
        public string SampleTypeName { get; set; }

        public override string ToString()
        {
            return SampleTypeName;
        }
    }

    public class CurrentStatus
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }

        public override string ToString()
        {
            return StatusName;
        }
    }

    public class TrimRequestStage
    {
        public int? TrimStageID { get; set; }
        public string RequestType { get; set; }

        public override string ToString()
        {
            return TrimStageID.ToString();
        }
    }

    public class TrimTypes
    {
        public int TrimID { get; set; }
        public string TrimDescription { get; set; }

        public override string ToString()
        {
            return TrimDescription;
        }
    }

    public class MerchantDetails
    {
        //public string FullName{ get; set; }
        //public string UserName { get; set; }
        public User ShareID { get; set; }
        //public double CompleteTask { get; set; }
        //public double PendingTask { get; set; }
        public bool SelectUsers { get; set; }
        public User UserID { get; set; }
        public MasterJobDetail DetailID { get; set; }



        public string FullName
        {
            get
            {
                return (ShareID.FirstName + " " + ShareID.LastName);
            }
        }
    }

    public class MasterJobDetail
    {

        public int JobHeaderID { get; set; }
        public int JobDetailID { get; set; }
        public JobTypes JobType { get; set; }
        public SampleType SampleType { get; set; }
        public User Requester { get; set; }
        public User Owner { get; set; }
        public DateTime? DateRequired { get; set; }
        public DateTime DateRequested { get; set; }
        public bool SampleRequired { get; set; }
        public DateTime? SampleSubmission { get; set; }
        public int OwnerID { get; set; }
        public DateTime? DateComplete { get; set; }
        public int BDVersionHdID { get; set; }
        public CurrentStatus Status { get; set; }
        public string Fty { get; set; }
        public string SampleRoomID { get; set; }
        public bool Priority { get; set; }
        public string DocLink { get; set; }
        public string Remark { get; set; }
        public TrimRequestStage TrimRequestStage { get; set; }
        public TrimTypes TrimTypes { get; set; }
        public string cTrimTypes { get; set; }
        public int JobDetialIDLink { get; set; }
        public string DelayReason { get; set; }
        public bool CompleteWithoutPerform { get; set; }
        public string SampleRoom { get; set; }
        public int PendingSampleTask { get; set; }

        public string JobNature
        {
            get
            {
                if (JobType != null)
                {
                    if (JobType.Nature.Equals("T"))
                    {
                        return "Tasks";
                    }
                    else
                    {
                        return "Notifications";
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public int cellformat
        {
            get
            {
                if (Priority == true && JobType.Nature == "T")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int merchJobTypes
        {
            get
            {
                if (JobType != null)
                {
                    if (JobType.JobTypeID == 1 || JobType.JobTypeID == 2 || JobType.JobTypeID == 3)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool IsSampleSubmited
        {
            get
            {
                if (JobType != null)
                {
                    if (JobType.JobTypeID == 5 && Status.StatusID == 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
        }

        public MasterJobHeader MasterJobHD { get; set; }

        public MerchantDetails MerchantDetails { get; set; }
        public int IsTrimRequested { get; set; }

    }

    public class MasterJobHeader
    {
        public int JobHeaderID { get; set; }

        public Style Style { get; set; }

        public string SimilarStyle { get; set; }

        public IEnumerable<MasterJobDetail> MasterJobDT { get; set; }
    }

    public class Style : ICloneable
    {
        public int? StyleCode { get; set; }
        public string StyleNumber { get; set; }
        public string KomarStyleNumber { get; set; }
        public string Season { get; set; }
        public GarmentTypes GarmentType { get; set; }
        //public Accounts Account { get; set; }
        //public FabricContent FabricContetnt { get; set; }
        //public PackingMethods PackingMethods { get; set; }
        //public CuttingTypes CuttingTypes { get; set; }
        //public SpecialOperation SpecialOperation { get; set; }
        public DateTime? TargetCut { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class SpecialOperation
    {
        public bool IsWash { get; set; }
        public string SO { get; set; }
    }

    public class CuttingTypes
    {
        public byte CuttingTypeID { get; set; }
        public string CuttingType { get; set; }
        public string OPSCode { get; set; }
    }

    public class PackingMethods
    {
        public int PackingMethodID { get; set; }
        public string PackingMethod { get; set; }
    }

    public class GarmentTypes
    {
        public int? GarmentTypeID { get; set; }
        public string GarmentType { get; set; }
        public int GarmentCatID { get; set; }
        public string GarmentCategory { get; set; }
        public string MainGarmentType { get; set; }
        public int? IEGmtTypeID { get; set; }


        public override string ToString()
        {
            return GarmentType;
        }
    }

    public class Accounts
    {
        public int AccountID { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        //public Contracts Contract { get; set; }




        public override string ToString()
        {
            return AccountNo;
        }
    }

    public class Contracts
    {
        public int ContractID { get; set; }
        public string Contract { get; set; }
        public Accounts Account { get; set; }
        public override string ToString()
        {
            return Contract;
        }
    }

    public class FabricContent
    {
        public int ContentID { get; set; }
        public string Content { get; set; }
        public string HSCode { get; set; }

        //public User EnterUser { get; set; }
    }

    public class SewingDepartment
    {
        public int DeptID { get; set; }
        public string DeptCode { get; set; }
        public string DeptDescription { get; set; }
        public bool BreakDown { get; set; }

        public string SubDepartmentName { get; set; }
    }

    public class ReviewData
    {
        public int? OperationID { get; set; }
        public string MCCode { get; set; }
        public string DepartmentCode { get; set; }
        public string DeptDescription { get; set; }
        public int DeptID { get; set; }
        public string ReviewOperation { get; set; }

    }
}
