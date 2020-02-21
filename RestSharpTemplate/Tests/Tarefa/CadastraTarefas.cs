﻿using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.Tests.Projeto;
using NUnit.Framework;
using RestSharp;


namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class CadastroTarefas : TestBase
    {
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        HelpersProjetos helpersProjetos = new HelpersProjetos();

        [Test]
        public void CadastrarTarefaMinimal()
        {
            #region Parameters
            string resumo = "Tarefa minima";
            string descricao = "tarefa minimal";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            IRestResponse<dynamic> response = cadastraTarefaRequest.ExecuteRequest();
            string retornoSummary = response.Data["issue"]["summary"];
            string retornoDescription = response.Data["issue"]["description"];
            string retornoProject = response.Data["issue"]["project"]["name"];
            string retornoCategory = response.Data["issue"]["category"]["name"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(resumo, retornoSummary);
                Assert.AreEqual(descricao, retornoDescription);
                Assert.AreEqual(projeto, retornoProject);
                Assert.AreEqual(categoria, retornoCategory);
            });
        }

        [Test]
        public void CadastrarTarefa()
        {
            //Criar tag 
            #region Parameters
            string resumo = "Tarefa resumo completa";
            string descricao = "Tarefa resumo completa";
            string informacao = "informacao";
            string projeto = "projeto geral";
            string categoria = "General";
            string visibilidade = "private";
            string prioridade = "high";
            string severidade = "trivial";
            string reprodutibilidade = "sometimes";
          //  string tag = "tag tarefa";
             string tag = "e";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, informacao, projeto, categoria, visibilidade, prioridade, severidade, reprodutibilidade, tag);
            IRestResponse<dynamic> response = cadastraTarefaRequest.ExecuteRequest();
            string retornoSummary = response.Data["issue"]["summary"];
            string retornoDescription = response.Data["issue"]["description"];
            string retornoInformation = response.Data["issue"]["additional_information"];
            string retornoProject = response.Data["issue"]["project"]["name"];
            string retornoCategory = response.Data["issue"]["category"]["name"];
            string retornoVisibilidade = response.Data["issue"]["view_state"]["name"];
            string retornoPrioridade = response.Data["issue"]["priority"]["name"];
            string retornoSeveridade = response.Data["issue"]["severity"]["name"];
            string retornoReprodutibilidade = response.Data["issue"]["reproducibility"]["name"];
            string retornoTag = response.Data["issue"]["tags"][0]["name"];

            Assert.Multiple(() =>
           {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
               Assert.AreEqual(resumo, retornoSummary);
               Assert.AreEqual(descricao, descricao);
               Assert.AreEqual(informacao, retornoInformation);
               Assert.AreEqual(projeto, retornoProject);
               Assert.AreEqual(categoria, retornoCategory);
               Assert.AreEqual(visibilidade, retornoVisibilidade);
               Assert.AreEqual(prioridade, retornoPrioridade);
               Assert.AreEqual(severidade, retornoSeveridade);
               Assert.AreEqual(reprodutibilidade, retornoReprodutibilidade);
               Assert.AreEqual(tag, retornoTag);
           });
        }

        [Test]
        public void CadastrarTarefaAnexo()
        {
            #region Parameters
            string resumo = "This is a test issue";
            string descricao = "This is a test description";
            string categoria = "General";
            string projeto = "projeto geral";
            string nomeAnexo = "test.txt";
            string anexo = "VGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4NClRoaXMgaXMgYSBURVNULg0KVGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4=";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto, nomeAnexo, anexo);
            IRestResponse<dynamic> response = cadastraTarefaRequest.ExecuteRequest();
            string retornoSummary = response.Data["issue"]["summary"];
            string retornoDescription = response.Data["issue"]["description"];
            string retornoProject = response.Data["issue"]["project"]["name"];
            string retornoCategory = response.Data["issue"]["category"]["name"];
            string retornoNomeAnexo = response.Data["issue"]["attachments"][0]["filename"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(resumo, retornoSummary);
                Assert.AreEqual(descricao, retornoDescription);
                Assert.AreEqual(projeto, retornoProject);
                Assert.AreEqual(categoria, retornoCategory);
                Assert.AreEqual(nomeAnexo, retornoNomeAnexo);
            });
        }

        [Test]
        public void ResumoTarefaNaoInformado()
        {
            #region Parameters
            string descricao = "descricao";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "Summary not specified";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody("", descricao, categoria, projeto);
            IRestResponse<dynamic> response = cadastraTarefaRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, statusCodeEsperado);
                Assert.AreEqual(descricaoErro, retornoMensagemErro);
            });
        }

        [Test]
        public void DescricaoTarefaNaoInformado()
        {
            #region Parameters
            string resumo = "resumo";
            string descricao = "descricao";
            string categoria = "General";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "Project not specified";
            #endregion
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, "");
            IRestResponse<dynamic> response = cadastraTarefaRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, statusCodeEsperado);
                Assert.AreEqual(descricaoErro, retornoMensagemErro);
            });
        }

        [Test]
        public void ProjetoTarefaNaoInformado()
        {
            #region Parameters
            string resumo = "resumo";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "BadRequest";
            string descricaoErro = "Description not specified";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, "", categoria, projeto);
            IRestResponse<dynamic> response = cadastraTarefaRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, statusCodeEsperado);
                Assert.AreEqual(descricaoErro, retornoMensagemErro);
            });
        }
    }
}
