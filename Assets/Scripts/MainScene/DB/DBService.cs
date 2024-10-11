using Cysharp.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public static class DBService
{
    private static string connectionString = "server=l64uf.h.filess.io;user=Drone_extrapaid;database=Drone_extrapaid;port=3306;password=cf3d934fa540d96fe90bead0dac7e6fd8ee14b04;SslMode=None;Pooling=false;";

    private static MySqlConnection connection;


    public static async UniTask<bool> VerifyLoginAsync(string login, string password)
    {
        await UniTask.SwitchToThreadPool(); //������������� ������

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand();

            try
            {
                await connection.OpenAsync();  // ����������� �������� ����������

                // SQL ������ �� ��������� ������ �� ������
                command.Connection = connection;
                command.CommandText = "SELECT password FROM Users WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);

                // ����������� ���������� �������
                object result = await command.ExecuteScalarAsync();

                if (result != null)
                {
                    string storedPassword = result.ToString();
                    return storedPassword == password;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("������ ��� �������� ������: " + ex.Message);
                return false;
            }
        }
    }

    public static async UniTask<bool> RegisterUserAsync(string login, string password)
    {
        await UniTask.SwitchToThreadPool(); //������������� ������

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand();

            try
            {
                await connection.OpenAsync();  // ��������� ���������� ����������

                // ���������, ���� �� ��� ������������ � ����� �������
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM Users WHERE login = @login";
                command.Parameters.AddWithValue("@login", login);

                object result = await command.ExecuteScalarAsync();
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    // ������������ � ����� ������� ��� ����������
                    Debug.Log("������������ � ����� ������� ��� ����������.");
                    return false;
                }

                // ��������� ������ ������������
                command.CommandText = "INSERT INTO Users (login, password) VALUES (@login, @password)";
                command.Parameters.AddWithValue("@password", password);

                // ����������� ���������� ������� �� ������� ������
                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.LogError("������ ��� ����������� ������������: " + ex.Message);
                return false;
            }
        }
    }

    public static async UniTask<bool> SaveLevelDataAsync(string level, int userId, TimeSpan completionTime, List<(Vector3, Quaternion)> recordedData)
    {
        await UniTask.SwitchToThreadPool(); //������������� ������

        // ����������� recordedData � JSON
        var dataToSave = recordedData.Select(data => new
        {
            Position = new { data.Item1.x, data.Item1.y, data.Item1.z },
            Rotation = new { data.Item2.x, data.Item2.y, data.Item2.z, data.Item2.w }
        });

        string json = JsonConvert.SerializeObject(dataToSave);

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand();

            try
            {
                await connection.OpenAsync(); // ��������� ���������� ����������

                // SQL-������ ��� ������� ������
                command.Connection = connection;
                command.CommandText = "INSERT INTO Level1 (user_id, completion_time, level_data) VALUES (@user_id, @completion_time, @level_data) ON DUPLICATE KEY UPDATE completion_time = @completion_time, level_data = @level_data";
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@completion_time", completionTime); // ������������ ����� � ������ ������, ���� ���������
                command.Parameters.AddWithValue("@level_data", json);

                // ����������� ���������� ������� �� ������� ������
                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.LogError("������ ��� ���������� ������ ������: " + ex.Message);
                return false;
            }
        }
    }

    public static async Task<(bool success, TimeSpan completionTime, List<(Vector3, Quaternion)> recordedData)> GetLevelDataAsync(int userId)
    {
        TimeSpan completionTime = TimeSpan.Zero; // �������������� �������� �� ���������
        List<(Vector3, Quaternion)> recordedData = new List<(Vector3, Quaternion)>(); // �������������� ������ ������

        using (var connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand();

            try
            {
                await connection.OpenAsync(); // ��������� ���������� ����������

                // SQL-������ ��� ��������� ������
                command.Connection = connection;
                command.CommandText = "SELECT completion_time, level_data FROM Level1 WHERE user_id = @user_id";
                command.Parameters.AddWithValue("@user_id", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // �������� ����� ����������� ������
                        if (reader["completion_time"] != DBNull.Value)
                        {
                            completionTime = (TimeSpan)reader["completion_time"];
                        }

                        // �������� JSON-������
                        if (reader["level_data"] != DBNull.Value)
                        {
                            string json = reader["level_data"].ToString();
                            // ������������� JSON � ������ ��������� ��������
                            var positions = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

                            // ����������� � ������ ��������
                            foreach (var pos in positions)
                            {
                                var position = JsonConvert.DeserializeObject<Vector3>(pos["Position"].ToString());
                                var rotation = JsonConvert.DeserializeObject<Quaternion>(pos["Rotation"].ToString());
                                recordedData.Add((position, rotation));
                            }
                        }
                    }
                    else
                    {
                        // ���� ������ �� �������
                        Debug.LogWarning("������ �� ������� ��� user_id: " + userId);
                        return (false, completionTime, recordedData); // ���������� false
                    }
                }

                return (true, completionTime, recordedData); // �������� ����������
            }
            catch (Exception ex)
            {
                Debug.LogError("������ ��� ��������� ������ ������: " + ex.Message);
                return (false, completionTime, recordedData); // ���������� false ��� ������
            }
        }
    }
}
