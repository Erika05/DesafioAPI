using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using System.Collections;
using System.Collections.Generic;
using DesafioAPI.Tests.Mantis.Projeto;
using DesafioAPI.Helpers;

namespace DesafioAPI.Tests.Mantis.Tarefas
{
    [TestFixture]
    public class ConsultaTarefasTests : TestBase
    {
        ConsultaTodasTarefasRequest consultaTodasTarefasRequest = new ConsultaTodasTarefasRequest();
        ConsultaTarefasRequest consultaTarefasRequest = new ConsultaTarefasRequest();
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        ConsultaTarefasAnexosRequest consultaTarefasAnexosRequest = new ConsultaTarefasAnexosRequest();
        ConsultaTarefasProjetoRequest consultaTarefasProjetoRequest = new ConsultaTarefasProjetoRequest();
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        string nomeVetor = "issues";

        [Test]
        public void ConsultarTarefa()
        {
            #region Parameters
            string resumo = "Tarefa consulta";
            string descricao = "descricao";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "OK";
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
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
            string projeto = "projeto geral";
            string resumo = "Tarefa consulta";
            string categoria = "General";
            string descricaoI = "tarefa todas as consultas I";
            string descricaoII = "tarefa todas as consultas II";
            string valorChave = "description";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);

            cadastraTarefaRequest.SetJsonBody(resumo, descricaoI, categoria, projeto);
            cadastraTarefaRequest.ExecuteRequest();

            cadastraTarefaRequest.SetJsonBody(resumo, descricaoII, categoria, projeto);
            cadastraTarefaRequest.ExecuteRequest();

            consultaTodasTarefasRequest.SetParameters("10", "1");
            IRestResponse response = consultaTodasTarefasRequest.ExecuteRequest();
            List<string> listaTarefas = GeneralHelpers.ObterListaResponse(response, nomeVetor, valorChave);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaTarefas, descricaoI));
                Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaTarefas, descricaoII));
                Assert.IsTrue(listaTarefas.Count >= 2);
            });
        }

        [Test]
        public void ConsultarAnexosTarefa()
        {
            #region Parameters
            string resumo = "Tarefa consulta anexo";
            string descricao = "descricao tarefa consult anexo";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "OK";
            string nomeAnexo = "test.txt";
            string anexo = "VGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4NClRoaXMgaXMgYSBURVNULg0KVGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4=";
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
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
            string resumo = "Tarefa consulta projeto";
            string descricao = "descricao consulta tarefas projeto";
            string descricaoII = "descricao consulta tarefas projetoII";
            string descricaoIII = "descricao consulta tarefas projetoIII";
            string categoria = "General";
            string projeto = "projeto consultar tarefas por projeto";
            string projetoI = "projeto geral";
            string statusCodeEsperado = "OK";
            string nomeVetorFilho = "project";
            string valorChave = "name";
            string idProjeto = helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            helpersProjetos.PreparaBaseCadastradoProjeto(projetoI);

            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            cadastraTarefaRequest.ExecuteRequest();

            cadastraTarefaRequest.SetJsonBody(resumo, descricaoII, categoria, projeto);
            cadastraTarefaRequest.ExecuteRequest();

            cadastraTarefaRequest.SetJsonBody(resumo, descricaoIII, categoria, projetoI);
            cadastraTarefaRequest.ExecuteRequest();           

            #endregion
            consultaTarefasProjetoRequest.SetParameters(idProjeto);
            IRestResponse response = consultaTarefasProjetoRequest.ExecuteRequest();

            List<string> listaTarefasDoProjeto = GeneralHelpers.ObterListaResponse(response, nomeVetor, nomeVetorFilho, valorChave);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaTarefasDoProjeto, projeto));
                Assert.IsFalse(GeneralHelpers.VerificaSeStringEstaContidaNaLista(listaTarefasDoProjeto, projetoI));
            });
        }

        [Test]
        public void TarefaNaoEncontrada()
        {
            #region Parameters
            string statusCodeEsperado = "NotFound";
            string descricaoErro = "not foundd";
            #endregion
            consultaTarefasRequest.SetParameters("0");
            IRestResponse<dynamic> response = consultaTarefasRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.That(true, descricaoErro, retornoMensagemErro);
            });
        }
    }
}
