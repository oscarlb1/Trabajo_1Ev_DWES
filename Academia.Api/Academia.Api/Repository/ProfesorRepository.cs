using System.Data.SqlClient;

namespace Academia.Api.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly string _connectionString;

        public ProfesorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Profesor>> GetAllAsync()
        {
            var profesores = new List<Profesor>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Especialidad, Salario, Experiencia, Certificado, Contrato, Email FROM Profesor";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var profe = new Profesor
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Especialidad = reader.GetString(2),
                                Salario = reader.GetDecimal(3),
                                Experiencia = reader.GetInt32(4),
                                Certificado = reader.GetBoolean(5),
                                Contrato = reader.GetDateTime(6),
                                Email = reader.GetString(7)
                            };

                            profesores.Add(profe);
                        }
                    }
                }
            }
            return profesores;
        }

        public async Task<Profesor?> GetByIdAsync(int id)
        {
            Profesor? profesor = null; 

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Especialidad, Salario, Experiencia, Certificado, Contrato, Email FROM Profesor WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            profesor = new Profesor
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Especialidad = reader.GetString(2),
                                Salario = reader.GetDecimal(3),
                                Experiencia = reader.GetInt32(4),
                                Certificado = reader.GetBoolean(5),
                                Contrato = reader.GetDateTime(6),
                                Email = reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return profesor; 
        }

        public async Task<Profesor> AddAsync(Profesor profesor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Profesor (Nombre, Especialidad, Salario, Experiencia, Certificado, Contrato, Email) VALUES (@Nombre, @Especialidad, @Salario, @Experiencia, @Certificado, @Contrato, @Email); SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                    command.Parameters.AddWithValue("@Especialidad", profesor.Especialidad);
                    command.Parameters.AddWithValue("@Salario", profesor.Salario);
                    command.Parameters.AddWithValue("@Experiencia", profesor.Experiencia);
                    command.Parameters.AddWithValue("@Certificado", profesor.Certificado);
                    command.Parameters.AddWithValue("@Contrato", profesor.Contrato);
                    command.Parameters.AddWithValue("@Email", profesor.Email);

                    var newId = await command.ExecuteScalarAsync();

                    if (newId != null && newId != DBNull.Value)
                        {
                            profesor.Id = Convert.ToInt32(newId);
                        }
                }
            }
            return profesor;
        }

        public async Task UpdateAsync(Profesor profesor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Profesor SET Nombre = @Nombre, Especialidad = @Especialidad, Salario = @Salario, Experiencia = @Experiencia, Certificado = @Certificado, Contrato = @Contrato, Email = @Email WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", profesor.Nombre);
                    command.Parameters.AddWithValue("@Especialidad", profesor.Especialidad);
                    command.Parameters.AddWithValue("@Salario", profesor.Salario);
                    command.Parameters.AddWithValue("@Experiencia", profesor.Experiencia);
                    command.Parameters.AddWithValue("@Certificado", profesor.Certificado);
                    command.Parameters.AddWithValue("@Contrato", profesor.Contrato);
                    command.Parameters.AddWithValue("@Email", profesor.Email);

                    command.Parameters.AddWithValue("@Id", profesor.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Profesor WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}