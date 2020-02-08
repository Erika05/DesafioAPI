using DesafioAPI.Helpers;
using System;
using System.IO;
using System.Text;

namespace DesafioAPI.DBSteps
{
    public class TarefaDBSteps
    {
        public static void DeleteTodasTarefas()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/DeleteTarefas.sql", Encoding.UTF8);
            DBHelpers.ExecuteQuery(query);
        }        

        public static int VerificaNotaTarefaExiste(string idNota)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/VerificaNotaTarefaExiste.sql", Encoding.UTF8).Replace("$idNota", idNota);

            return Int32.Parse(DBHelpers.RetornaDadosQuery(query)[0]);
        }

        public static int VerificaCopiaTarefaExiste(string idTarefaRelacionada)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/VerificaTagCopiaTarefaExiste.sql", Encoding.UTF8).Replace("$relationship", idTarefaRelacionada);

            return Int32.Parse(DBHelpers.RetornaDadosQuery(query)[0]);
        }

        public static int VerificaTarefaExiste(string idTarefa)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/VerificaTarefaExiste.sql", Encoding.UTF8).Replace("$idTarefa", idTarefa);

            return Int32.Parse(DBHelpers.RetornaDadosQuery(query)[0]);
        }
    }
}
