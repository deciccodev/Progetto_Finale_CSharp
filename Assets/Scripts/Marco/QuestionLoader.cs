using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionLoader : MonoBehaviour
{
    private List<QuestionData> tutteLeDomande;
    private int questionToPop;

    void Awake()
    {
        CaricaDomande();
    }

    void CaricaDomande()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "questions.json");

        if (!File.Exists(path))
        {
            Debug.LogError("File questions.json non trovato in StreamingAssets!");
            return;
        }

        string json = File.ReadAllText(path);

        QuestionDataList wrapper = JsonUtility.FromJson<QuestionDataList>(json);
        tutteLeDomande = wrapper.questions;

        Debug.Log("Domande caricate: " + tutteLeDomande.Count);
    }

    /// <summary>
    /// Restituisce 3 domande filtrate e mescolate per topic e difficulty
    /// </summary>
    public List<QuestionData> GetQuestions(string topic, string difficulty)
    {
        // Filtra per topic e difficulty
        List<QuestionData> filtrate = tutteLeDomande.FindAll(q => q.topic == topic && q.difficulty == difficulty
        );

        if (filtrate.Count == 0)
        {
            Debug.LogWarning("Nessuna domanda trovata per topic/difficulty: " + topic + "/" + difficulty);
            return new List<QuestionData>();
        }

        // Mescola le domande
        MescolaLista(filtrate);

        // Prende solo le prime 3 domande dalla lista
        int count = Mathf.Min(questionToPop, filtrate.Count);
        return filtrate.GetRange(0, count);
    }

    void MescolaLista(List<QuestionData> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = Random.Range(i, lista.Count);

            QuestionData temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }
}