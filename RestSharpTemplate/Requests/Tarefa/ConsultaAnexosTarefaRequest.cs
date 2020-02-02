using DesafioAPI.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Tarefas
{
    public class ConsultaTarefasAnexosRequest : RequestBase
    {
        public ConsultaTarefasAnexosRequest()
        {
            requestService = "/api/rest/issues/{issue_id}/files/{file_id}";
            method = Method.GET;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa, string idAnexo)
        {
            parameters.Add("issue_id", idTarefa);
            parameters.Add("file_id", idAnexo);
        }
    }
}
