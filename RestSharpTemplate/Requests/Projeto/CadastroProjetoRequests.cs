﻿using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Projeto
{
    public class CadastroProjetoRequests : RequestBase
    {
        public CadastroProjetoRequests()
        {
            requestService = "/api/rest/projects/";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }
        public void SetJsonBody(string name,
                               string descricao)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projeto/CadastroProjetoJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$name", name);
            jsonBody = jsonBody.Replace("$descricao", descricao);
        }
    }
}
