using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    /// <summary>
    /// Carica le domande da un JSON relativo al topic selezionato
    /// Restituisce un numero di domande in base alla difficoltà del player
    /// Le domande vengono scelte casualmente dal file
    /// </summary>
    /// <param name="topicFileName">nome del file JSON del topic (es. "Fondamenta.json")</param>
    /// <param name="difficulty">0=facile, 1=medio, 2=difficile</param>
    /// <returns>Lista di domande random selezionate</returns>
    public List<FormQuestion> LoadQuestions(string topicFileName, int difficulty)
    {
        string path = Path.Combine(Application.streamingAssetsPath, topicFileName);

        if (!File.Exists(path))
        {
            Debug.LogError("File non trovato: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        FormQuestionList wrapper = JsonUtility.FromJson<FormQuestionList>(json);

        List<FormQuestion> lista = wrapper.questions;

        if (lista == null || lista.Count == 0)
            return null;

        // Determina quante domande prendere in base alla difficulty
        int numeroDomande = 3;
        if (difficulty == 1) numeroDomande = 5;
        else if (difficulty == 2) numeroDomande = 10;

        // Se ci sono meno domande di quelle richieste, prendile tutte
        numeroDomande = Mathf.Min(numeroDomande, lista.Count);

        // Mescola la lista
        List<FormQuestion> listaRandom = new List<FormQuestion>();
        List<int> indiciUsati = new List<int>();
        while (listaRandom.Count < numeroDomande)
        {
            int index = Random.Range(0, lista.Count);
            if (!indiciUsati.Contains(index))
            {
                indiciUsati.Add(index);
                listaRandom.Add(lista[index]);
            }
        }

        return listaRandom;
    }

    /// <summary>
    /// Restituisce una singola domanda random (se dovesse servire di nuovo)
    /// </summary>
    /*public FormQuestion GetRandomQuestion(string topicFileName)
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
    }*/
}

/// <summary>
/// Wrapper per JsonUtility (necessario per leggere una lista di domande)
/// </summary>
[System.Serializable]
public class FormQuestionList
{
    public List<FormQuestion> questions;
}