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

namespace POC.Application.Features.Styles.GetStyleList
{
    public class GetStyleListQueryHandler : IRequestHandler<GetStyleListQuery, SuccessResponse<IEnumerable<StyleListViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly string _starIEConnString;

        public GetStyleListQueryHandler(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _starIEConnString = configuration.GetConnectionString("StarIE-ADOConnection");
        }

        public async Task<SuccessResponse<IEnumerable<StyleListViewModel>>> Handle(GetStyleListQuery request, CancellationToken cancellationToken)
        {
            using (var conn = new SqlConnection(_starIEConnString))
            {
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[Sp_IE_Get_BDDoneStyles]";

                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    var list = _mapper.Map<IEnumerable<StyleListViewModel>>(dt.CreateDataReader());

                    var response = new SuccessResponse<IEnumerable<StyleListViewModel>>()
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
