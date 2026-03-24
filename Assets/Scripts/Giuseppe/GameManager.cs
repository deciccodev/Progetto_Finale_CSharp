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

    /*
    public int progressVariabili;
    public int progressCicli;
    public int progressDesignPattern;
    public int progressOOP;
    public int progressMetodi;
    */

    public float gameVolume;
}

//[System.Serializable]
public enum TypeQuestion
{
    DomandaMultipla,
    Input,
    Dragger,
    Maze
}

[System.Serializable]
public class FormQuestion
{

    public TypeQuestion QuestionType;
    public int rightAnswer;

    public string question;
    public string answerA;
    public string tooltipA;
    public string answerB;
    public string tooltipB;
    public string answerC;
    public string tooltipC;
    public string answerD;
    public string tooltipD;
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
        catch (Exception e)
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
    
    public void SetDifficulty(int i) 
    {
        if(i>=0 && i<= _maxDifficulty && _playerData.difficulty != i)
        {
            _playerData.difficulty = i;
            _playerData.progressLvl = 0;
        }
    }

    public int GetDifficulty()
    {
        return _playerData.difficulty;
    }

    //public void ProgressToSave(int i) {_playerData.progressLvl = i;}
    
    

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

    //Getter Progressi
    //
    public int GetProgress(string tipoProgresso)
    {
        return _playerData.progressLvl;
        /*
            switch (tipoProgresso)
            {
                case "variabili":
                    Debug.Log($"Ritorno il progresso sulle variabili [{_playerData.progressVariabili}]");
                    return _playerData.progressVariabili;
                case "cicli":
                    Debug.Log($"Ritorno il progresso sui cicli [{_playerData.progressCicli}]");
                    return _playerData.progressCicli;
                case "designPattern":
                    Debug.Log($"Ritorno il progresso sui design pattern [{_playerData.progressDesignPattern}]");
                    return _playerData.progressDesignPattern;
                case "oop":
                    Debug.Log($"Ritorno il progresso sui principi OOP [{_playerData.progressOOP}]");
                    return _playerData.progressOOP;
                case "metodi":
                    Debug.Log($"Ritorno il progresso sui metodi [{_playerData.progressMetodi}]");
                    return _playerData.progressMetodi;
                case "livelli":
                    Debug.Log($"Ritorno il progresso sui livelli [{_playerData.progressLvl}]");
                    return _playerData.progressLvl;
                default:
                    Debug.Log("Valore per il progresso non valido");
                    return 0;

        }
        */
    }

    //Setter Progressi
    public void SetProgress(/*bool isPassed*//*string tipoProgresso, int valoreProgress*/)
    {
        _playerData.progressLvl += 1;

        /*
        if (isPassed)
        {
            _playerData.progressLvl += 1;
        }
        */

        /*
        // Metodo privato  SetMatchProgress
            switch (tipoProgresso)
            {
                case "variabili":
                    Debug.Log($"Aggiorno il progresso sulle variabili [{_playerData.progressVariabili} diventa {valoreProgress}]");
                    if (_playerData.progressVariabili != valoreProgress)
                    {
                        _playerData.progressVariabili = valoreProgress;
                    }
                    break;

                case "cicli":
                    Debug.Log($"Aggiorno il progresso sui cicli [{_playerData.progressCicli} diventa {valoreProgress}]");
                    if (_playerData.progressCicli != valoreProgress)
                    {
                        _playerData.progressCicli = valoreProgress;
                    }
                    break;

                case "designPattern":
                    Debug.Log($"Aggiorno il progresso sui design pattern [{_playerData.progressDesignPattern} diventa {valoreProgress}]");
                    if(_playerData.progressDesignPattern != valoreProgress)
                    {
                        _playerData.progressDesignPattern = valoreProgress;
                    }
                    break;

                case "oop":
                    Debug.Log($"Aggiorno il progresso sui principi OOP [{_playerData.progressOOP} diventa {valoreProgress}]");
                    if( _playerData.progressOOP != valoreProgress)
                    {
                        _playerData.progressOOP = valoreProgress;
                    }
                    break;

                case "metodi":
                    Debug.Log($"Aggiorno il progresso sui metodi [{_playerData.progressMetodi} diventa {valoreProgress}]");
                    if(_playerData.progressMetodi != valoreProgress)
                    {
                        _playerData.progressMetodi = valoreProgress;
                    }
                    break;

                case "livelli":
                    Debug.Log($"Aggiorno il progresso sui livelli [{_playerData.progressLvl} diventa {valoreProgress}]");
                    if (_playerData.progressLvl != valoreProgress)
                    {
                        _playerData.progressLvl = valoreProgress;
                    }
                    break;

                default:
                    Debug.Log("Valore per il progresso non valido");
                    break;
        }*/

    }


    #endregion

}

