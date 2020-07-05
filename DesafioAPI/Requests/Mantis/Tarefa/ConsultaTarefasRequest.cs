using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Tarefas
{
    public class ConsultaTarefasRequest : RequestBase
    {
        public ConsultaTarefasRequest()
        {
            requestService = "/api/rest/issues/{issue_id}";
            method = Method.GET;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa)
        {
            parameters.Clear();
            parameters.Add("issue_id", idTarefa);
        }
    }
}
