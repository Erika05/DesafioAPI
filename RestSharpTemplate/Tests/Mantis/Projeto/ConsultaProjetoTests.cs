using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using NUnit.Framework;
using RestSharp;
using System.Collections;
using System.Collections.Generic;

namespace DesafioAPI.Tests.Projeto
{

    [TestFixture]
    public class ConsultaProjetoTests : TestBase
    {
        ConsultaProjetoRequests consultaProjetoRequests = new ConsultaProjetoRequests();
        ConsultaTodosProjetosRequest consultaTodosProjetosRequest = new ConsultaTodosProjetosRequest();
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        DeletaProjetoRequests deletaProjetoRequests = new DeletaProjetoRequests();

        [Test]
        public void ConsultarProjeto()
        {
            #region Parameters
            string nomeProjeto = "projeto consulta";
            string statusCodeEsperado = "OK";
            #endregion
            string  idProjeto = helpersProjetos.PreparaBaseCadastradoProjeto(nomeProjeto);
            consultaProjetoRequests.SetParameters(idProjeto);
            IRestResponse<dynamic> response = consultaProjetoRequests.ExecuteRequest();
            string retornoIdProjeto = response.Data["projects"][0]["id"];
            string retornoNomeProjeto = response.Data["projects"][0]["name"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idProjeto, retornoIdProjeto);
                Assert.AreEqual(nomeProjeto, retornoNomeProjeto);
            });
        }

        [Test]
        public void ConsultarProjetoNaoCadastrado()
        {
            #region Parameters
            string idProjeto = "1414";
            string statusCodeEsperado = "NotFound";
            string mensagemErro = "Project #"+idProjeto+" not found";
            #endregion
            if (!ProjetoDBSteps.VerificaProjetoPeloId(idProjeto).Equals(0))
            {
                deletaProjetoRequests.SetParameters(idProjeto);
                deletaProjetoRequests.ExecuteRequest();
            }
            consultaProjetoRequests.SetParameters(idProjeto);
            IRestResponse<dynamic> response = consultaProjetoRequests.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(mensagemErro, retornoMensagemErro);
            });
        }

        [Test]
        public void ConsultarTodosProjetos()
        {
            #region Parameters
            string nomeProjetoI = "projeto todas as consultas I";
            string nomeProjetoII = "projeto todas as consultas II";
            string statusCodeEsperado = "OK";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(nomeProjetoI);
            helpersProjetos.PreparaBaseCadastradoProjeto(nomeProjetoII);
            IRestResponse<dynamic> response = consultaTodosProjetosRequest.ExecuteRequest();
            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        }
    }
}