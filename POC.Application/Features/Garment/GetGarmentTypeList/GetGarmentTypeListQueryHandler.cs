using AutoMapper;
using MediatR;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Extensions.Configuration;
using POC.Application.Responses;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Application.Features.Garment.GetGarmentTypeList
{
    public class GetGarmentTypeListQueryHandler : IRequestHandler<GetGarmentTypeListQuery, SuccessResponse<IEnumerable<GarmentTypeListViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly string _opsConnString;


        public GetGarmentTypeListQueryHandler(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            //_opsConnString = new System.MyUtil.Strings.Crypto().Decrypt(configuration.GetConnectionString("OPS-ADOConnection"));
            _opsConnString = configuration.GetConnectionString("OPS-ADOConnection");


        }

        public async Task<SuccessResponse<IEnumerable<GarmentTypeListViewModel>>> Handle(GetGarmentTypeListQuery request, CancellationToken cancellationToken)
        {
            using (var conn = new SqlConnection(_opsConnString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "Select nIEGmtTypeID,cGmtType From [OPS].[dbo].[IE_REF_GarmentType]";
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    var list = _mapper.Map<IEnumerable<GarmentTypeListViewModel>>(dt.CreateDataReader());


                    var response = new SuccessResponse<IEnumerable<GarmentTypeListViewModel>>()
                    {
                        TotalRecordCount = list.Count(),
                        Data = list
                    };
                    return response;
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
