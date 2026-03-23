using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData //Classe pubblica per i dati del player da scrivere/leggere
{
    public string userName;
    public int maxScore;
    public int difficulty;
    public int progressLvl;
    public float gameVolume;
}
//[System.Serializable]
public enum TypeQuestion
{
    DomandaMultipla,
    Toggle,
    Input,
    Dragger
}

[System.Serializable]
public class FormQuestion
{
    public int difficulty;

    public TypeQuestion QuestionType;
    public int rightAnswer;

    public string question;
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;

}


//Singleton
public sealed class GameManager : MonoBehaviour
{
    #region Proprietà di classe
    private PlayerData _playerData = new PlayerData();
    [SerializeField] private string _path;

    //Valore placeholder possiamo inserire qualsiasi livello di difficoltà, il minimo sarà sempre 0
    private int _maxDifficulty = 2;

    //Costruttore privato ? 
    private static GameManager _instance;
    public static GameManager Instance 
    {
        get
        {
            return _instance;
        }
    }


    #endregion

    void Awake()
    {
        _path = Path.Combine(Application.persistentDataPath, "PlayerData.json");

        //Istanza Singleton
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Controlliamo se il file json esiste, se non esiste lo creiamo
        if (File.Exists(_path))
        {
            LoadData();
        }
        else
        {
            //Impostiamo dei valori di default per il player
            _playerData.userName = "Player";
            _playerData.gameVolume = 75f;
            SaveData();
        }
    }

    #region Salva & Carica

    public void SaveData()
    {
        try
        {
            //Converto le proprietà pubbliche di player data in una stringa in formato json
            //  il true serve per indentare la stringa e renderla più leggibile
            string jsonString = JsonUtility.ToJson(_playerData,true); 
            File.WriteAllText(_path,jsonString);
            Debug.Log("Salvataggio completato");
        }
        catch (System.Exception e)
        {
            Debug.Log($"Eccezione durante il salvataggio: {e}");
        }
    }

    public void LoadData()
    {
        try
        {
            string jsonString = File.ReadAllText(_path);
            _playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        }
        catch (Exception e)
        {
            Debug.Log($"Eccezione durante il caricamento: {e}");
        }
    }

    #endregion

    #region Scene Manager
    public void NextScene(string NextScene)
    {
        Debug.Log("Cambio scena...");
        SceneManager.LoadScene(NextScene);
    }

    public void MenuScene(GameObject menu )
    {
        if(menu != null)
        {
            //Se l'oggetto è attivo lo disattiva e vice versa 
            menu.SetActive(!menu.activeSelf);
        }
    }

    #endregion

    #region Info Player
    // Gestione delle proprietà della classe per impostare il salvataggio dei dati

    public void NameToSave(string name)
    {
        if (!string.IsNullOrWhiteSpace(name) && name!= _playerData.userName)
        {
            _playerData.userName = name;
            Debug.Log("Username aggiornato!");
        }
    }
    public void ScoreToSave(int i)
    {
        //Prendiamo il valore maggiore e lo assegniamo a maxscore
        _playerData.maxScore = Mathf.Max(_playerData.maxScore, i);

    }
    public void DifficultyToSave(int i) 
    {
        if(i>=0 && i<= _maxDifficulty)
        {
            _playerData.difficulty = i;
        }
    }

    public void ProgressToSave(int i) {_playerData.progressLvl = i;}
    public void VolumeToSave(float i)
    {
        if(i>=0 && i<=100)
        {
            _playerData.gameVolume = i;
        }
        else
        {
            _playerData.gameVolume = 75f;
        }
    }

    #endregion

}

