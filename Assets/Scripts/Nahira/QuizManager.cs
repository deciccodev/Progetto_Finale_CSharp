using UnityEngine;
using UnityEngine.UI; // Necessario per gestire i colori dei bottoni

public class QuizManager : MonoBehaviour
{
    public static QuizManager istanza;

    [Header("Stati Colore")]
    public Color colorLocked = Color.gray;
    public Color colorUnlocked = Color.white;
    public Color colorDone = Color.green;

    public string argomentoScelto;
    public int quizScelto;

    void Awake() { istanza = this; }

    // Funzione chiamata dai bottoni del Quiz
    public void SelezionaQuiz(string argomento, int numeroQuiz, Button bottoneCliccato)
    {
        // Se il bottone è grigio, non fare nulla (è locked)
        if (bottoneCliccato.image.color == colorLocked)
        {
            Debug.Log("Quiz Bloccato!");
            return;
        }

        argomentoScelto = argomento;
        quizScelto = numeroQuiz;
        
        Debug.Log($"Scelto: {argomento} - Quiz {numeroQuiz}");
        
        // Qui apri il pannello della teoria o del quiz
        // MenuNavigation.istanza.OpenPanel(Panel_Theory); 
    }
}