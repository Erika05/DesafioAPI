using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Tarefas
{
    public class ConsultaTodasTarefasRequest : RequestBase
    {
        public ConsultaTodasTarefasRequest()
        {
            requestService = "/api/rest/issues";
            method = Method.GET;            
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string pageSize, string page)
        {
            parameters.Clear();
            parameters.Add("page_size", pageSize);
            parameters.Add("page", page);
        }
    }
}
