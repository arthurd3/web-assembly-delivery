namespace api_delivery.Database;

using Dapper;
using System.Data;

public class DatabaseInitializer
{
    // CORRIGIDO: Injeta IDbConnection diretamente
    private readonly IDbConnection _connection;

    public DatabaseInitializer(IDbConnection connection)
    {
        _connection = connection;
    }

    public void Initialize()
    {
        // A conexão agora é injetada diretamente
        var sql = @"
        CREATE TABLE IF NOT EXISTS Users (
            Id           INTEGER PRIMARY KEY AUTOINCREMENT,
            Username     TEXT NOT NULL UNIQUE,
            PasswordHash TEXT NOT NULL,
            Email        TEXT
        );";
        
        _connection.Execute(sql);
    }
}