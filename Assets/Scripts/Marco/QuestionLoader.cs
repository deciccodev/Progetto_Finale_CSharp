using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    /// <summary>
    /// Carica le domande da un JSON relativo al topic selezionato
    /// Restituisce solo il numero di domande corretto in base alla difficoltà del player
    /// </summary>
    /// <param name="topicFileName">nome del file JSON del topic (es. "Fondamenta.json")</param>
    /// <param name="difficulty">0=facile, 1=medio, 2=difficile</param>
    public FormQuestion GetRandomQuestion(string topicFileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, topicFileName);

        if (!File.Exists(path))
        {
            Debug.LogError("File non trovato: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        FormQuestionList wrapper = JsonUtility.FromJson<FormQuestionList>(json);

        var lista = wrapper.questions;

        if (lista == null || lista.Count == 0)
            return null;

        int index = Random.Range(0, lista.Count);
        return lista[index];
    }
}

/// <summary>
/// Wrapper per JsonUtility (necessario per leggere una lista di domande)
/// </summary>
[System.Serializable]
public class FormQuestionList
{
    public List<FormQuestion> questions;
}