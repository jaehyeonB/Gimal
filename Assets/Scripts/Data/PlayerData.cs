using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public List<string> collectedPassive = new List<string>();
    public int stage;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public PlayerData playerData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void SaveData(PlayerData playerData)
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        string json = JsonUtility.ToJson(playerData, true);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("���� ������ �����: " + json);
    }

    public PlayerData LoadData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("���� ������ �ε� ��");
            return playerData;
        }
        else
        {
            Debug.LogWarning("����� ���� �����Ͱ� �����ϴ�.");
            return new PlayerData();
        }
    }
    public void GameStart()
    {
        playerData = LoadData();

        if (playerData == null)
        {
            playerData = new PlayerData();
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("Level" + playerData.stage);           //���� -> playerData.stage
        }
    }

    public void PlayerDead()
    {
        PlayerData playerData = LoadData();
        if(playerData == null)
        {
            playerData.stage = 1;

            foreach (string item in playerData.collectedPassive.ToList())
            {
                if(UnityEngine.Random.Range(0,1) == 0)
                {
                    playerData.collectedPassive.Remove(item);
                }
            }

            SaveData(playerData);
        }
        SceneManager.LoadScene("GameOver");
    }
}
