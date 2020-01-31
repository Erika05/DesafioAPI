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
    }
}
