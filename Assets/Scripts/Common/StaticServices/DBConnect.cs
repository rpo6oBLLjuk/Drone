using Cysharp.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

public static class DBConnect
{
    private static string connectionString = "server=l64uf.h.filess.io;user=Drone_extrapaid;database=Drone_extrapaid;port=3306;password=cf3d934fa540d96fe90bead0dac7e6fd8ee14b04;SslMode=None;Pooling=false;";

    public static async UniTask<(bool success, string error)> VerifyLoginAsync(string login, string password, Action endAction)
    {
        await UniTask.SwitchToThreadPool();

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new();

            try
            {
                await connection.OpenAsync();

                command.Connection = connection;
                command.CommandText = "SELECT user_id, password FROM Users WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        int user_id = reader.GetInt32("user_id");
                        string storedPassword = reader.GetString("password");

                        if (storedPassword == password)
                        {
                            await UniTask.SwitchToMainThread();
                            SaveService.SaveLocalUser(user_id, login);

                            return (true, "Successful authorization.");
                        }
                        else
                            return (false, "Incorrect password.");
                    }
                    else
                        return (false, "The user with this username was not found.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
            finally
            {
                endAction?.Invoke();
            }
        }
    }

    public static async UniTask<(bool success, string error)> RegisterUserAsync(string login, string password, Action endAction)
    {
        await UniTask.SwitchToThreadPool(); //Разграничение потока

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new();

            try
            {
                await connection.OpenAsync();


                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM Users WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);

                object result = await command.ExecuteScalarAsync();
                int count = Convert.ToInt32(result);

                if (count > 0)
                    return (false, "A user with this username already exists.");

                // Добавляем нового пользователя
                command.CommandText = "INSERT INTO Users (login, password) VALUES (@login, @password); SELECT LAST_INSERT_ID();";
                command.Parameters.AddWithValue("@password", password);

                // Асинхронное выполнение команды на вставку данных и получение user_id
                object insertResult = await command.ExecuteScalarAsync();
                int newUserId = Convert.ToInt32(insertResult); // Получаем user_id

                if (newUserId > 0)
                {
                    SaveService.SaveLocalUser(newUserId, login);
                    return (true, $"Successful registration, id: {newUserId}");
                }
                else
                    return (false, "User registration error.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
            finally
            {
                endAction?.Invoke();
            }
        }
    }

    public static async UniTask<bool> SaveLevelDataAsync(int level, int userId, float completionTime)
    {
        await UniTask.SwitchToThreadPool();

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new();

            try
            {
                await connection.OpenAsync();
                command.Connection = connection;

                string levelColumn = $"completion_time_level{level}";
                command.CommandText = $"INSERT INTO LevelCompletionTimes (user_id, {levelColumn}) VALUES (@user_id, @completion_time) " +
                                      $"ON DUPLICATE KEY UPDATE {levelColumn} = @completion_time";

                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@completion_time", TimeSpan.FromSeconds(completionTime));

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.LogError("Ошибка при сохранении данных уровня: " + ex.Message);
                return false;
            }
        }
    }

    public static async UniTask<(bool success, float completionTime)> LoadLevelDataAsync(int level, int userId)
    {
        await UniTask.SwitchToThreadPool();

        float completionTime = 0;

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new();

            try
            {
                await connection.OpenAsync();
                command.Connection = connection;

                string levelColumn = $"completion_time_level{level}";
                command.CommandText = $"SELECT {levelColumn} FROM LevelCompletionTimes WHERE user_id = @user_id";
                command.Parameters.AddWithValue("@user_id", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        if (reader[levelColumn] != DBNull.Value)
                        {
                            TimeSpan timeSpan = (TimeSpan)reader[levelColumn];
                            completionTime = (float)timeSpan.TotalSeconds;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Данные не найдены для user_id: " + userId);
                        return (false, completionTime);
                    }
                }

                return (true, completionTime);
            }
            catch (Exception ex)
            {
                Debug.LogError("Ошибка при получении данных уровня: " + ex.Message);
                return (false, completionTime);
            }
        }
    }

}
