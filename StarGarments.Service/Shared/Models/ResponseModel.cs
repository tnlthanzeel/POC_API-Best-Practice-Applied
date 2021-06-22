using System.Collections.Generic;

namespace StarGarments.Service.Shared.Models
{
    public class ReponseModel<TModel> where TModel : class
    {
        public int TotalRecordCount { get; set; }
        public TModel Data { get; set; }

        public bool Success { get; set; }
        public string Message { get; set; }

        public List<string> ValidationErrors { get; set; }
    }
}
