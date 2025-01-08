using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace csharp_front
{
    internal class StudentRequest
    {
        public async void Request(string matricula, DataGridView studentsDataGrid)
        {
            //Set the data source
            DataTable studentsDataTable = await ProcessRepositoriesAsync(matricula, studentsDataGrid);
            studentsDataGrid.DataSource = studentsDataTable;
        }
        protected async Task<DataTable> ProcessRepositoriesAsync(string matricula, DataGridView studentsDataGrid)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET");

                HttpResponseMessage response = await client.GetAsync($"http://localhost:5112/api/Students/{matricula}");

                if (response.IsSuccessStatusCode)
                {
                    var students = await JsonSerializer.DeserializeAsync<List<StudentsRepository>>(response.Content.ReadAsStreamAsync().Result);

                    // Create and populate the datatable
                    DataColumn[] columns = {
                        new DataColumn("Matrícula"),
                        new DataColumn("Tipo de Pago"),
                        new DataColumn("Institucíon")
                    };
                    dataTable.Columns.AddRange(columns);

                    foreach (var student in students ?? Enumerable.Empty<StudentsRepository>())
                    {
                        Object[] studentRow = { student.matricula, student.tipoPago, student.institucion };
                        dataTable.Rows.Add(studentRow);
                    }
                }
                else
                {
                    // problems handling here
                    MessageBox.Show("No se encontraron resultados!", "Error");
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION: " + exception.Message);
                MessageBox.Show(exception.Message, "Error en el servicio!");
            }

            return dataTable;
        }
    }
}
