using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Application.Features.SMV.Command.GetSMVBreakDownVersion
{
    public class GetSMVBreakDownVersionQueryHandler : IRequestHandler<GetSMVBreakDownVersionQuery, ResponseResult<SMVBreakDownVersionViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly string _starIEConnString;

        public GetSMVBreakDownVersionQueryHandler(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _starIEConnString = configuration.GetConnectionString("StarIE-ADOConnection");
        }

        public async Task<ResponseResult<SMVBreakDownVersionViewModel>> Handle(GetSMVBreakDownVersionQuery request, CancellationToken cancellationToken)
        {
            using (var conn = new SqlConnection(_starIEConnString))
            {
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@In_VersionHDID", SqlDbType.Int).Value = 614635;// request.VersionHDID;
                    cmd.CommandText = "Sp_IE_GetBreakDownByVertionHDID";

                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    DataTable dtHeader = ds.Tables[0];
                    DataTable dtDetails = ds.Tables[1];
                    DataTable dtReviewData = ds.Tables[2];

                    var smvBDHeader = _mapper.Map<IEnumerable<SMVBreakDownVersionViewModel>>(dtHeader.CreateDataReader()).FirstOrDefault();

                    var smvDetails = _mapper.Map<IEnumerable<SMVBreakDownDetails>>(dtDetails.CreateDataReader());

                    var smvReviewData = _mapper.Map<IEnumerable<ReviewData>>(dtReviewData.CreateDataReader());


                    foreach (var svmDetail in smvDetails)
                    {
                        var smvReviewList = smvReviewData.Where(r => r.OperationID == svmDetail.OperationID).ToList();

                        svmDetail.NewMcCode = new IEData
                        {
                            MCCodeID = smvReviewList == null ? svmDetail.MCCodeID : smvReviewList?.FirstOrDefault() == null ? svmDetail.MCCodeID == null ? default(int) : svmDetail.MCCodeID : smvReviewList.FirstOrDefault().OperationID,

                            MCCode = smvReviewList == null ? svmDetail.StringMcCode == null ? "" : svmDetail.StringMcCode.Trim() : smvReviewList?.FirstOrDefault() == null ? svmDetail.StringMcCode == null ? "" : svmDetail.StringMcCode.Trim() : smvReviewList.FirstOrDefault().MCCode,
                        };

                        svmDetail.NewDepCode = new SewingDepartment
                        {

                            DeptCode = smvReviewList == null ? svmDetail.NewDeptCode.Trim() : smvReviewList?.FirstOrDefault() == null ? svmDetail.NewDeptCode : smvReviewList.FirstOrDefault().DepartmentCode.Trim(),

                            DeptDescription = smvReviewList == null ? svmDetail.NewDeptDesc : smvReviewList?.FirstOrDefault() == null ? svmDetail.NewDeptDesc : smvReviewList.FirstOrDefault().DeptDescription,

                            DeptID = smvReviewList == null ? svmDetail.NewDeptId : smvReviewList?.FirstOrDefault() == null ? svmDetail.NewDeptId : smvReviewList.FirstOrDefault().DeptID
                        };

                        svmDetail.Indicator = smvReviewList?.FirstOrDefault()?.ReviewOperation;
                    }

                    smvBDHeader.SMVBreakDownDetail = smvDetails;

                    var response = new ResponseResult<SMVBreakDownVersionViewModel>(smvBDHeader);

                    return await Task.FromResult(response);

                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
    }
}
