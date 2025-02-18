using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using People.Models;

namespace People
{
    public class PersonRepository
    {
        private string _dbPath;
        private SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        // Método Init modificado a asincrónico
        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Person>();
        }

        // Método AddNewPerson modificado a asincrónico
        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                // Llamada a Init()
                await Init();

                // Validación básica para asegurar que se ha ingresado un nombre
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new Person { Name = name });

                StatusMessage = string.Format("{0} record(s) added [Name: {1}]", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

        // Método GetAllPeople modificado a asincrónico
        public async Task<List<Person>> GetAllPeople()
        {
            try
            {
                await Init();
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
