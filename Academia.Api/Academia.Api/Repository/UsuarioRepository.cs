using Microsoft.Data.SqlClient;

namespace Academia.Api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AcademiaDB") ?? "Not found";
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Email, Creditos, Cursos, Premium, Registro FROM Usuario";

                using (var command = new SqlCommand(query, connection))
                {
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
    }
}