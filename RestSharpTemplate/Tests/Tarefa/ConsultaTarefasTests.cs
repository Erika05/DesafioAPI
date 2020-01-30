using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.Requests.Tarefas;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class ConsultaTarefasTests : TestBase
    {
        ConsultaTodasTarefasRequest consultaTodasTarefasRequest = new ConsultaTodasTarefasRequest();
        ConsultaTarefasRequest consultaTarefasRequest = new ConsultaTarefasRequest();

        [Test]
        public void ConsultarTarefa()
        {
            #region Parameters
            string idTarefa = "4";
            string statusCodeEsperado = "OK";
            #endregion
            consultaTarefasRequest.SetParameters(idTarefa);
            IRestResponse<dynamic> response = consultaTarefasRequest.ExecuteRequest();

            Assert.Multiple(() =>
                {
                    Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                    //Assert.AreEqual(agendamentoId, response.Data["result"]["id"]);
                    //Assert.AreEqual(createDate, response.Data["result"]["createDate"]);
                    //Assert.AreEqual(recepcaoId, response.Data["result"]["recepcaoId"]);
                    //Assert.AreEqual(carteiraPacienteId, response.Data["result"]["carteiraPacienteId"]);
                    //Assert.AreEqual(dataAgendamento, response.Data["result"]["dataAgendamento"]);
                    //Assert.AreEqual(horaInicio, response.Data["result"]["horaInicio"]);
                    //Assert.AreEqual(horaFim, response.Data["result"]["horaFim"]);
                    //Assert.AreEqual(horaChegada, response.Data["result"]["horaChegada"]);
                    //Assert.AreEqual(status, response.Data["result"]["status"]);
                    //Assert.AreEqual(pacienteId, response.Data["result"]["pacienteId"]);
                    //Assert.AreEqual(pacienteNome, response.Data["result"]["pacienteNome"]);
                    //Assert.AreEqual(medicoId, response.Data["result"]["medicoId"]);
                    //Assert.AreEqual(medicoNome, response.Data["result"]["medicoNome"]);
                    //Assert.AreEqual(carteiraPacienteId, response.Data["result"]["paciente"]["carteiras"][0]["id"]);
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
    }
}
