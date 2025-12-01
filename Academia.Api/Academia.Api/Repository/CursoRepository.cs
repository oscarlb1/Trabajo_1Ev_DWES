using System.Data.SqlClient;

namespace Academia.Api.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            var cursos = new List<Curso>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Titulo, Detalle, Costo, Horas, Publicado, Creacion, ProfesorId, MateriaId FROM Curso";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var curso = new Curso
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Detalle = reader.GetString(2),
                                Costo = reader.GetDecimal(3),
                                Horas = reader.GetInt32(4),
                                Publicado = reader.GetBoolean(5),
                                Creacion = reader.GetDateTime(6),
                                ProfesorId = reader.GetInt32(7),
                                MateriaId = reader.GetInt32(8)
                            };

                            cursos.Add(curso);
                        }
                    }
                }
            }
            return cursos;
        }

        public async Task<Curso?> GetByIdAsync(int id)
        {
            Curso? curso = null; 

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Titulo, Detalle, Costo, Horas, Publicado, Creacion, ProfesorId, MateriaId FROM Curso WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            curso = new Curso
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Detalle = reader.GetString(2),
                                Costo = reader.GetDecimal(3),
                                Horas = reader.GetInt32(4),
                                Publicado = reader.GetBoolean(5),
                                Creacion = reader.GetDateTime(6),
                                ProfesorId = reader.GetInt32(7),
                                MateriaId = reader.GetInt32(8)
                            };
                        }
                    }
                }
            }
            return curso; 
        }

        public async Task<Curso> AddAsync(Curso curso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Curso (Titulo, Detalle, Costo, Horas, Publicado, Creacion, ProfesorId, MateriaId) VALUES (@Titulo, @Detalle, @Costo, @Horas, @Publicado, @Creacion, @ProfesorId, @MateriaId); SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", curso.Titulo);
                    command.Parameters.AddWithValue("@Detalle", curso.Detalle);
                    command.Parameters.AddWithValue("@Costo", curso.Costo);
                    command.Parameters.AddWithValue("@Horas", curso.Horas);
                    command.Parameters.AddWithValue("@Publicado", curso.Publicado);
                    command.Parameters.AddWithValue("@Creacion", curso.Creacion);
                    command.Parameters.AddWithValue("@ProfesorId", curso.ProfesorId);
                    command.Parameters.AddWithValue("@MateriaId", curso.MateriaId);

                    var newId = await command.ExecuteScalarAsync();

                    if (newId != null && newId != DBNull.Value)
                        {
                            curso.Id = Convert.ToInt32(newId);
                        }
                }
            }
            return curso;
        }

        public async Task UpdateAsync(Curso curso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Curso SET Titulo = @Titulo, Detalle = @Detalle, Costo = @Costo, Horas = @Horas, Publicado = @Publicado, Creacion = @Creacion, ProfesorId = @ProfesorId, MateriaId = @MateriaId WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", curso.Titulo);
                    command.Parameters.AddWithValue("@Detalle", curso.Detalle);
                    command.Parameters.AddWithValue("@Costo", curso.Costo);
                    command.Parameters.AddWithValue("@Horas", curso.Horas);
                    command.Parameters.AddWithValue("@Publicado", curso.Publicado);
                    command.Parameters.AddWithValue("@Creacion", curso.Creacion);
                    command.Parameters.AddWithValue("@ProfesorId", curso.ProfesorId);
                    command.Parameters.AddWithValue("@MateriaId", curso.MateriaId);
                    command.Parameters.AddWithValue("@Id", curso.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Curso WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}