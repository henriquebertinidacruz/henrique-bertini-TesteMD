namespace SistemaGerenciamento.Database
{
    public static class Connection
    {
        private static string _ConnectionDatabase = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=sistema_gerenciamento";

        public static string ConnectionDatabase
        {
            get { return _ConnectionDatabase; }
            set { _ConnectionDatabase = value; }
        }
    }
}
