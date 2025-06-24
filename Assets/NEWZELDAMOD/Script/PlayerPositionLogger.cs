using System.IO;
using UnityEngine;

public class PlayerPositionLogger : MonoBehaviour
{
    string logFilePath;

    void Start()
    {
        logFilePath = Path.Combine(Application.persistentDataPath, "player_position_log.txt");
        LogPosition("Start");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))  // Aperte P para salvar a posição atual no log
        {
            LogPosition("Update");
        }
    }

    void LogPosition(string method)
    {
        string message = $"{method} - Player position: {transform.position} at time {Time.time}";
        File.AppendAllText(logFilePath, message + "\n");
    }
}
