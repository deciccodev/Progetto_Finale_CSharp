using UnityEngine;

public class TestQuiz : MonoBehaviour
{
    [SerializeField] QuizController quizController;

    void Start()
    {
        // Simula la scelta del topic
        quizController.SelezionaArgomento("DomandeTipiDiDato.json");
    }
}