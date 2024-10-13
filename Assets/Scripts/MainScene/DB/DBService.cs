using Cysharp.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using UnityEngine;

public class DBService : MonoBehaviour
{
    public static DBService instance; //Синглтон, не вижу смысла использовать тут Zenject (возможно, будет использован позже)

    [SerializeField] public LoadingWidget loadingWidget;
    [SerializeField] public PopupService popupService;
    [SerializeField, TextArea(1, 5)] private string connectionString;


    private void Start()
    {
        instance = this;
    }

    public async UniTask<(bool, string)> VerifyLoginAsync(string login, string password)
    {
        RequestEnable();

        await UniTask.SwitchToThreadPool();

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand();

            try
            {
                await connection.OpenAsync();

                command.Connection = connection;
                command.CommandText = "SELECT password FROM Users WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);

                object result = await command.ExecuteScalarAsync();

                if (result != null)
                {
                    string storedPassword = result.ToString();
                    if (storedPassword == password)
                        return (true, "Successful authorization.");
                    else
                        return (false, "Incorrect password.");
                }
                else
                    return (false, "The user with this username was not found");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
            finally
            {
                await RequestDisable();
            }
        }
    }

    public async UniTask<(bool, string)> RegisterUserAsync(string login, string password)
    {
        RequestEnable();

        await UniTask.SwitchToThreadPool(); //Разграничение потока

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand();

            try
            {
                await connection.OpenAsync();  // Открываем соединение асинхронно

                // Проверяем, есть ли уже пользователь с таким логином
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM Users WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);

                object result = await command.ExecuteScalarAsync();
                int count = Convert.ToInt32(result);

                if (count > 0)
                    return (false, "A user with this username already exists.");

                // Добавляем нового пользователя
                command.CommandText = "INSERT INTO Users (login, password) VALUES (@login, @password)";
                command.Parameters.AddWithValue("@password", password);

                // Асинхронное выполнение команды на вставку данных
                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                    return (true, "Successful registration");
                else
                    return (false, "User registration error.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
            finally
            {
                await RequestDisable();
            }
        }
    }

    public async UniTask<bool> SaveLevelDataAsync(string level, int userId, float completionTime)
    {
        RequestEnable();

        await UniTask.SwitchToThreadPool();

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new();

            try
            {
                await connection.OpenAsync();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Level1 (user_id, completion_time) VALUES (@user_id, @completion_time) ON DUPLICATE KEY UPDATE completion_time = @completion_time";

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
            finally
            {
                await RequestDisable();
            }
        }
    }

    public async UniTask<(bool success, float completionTime)> LoadLevelDataAsync(int userId)
    {
        RequestEnable();

        await UniTask.SwitchToThreadPool();

        float completionTime = 0;

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new();

            try
            {
                await connection.OpenAsync();

                command.Connection = connection;
                command.CommandText = "SELECT completion_time FROM Level1 WHERE user_id = @user_id";
                command.Parameters.AddWithValue("@user_id", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        if (reader["completion_time"] != DBNull.Value)
                        {
                            TimeSpan timeSpan = (TimeSpan)reader["completion_time"];
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
            finally
            {
                await RequestDisable();
            }
        }
    }


    private void RequestEnable()
    {
        loadingWidget.EnableWidget();
    }

    private async UniTask RequestDisable()
    {
        await UniTask.SwitchToMainThread();

        loadingWidget.DisableWidget();
    }
}

