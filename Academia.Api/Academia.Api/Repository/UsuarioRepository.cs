using System.Data.SqlClient;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Usuario>> GetAllAsync(UsuarioParameters parameters)
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Email, Creditos, Cursos, Premium, Registro FROM Usuario";
                string whereClause = "";
                string orderByClause = "";

                // WHERE para el filtro por Email
                if (!string.IsNullOrEmpty(parameters.Email))
                {
                    whereClause = " WHERE Email LIKE @Email";
                }

                // ORDER BY
                if (!string.IsNullOrEmpty(parameters.OrderBy))
                {
                    // Dirección del orden por defecto 
                    const string defaultDirection = "ASC";

                    // Por Registro
                    if (parameters.OrderBy.Equals("Registro", StringComparison.OrdinalIgnoreCase))
                    {
                        orderByClause = $" ORDER BY Registro {defaultDirection}";
                    }
                    // Por Creditos
                    else if (parameters.OrderBy.Equals("Creditos", StringComparison.OrdinalIgnoreCase))
                    {
                        orderByClause = $" ORDER BY Creditos {defaultDirection}";
                    }
                }

                query += whereClause + orderByClause;

                using (var command = new SqlCommand(query, connection))
                {
                    // Añadir parámetro SQL para el filtro WHERE
                    if (!string.IsNullOrEmpty(parameters.Email))
                    {
                        command.Parameters.AddWithValue("@Email", $"%{parameters.Email}%");
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Creditos = reader.GetDecimal(3),
                                Cursos = reader.GetInt32(4),
                                Premium = reader.GetBoolean(5),
                                Registro = reader.GetDateTime(6)
                            };
                            usuarios.Add(user);
                        }
                    }
                }
            }
            return usuarios;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            Usuario? usuario = null; 

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Email, Creditos, Cursos, Premium, Registro FROM Usuario WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Creditos = reader.GetDecimal(3),
                                Cursos = reader.GetInt32(4),
                                Premium = reader.GetBoolean(5),
                                Registro = reader.GetDateTime(6)
                            };
                        }
                    }
                }
            }
            return usuario; 
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuario (Nombre, Email, Creditos, Cursos, Premium, Registro) VALUES (@Nombre, @Email, @Creditos, @Cursos, @Premium, @Registro); SELECT CAST(scope_identity() AS int)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Creditos", usuario.Creditos);
                    command.Parameters.AddWithValue("@Cursos", usuario.Cursos);
                    command.Parameters.AddWithValue("@Premium", usuario.Premium);
                    command.Parameters.AddWithValue("@Registro", usuario.Registro);

                    var newId = await command.ExecuteScalarAsync();

                    if (newId != null && newId != DBNull.Value)
                        {
                            usuario.Id = Convert.ToInt32(newId);
                        }
                }
            }
            return usuario;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Usuario SET Nombre = @Nombre, Email = @Email, Creditos = @Creditos, Cursos = @Cursos, Premium = @Premium, Registro = @Registro WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Creditos", usuario.Creditos);
                    command.Parameters.AddWithValue("@Cursos", usuario.Cursos);
                    command.Parameters.AddWithValue("@Premium", usuario.Premium);
                    command.Parameters.AddWithValue("@Registro", usuario.Registro);

                    command.Parameters.AddWithValue("@Id", usuario.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Usuario WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}