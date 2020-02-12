using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using System.Collections;
using System.Collections.Generic;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class ConsultaTarefasTests : TestBase
    {
        ConsultaTodasTarefasRequest consultaTodasTarefasRequest = new ConsultaTodasTarefasRequest();
        ConsultaTarefasRequest consultaTarefasRequest = new ConsultaTarefasRequest();
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        ConsultaTarefasAnexosRequest consultaTarefasAnexosRequest = new ConsultaTarefasAnexosRequest();
        ConsultaTarefasProjetoRequest consultaTarefasProjetoRequest = new ConsultaTarefasProjetoRequest();

        [Test]
        public void ConsultarTarefa()
        {           
            #region Parameters
            string resumo = "Tarefa consulta";
            string descricao = "descricao";
            string categoria = "General";
            string projeto = "projeto geral";            
            string statusCodeEsperado = "OK";
            VerificaProjetoExiste(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            #endregion
            consultaTarefasRequest.SetParameters(idTarefa);
            IRestResponse<dynamic> response = consultaTarefasRequest.ExecuteRequest();
            string retornoId = response.Data["issues"][0]["id"];
            string retornoSummary = response.Data["issues"][0]["summary"];
            string retornoDescription = response.Data["issues"][0]["description"];
            string retornoProject = response.Data["issues"][0]["project"]["name"];
            string retornoCategory = response.Data["issues"][0]["category"]["name"];

            Assert.Multiple(() =>
                {
                    Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                    Assert.AreEqual(idTarefa, retornoId);
                    Assert.AreEqual(resumo, retornoSummary);
                    Assert.AreEqual(descricao, retornoDescription);
                    Assert.AreEqual(projeto, retornoProject);
                    Assert.AreEqual(categoria, retornoCategory);
                });
        }

        [Test]
        public void ConsultarTodasTarefas()
        {
            #region Parameters
            string statusCodeEsperado = "OK";
            #endregion            
            consultaTodasTarefasRequest.SetParameters("10", "1");
            IRestResponse<dynamic> response = consultaTodasTarefasRequest.ExecuteRequest();
            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        }

        [Test]
        public void ConsultarAnexosTarefa()
        {
            #region Parameters
            string resumo = "Tarefa consulta";
            string descricao = "descricao";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "OK";
            string nomeAnexo = "test.txt";
            string anexo = "VGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4NClRoaXMgaXMgYSBURVNULg0KVGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4=";
            VerificaProjetoExiste(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto, nomeAnexo, anexo);
            IRestResponse<dynamic> responseCadastro = cadastraTarefaRequest.ExecuteRequest();
            string idTarefa = responseCadastro.Data["issue"]["id"];
            string idAnexo = responseCadastro.Data["issue"]["attachments"][0]["id"];
            #endregion
            consultaTarefasAnexosRequest.SetParameters(idTarefa, idAnexo);
            IRestResponse<dynamic> response = consultaTarefasAnexosRequest.ExecuteRequest();
            string retornoId = response.Data["files"][0]["id"];
            string retornoNomeAnexo = response.Data["files"][0]["filename"];
            string retornoAnexo = response.Data["files"][0]["content"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idAnexo, retornoId);
                Assert.AreEqual(nomeAnexo, retornoNomeAnexo);
                Assert.AreEqual(anexo, retornoAnexo);
            });
        }

        [Test]
        public void ConsultarTarefasProjeto()
        {
            #region Parameters
            string resumo = "Tarefa consulta";
            string descricao = "descricao";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "OK";
            VerificaProjetoExiste(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idProjeto = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["project"]["id"];
            #endregion
            consultaTarefasProjetoRequest.SetParameters(idProjeto);
            IRestResponse<dynamic> response = consultaTarefasProjetoRequest.ExecuteRequest();
            string retornoProjeto = response.Data["issues"][0]["project"]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(projeto, retornoProjeto);
            });
        }

        //Consulta utilizando filtro cadastrado pelo usuário (Get issues matching filter)
        //Consulta tarefas associadas a um usuário (Get issues assigned to me)
        //Consulta tarefas criadas por um usuário (Get issues reported by me)
        //Consulta tarefas monitoradaas por um usuário (Get unassigned issues)

        public void VerificaProjetoExiste(string nomeProjeto)
        {
            if (ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto).Equals(0))
            {
                cadastroProjetoRequests.SetJsonBody(nomeProjeto, "");
                cadastroProjetoRequests.ExecuteRequest();
            }
        }
    }
}
