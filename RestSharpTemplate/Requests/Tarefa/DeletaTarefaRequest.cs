using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Tarefa
{
    public class DeletaTarefaRequest : RequestBase
    {
        public DeletaTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}";
            method = Method.DELETE;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }
              
        public void SetParameters(string idTarefa)
        {
            parameters.Add("issue_id", idTarefa);
        }
    }
}
