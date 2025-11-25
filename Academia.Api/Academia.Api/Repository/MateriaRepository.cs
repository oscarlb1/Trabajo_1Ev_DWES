using System.Data.SqlClient;

namespace Academia.Api.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly string _connectionString;

        public MateriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Materia>> GetAllAsync()
        {
            var materias = new List<Materia>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Detalle, Nivel, Cantidad, Obligatoria, Creacion FROM Materia";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var materia = new Materia
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Detalle = reader.GetString(2),
                                Nivel = reader.GetDecimal(3),
                                Cantidad = reader.GetInt32(4),
                                Obligatoria = reader.GetBoolean(5),
                                Creacion = reader.GetDateTime(6)
                            };

                            materias.Add(materia);
                        }
                    }
                }
            }
            return materias;
        }

        public async Task<Materia?> GetByIdAsync(int id)
        {
            Materia? materia = null; 

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Detalle, Nivel, Cantidad, Obligatoria, Creacion FROM Materia WHERE Id = @Id";

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
                                Detalle = reader.GetString(2),
                                Nivel = reader.GetDecimal(3),
                                Cantidad = reader.GetInt32(4),
                                Obligatoria = reader.GetBoolean(5),
                                Creacion = reader.GetDateTime(6)
                            };
                        }
                    }
                }
            }
            return materia; 
        }

        public async Task<Materia> AddAsync(Materia materia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Materia (Nombre, Detalle, Nivel, Cantidad, Obligatoria, Creacion) VALUES (@Nombre, @Detalle, @Nivel, @Cantidad, @Obligatoria, @Creacion); SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", materia.Nombre);
                    command.Parameters.AddWithValue("@Detalle", materia.Detalle);
                    command.Parameters.AddWithValue("@Nivel", materia.Nivel);
                    command.Parameters.AddWithValue("@Cantidad", materia.Cantidad);
                    command.Parameters.AddWithValue("@Obligatoria", materia.Obligatoria);
                    command.Parameters.AddWithValue("@Creacion", materia.Creacion);

                    var newId = await command.ExecuteScalarAsync();

                    if (newId != null && newId != DBNull.Value)
                        {
                            materia.Id = Convert.ToInt32(newId);
                        }
                }
            }
            return materia;
        }

        public async Task UpdateAsync(Materia materia)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Materia SET Nombre = @Nombre, Detalle = @Detalle, Nivel = @Nivel, Cantidad = @Cantidad, Obligatoria = @Obligatoria, Creacion = @Creacion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", materia.Nombre);
                    command.Parameters.AddWithValue("@Detalle", materia.Detalle);
                    command.Parameters.AddWithValue("@Nivel", materia.Nivel);
                    command.Parameters.AddWithValue("@Cantidad", materia.Cantidad);
                    command.Parameters.AddWithValue("@Obligatoria", materia.Obligatoria);
                    command.Parameters.AddWithValue("@Creacion", materia.Creacion);

                    command.Parameters.AddWithValue("@Id", materia.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Materia WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}