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
    public List<FormQuestion> LoadQuestions(string topicFileName, int difficulty)
    {
        string path = Path.Combine(Application.streamingAssetsPath, topicFileName);

        if (!File.Exists(path))
        {
            Debug.LogError("File JSON del topic non trovato: " + path);
            return new List<FormQuestion>();
        }

        string jsonString = File.ReadAllText(path);

        // JsonUtility richiede un wrapper per le liste
        FormQuestionList wrapper = JsonUtility.FromJson<FormQuestionList>(jsonString);

        List<FormQuestion> tutteLeDomande = wrapper.questions;

        // Determina quante domande prendere in base alla difficoltà
        int numDomande = 3; // default facile
        switch (difficulty)
        {
            case 0: numDomande = 3; break;
            case 1: numDomande = 5; break;
            case 2: numDomande = 10; break;
        }

        // Mescola le domande
        MescolaLista(tutteLeDomande);

        // Prendi solo le prime N domande disponibili
        int count = Mathf.Min(numDomande, tutteLeDomande.Count);
        return tutteLeDomande.GetRange(0, count);
    }

    private void MescolaLista(List<FormQuestion> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = Random.Range(i, lista.Count);
            FormQuestion temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
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