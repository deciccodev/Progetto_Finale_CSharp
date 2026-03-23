using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    /// <summary>
    /// Carica le domande da un JSON relativo al topic selezionato
    /// Restituisce esattamente il numero di domande corretto in base alla difficoltà
    /// </summary>
    /// <param name="topicFileName">Nome file JSON (es. "DomandeTipiDiDato.json")</param>
    /// <param name="difficulty">0=facile, 1=medio, 2=difficile</param>
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

        if (wrapper?.questions == null || wrapper.questions.Count == 0)
        {
            Debug.LogError("Lista domande vuota!");
            return null;
        }

        // Mescola la lista
        List<FormQuestion> domandeMescolate = new List<FormQuestion>(wrapper.questions);
        ShuffleList(domandeMescolate);

        // Determina quante domande prendere
        int n = 3; // default facile
        switch (difficulty)
        {
            case 0: n = 3; break;
            case 1: n = 5; break;
            case 2: n = 10; break;
        }

        // Prende solo le prime N domande
        if (n > domandeMescolate.Count)
            n = domandeMescolate.Count;

        return domandeMescolate.GetRange(0, n);
    }

    // Metodo per mescolare la lista domande
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            T tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }
}

[System.Serializable]
public class FormQuestionList
{
    public List<FormQuestion> questions;
}