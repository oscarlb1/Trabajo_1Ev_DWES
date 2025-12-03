using System.Data.SqlClient;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public class LeccionRepository : ILeccionRepository
    {
        private readonly string _connectionString;

        public LeccionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Leccion>> GetAllAsync(LeccionParameters parameters)
        {
            var lecciones = new List<Leccion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Titulo, Tipo, Minutos, Examen, Publicacion, URL, CursoId FROM Leccion";
                string whereClause = "";
                string orderByClause = "";

                // WHERE para el filtro por Tipo
                if (!string.IsNullOrEmpty(parameters.Tipo))
                {
                    whereClause = " WHERE Tipo LIKE @Tipo";
                }

                // ORDER BY
                if (!string.IsNullOrEmpty(parameters.OrderBy))
                {
                    // Dirección del orden por defecto 
                    const string defaultDirection = "ASC";

                    // Por Publicacion
                    if (parameters.OrderBy.Equals("Publicacion", StringComparison.OrdinalIgnoreCase))
                    {
                        orderByClause = $" ORDER BY Publicacion {defaultDirection}";
                    }
                    // Por Minutos
                    else if (parameters.OrderBy.Equals("Minutos", StringComparison.OrdinalIgnoreCase))
                    {
                        orderByClause = $" ORDER BY Minutos {defaultDirection}";
                    }
                }

                query += whereClause + orderByClause;

                using (var command = new SqlCommand(query, connection))
                {
                    // Añadir parámetro SQL para el filtro WHERE
                    if (!string.IsNullOrEmpty(parameters.Tipo))
                    {
                        command.Parameters.AddWithValue("@Tipo", $"%{parameters.Tipo}%");
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var leccion = new Leccion
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Tipo = reader.GetString(2),
                                Minutos = reader.GetInt32(3),
                                Examen = reader.GetBoolean(4),
                                Publicacion = reader.GetDateTime(5),
                                URL = reader.GetString(6),
                                CursoId = reader.GetInt32(7)
                            };
                            lecciones.Add(leccion);
                        }
                    }
                }
            }
            return lecciones;
        }

        public async Task<Leccion?> GetByIdAsync(int id)
        {
            Leccion? leccion = null; 

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Titulo, Tipo, Minutos, Examen, Publicacion, URL, CursoId FROM Leccion WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            leccion = new Leccion
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Tipo = reader.GetString(2),
                                Minutos = reader.GetInt32(3),
                                Examen = reader.GetBoolean(4),
                                Publicacion = reader.GetDateTime(5),
                                URL = reader.GetString(6),
                                CursoId = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }
            return leccion; 
        }

        public async Task<Leccion> AddAsync(Leccion leccion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Leccion (Titulo, Tipo, Minutos, Examen, Publicacion, URL, CursoId) VALUES (@Titulo, @Tipo, @Minutos, @Examen, @Publicacion, @URL, @CursoId); SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", leccion.Titulo);
                    command.Parameters.AddWithValue("@Tipo", leccion.Tipo);
                    command.Parameters.AddWithValue("@Minutos", leccion.Minutos);
                    command.Parameters.AddWithValue("@Examen", leccion.Examen);
                    command.Parameters.AddWithValue("@Publicacion", leccion.Publicacion);
                    command.Parameters.AddWithValue("@URL", leccion.URL);
                    command.Parameters.AddWithValue("@CursoId", leccion.CursoId);

                    var newId = await command.ExecuteScalarAsync();

                    if (newId != null && newId != DBNull.Value)
                        {
                            leccion.Id = Convert.ToInt32(newId);
                        }
                }
            }
            return leccion;
        }

        public async Task UpdateAsync(Leccion leccion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Leccion SET Titulo = @Titulo, Tipo = @Tipo, Minutos = @Minutos, Examen = @Examen, Publicacion = @Publicacion, URL = @URL, CursoId = @CursoId WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", leccion.Titulo);
                    command.Parameters.AddWithValue("@Tipo", leccion.Tipo);
                    command.Parameters.AddWithValue("@Minutos", leccion.Minutos);
                    command.Parameters.AddWithValue("@Examen", leccion.Examen);
                    command.Parameters.AddWithValue("@Publicacion", leccion.Publicacion);
                    command.Parameters.AddWithValue("@URL", leccion.URL);
                    command.Parameters.AddWithValue("@CursoId", leccion.CursoId);

                    command.Parameters.AddWithValue("@Id", leccion.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Leccion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}