using UnityEngine;

public class TestQuiz : MonoBehaviour
{
    [SerializeField] QuizController quizController;

    void Start()
    {
        // Simula la scelta del topic
        quizController.SelezionaArgomento("questions_test.json");
    }
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