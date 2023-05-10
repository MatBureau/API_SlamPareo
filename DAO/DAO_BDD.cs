using Npgsql;


namespace DLL_DAO
{
    public class DAO_BDD
    {
        private static NpgsqlConnection _Conn;

        public static void Connecter()
        {
            _Conn = new NpgsqlConnection();
            _Conn.ConnectionString = ConnectionService.GetConnectionString();
            _Conn.Open();
        }
        public static void CloseConn()
        {
            _Conn.Close();
        }

        public NpgsqlConnection Connexion
        {
            get { return _Conn; }
        }
    }
}
