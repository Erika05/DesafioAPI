using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.Tests.Mantis.Projeto;
using NUnit.Framework;
using RestSharp;
using System.Threading;

namespace DesafioAPI.Tests.Mantis.Tarefas
{
    [TestFixture]
    public class AdicionaAnexoTarefasTests : TestBase
    {
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        AdicionaAnexoTarefaRequest adicionaAnexoTarefaRequest = new AdicionaAnexoTarefaRequest();
        HelpersProjetos helpersProjetos = new HelpersProjetos();

        [Test]
        public void AdicionarAnexoTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar anexo";
            string descricao = "Descricao tarefa anexo";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona anexo Tarefa
            string nomeAnexo = "test.txt";
            string anexo = "VGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4NClRoaXMgaXMgYSBURVNULg0KVGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4=";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaAnexoTarefaRequest.SetParameters(idTarefa);
            adicionaAnexoTarefaRequest.SetJsonBody(nomeAnexo, anexo);
            IRestResponse<dynamic> response = adicionaAnexoTarefaRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }

        [Test]
        public void AnexoTarefaNaoInformado()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa adicionar anexo";
            string descricao = "Descricao tarefa anexo";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona anexo Tarefa
            string nomeAnexo = "test.txt";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "not uploaded";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaAnexoTarefaRequest.SetParameters(idTarefa);
            adicionaAnexoTarefaRequest.SetJsonBody(nomeAnexo, " ");
            IRestResponse<dynamic> response = adicionaAnexoTarefaRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.That(true, descricaoErro, retornoMensagemErro);
            });
        }
    }
}