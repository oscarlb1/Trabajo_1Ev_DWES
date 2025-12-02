using System.Data.SqlClient;

namespace Academia.Api.Repositories
{
    public class InscripcionRepository : IInscripcionRepository
    {
        private readonly string _connectionString;

        public InscripcionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Inscripcion>> GetAllAsync()
        {
            var inscripciones = new List<Inscripcion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Progreso, Comentario, Nota, Activa, Inscripcion, UsuarioId, CursoId FROM Inscripcion";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var inscripcion = new Inscripcion
                            {
                                Id = reader.GetInt32(0),
                                Progreso = reader.GetString(1),
                                Comentario = reader.GetString(2),
                                Nota = reader.GetDecimal(3),
                                Activa = reader.GetBoolean(4),
                                InscripcionFecha = reader.GetDateTime(5),
                                UsuarioId = reader.GetInt32(6),
                                CursoId = reader.GetInt32(7)
                            };

                            inscripciones.Add(inscripcion);
                        }
                    }
                }
            }
            return inscripciones;
        }

        public async Task<Inscripcion?> GetByIdAsync(int id)
        {
            Inscripcion? inscripcion = null; 

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Progreso, Comentario, Nota, Activa, Inscripcion, UsuarioId, CursoId FROM Inscripcion WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            inscripcion = new Inscripcion
                            {
                                Id = reader.GetInt32(0),
                                Progreso = reader.GetString(1),
                                Comentario = reader.GetString(2),
                                Nota = reader.GetDecimal(3),
                                Activa = reader.GetBoolean(4),
                                InscripcionFecha = reader.GetDateTime(5),
                                UsuarioId = reader.GetInt32(6),
                                CursoId = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }
            return inscripcion; 
        }

        public async Task<Inscripcion> AddAsync(Inscripcion inscripcion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Inscripcion (Progreso, Comentario, Nota, Activa, Inscripcion, UsuarioId, CursoId) VALUES (@Progreso, @Comentario, @Nota, @Activa, @Inscripcion, @UsuarioId, @CursoId); SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Progreso", inscripcion.Progreso);
                    command.Parameters.AddWithValue("@Comentario", inscripcion.Comentario);
                    command.Parameters.AddWithValue("@Nota", inscripcion.Nota);
                    command.Parameters.AddWithValue("@Activa", inscripcion.Activa);
                    command.Parameters.AddWithValue("@Inscripcion", inscripcion.InscripcionFecha);
                    command.Parameters.AddWithValue("@UsuarioId", inscripcion.UsuarioId);
                    command.Parameters.AddWithValue("@CursoId", inscripcion.CursoId);

                    var newId = await command.ExecuteScalarAsync();

                    if (newId != null && newId != DBNull.Value)
                        {
                            inscripcion.Id = Convert.ToInt32(newId);
                        }
                }
            }
            return inscripcion;
        }

        public async Task UpdateAsync(Inscripcion inscripcion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Inscripcion SET Progreso = @Progreso, Comentario = @Comentario, Nota = @Nota, Activa = @Activa, Inscripcion = @Inscripcion, UsuarioId = @UsuarioId, CursoId = @CursoId WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Progreso", inscripcion.Progreso);
                    command.Parameters.AddWithValue("@Comentario", inscripcion.Comentario);
                    command.Parameters.AddWithValue("@Nota", inscripcion.Nota);
                    command.Parameters.AddWithValue("@Activa", inscripcion.Activa);
                    command.Parameters.AddWithValue("@Inscripcion", inscripcion.InscripcionFecha);
                    command.Parameters.AddWithValue("@UsuarioId", inscripcion.UsuarioId);
                    command.Parameters.AddWithValue("@CursoId", inscripcion.CursoId);

                    command.Parameters.AddWithValue("@Id", inscripcion.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Inscripcion WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}