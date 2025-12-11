using System.Data.SqlClient;
using Academia.Api.Models;
using Academia.Api.Models.QueryParameters;
using System.Data.SqlClient;
using Academia.Api.Models.DTO;

namespace Academia.Api.Repositories
{
    public class OpinionRepository : IOpinionRepository
    {
        private readonly string _connectionString;

        public OpinionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Opinion>> GetAllAsync()
        {
            var opiniones = new List<Opinion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, FechaComentario, Mensaje, Puntuacion, CursoId FROM Opinion";
               
                using (var command = new SqlCommand(query, connection))
                {
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var opinion = new Opinion
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                FechaComentario = reader.GetDateTime(2),
                                Mensaje = reader.GetString(3),
                                Puntuacion = reader.GetInt32(4),
                                CursoId = reader.GetInt32(5)
                            };
                            opiniones.Add(opinion);
                        }
                    }
                }
            }
            return opiniones;
        }

        public async Task<List<Opinion>> GetAllAsyncParams(OpinionParameters parameters)
        {
            var opiniones = new List<Opinion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, FechaComentario, Mensaje, Puntuacion, CursoId FROM Opinion";
                string whereClause = "";
                string orderByClause = "";

                // WHERE para el filtro por puntuacion mínima
                if (!string.IsNullOrEmpty(parameters.minPuntuacion))
                {
                    whereClause = " WHERE Puntuacion >= @Puntuacion";
                }

                // ORDER BY
                if (!string.IsNullOrEmpty(parameters.OrderBy))
                {
                    // Orden por defecto ascendente
                    const string defaultDirection = "ASC";

                    // Ordenar por Fecha
                    if (parameters.OrderBy.Equals("Fecha", StringComparison.OrdinalIgnoreCase))
                    {
                        orderByClause = $" ORDER BY FechaComentario {defaultDirection}";
                    }
                    
                }

                query += whereClause + orderByClause;

                using (var command = new SqlCommand(query, connection))
                {
                    // Añadir parámetro SQL para el filtro WHERE
                    if (int.TryParse(parameters.minPuntuacion, out int minScore))
                    {
                        command.Parameters.AddWithValue("@Puntuacion", minScore);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var opinion = new Opinion
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                FechaComentario = reader.GetDateTime(2),
                                Mensaje = reader.GetString(3),
                                Puntuacion = reader.GetInt32(4),
                                CursoId = reader.GetInt32(5)
                            };
                            opiniones.Add(opinion);
                        }
                    }
                }
            }
            return opiniones;
        }

        public async Task<OpinionStatsDTO> GetStats()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
            SELECT COUNT(*) AS Total, AVG(CAST(Puntuacion AS DECIMAL(10, 2))) AS Media, MIN(Puntuacion) AS Minima, MAX(Puntuacion) AS Maxima FROM Opinion;";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new OpinionStatsDTO
                            {
                                TotalOpiniones = reader.GetInt32(reader.GetOrdinal("Total")),
                                PuntuacionMedia = reader.IsDBNull(reader.GetOrdinal("Media")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Media")),
                                PuntuacionMinima = reader.IsDBNull(reader.GetOrdinal("Minima")) ? 0 : reader.GetInt32(reader.GetOrdinal("Minima")),
                                PuntuacionMaxima = reader.IsDBNull(reader.GetOrdinal("Maxima")) ? 0 : reader.GetInt32(reader.GetOrdinal("Maxima"))
                            };
                        }
                    }
                }
                return new OpinionStatsDTO();
            }
        }

        public async Task<Opinion?> GetByIdAsync(int id)
        {
            Opinion? opinion = null; 

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, FechaComentario, Mensaje, Puntuacion, CursoId FROM Opinion WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            opinion = new Opinion
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                FechaComentario = reader.GetDateTime(2),
                                Mensaje = reader.GetString(3),
                                Puntuacion = reader.GetInt32(4),
                                CursoId = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return opinion; 
        }

        public async Task<Opinion> AddAsync(Opinion opinion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Opinion (Nombre, FechaComentario, Mensaje, Puntuacion, CursoId) VALUES (@Nombre, @FechaComentario, @Mensaje, @Puntuacion, @CursoId); SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", opinion.Nombre);
                    command.Parameters.AddWithValue("@FechaComentario", opinion.FechaComentario);
                    command.Parameters.AddWithValue("@Mensaje", opinion.Mensaje);
                    command.Parameters.AddWithValue("@Puntuacion", opinion.Puntuacion);
                    command.Parameters.AddWithValue("@CursoId", opinion.CursoId);

                    var newId = await command.ExecuteScalarAsync();

                    if (newId != null && newId != DBNull.Value)
                        {
                            opinion.Id = Convert.ToInt32(newId);
                        }
                }
            }
            return opinion;
        }


         public async Task UpdateAsync(Opinion opinion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Opinion SET Nombre = @Nombre, FechaComentario = @FechaComentario, Mensaje = @Mensaje, Puntuacion = @Puntuacion, CursoId = @CursoId WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", opinion.Nombre);
                    command.Parameters.AddWithValue("@FechaComentario", opinion.FechaComentario);
                    command.Parameters.AddWithValue("@Mensaje", opinion.Mensaje);
                    command.Parameters.AddWithValue("@Puntuacion", opinion.Puntuacion);
                    command.Parameters.AddWithValue("@CursoId", opinion.CursoId);;
                    command.Parameters.AddWithValue("@Id", opinion.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Opinion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
       
    }
}