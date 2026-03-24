using UnityEngine;
using UnityEngine.UI;
using TMPro; // Necessario per TextMeshPro
using UnityEngine.SceneManagement; // Necessario per cambiare scena

public class QuizManager : MonoBehaviour
{
    public static QuizManager istanza;

    [Header("Impostazioni Difficoltà e Argomenti")]
    public string difficoltaScelta;
    public string argomentoScelto;
    public int quizScelto;

    [Header("Stati Colore Bottoni")]
    public Color colorLocked = Color.gray;
    public Color colorUnlocked = Color.white;
    public Color colorDone = Color.green;

    [Header("UI Risultato Finale")]
    public TextMeshProUGUI txtScore; // Trascina qui l'oggetto Txt_Score nell'Inspector

    void Awake()
    {
        // Singleton: per far sì che il manager sia unico
        if (istanza == null)
        {
            istanza = this;
            // Se vuoi che il manager non venga distrutto tra le scene, usa:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- LOGICA SELEZIONE DIFFICOLTÀ ---
    public void ImpostaDifficolta(string nomeDifficolta)
    {
        difficoltaScelta = nomeDifficolta;
        Debug.Log("Difficoltà salvata: " + difficoltaScelta);
    }

    // --- LOGICA SELEZIONE ARGOMENTO E QUIZ ---
    public void SelezionaQuiz(string argomento, int numeroQuiz, Button bottoneCliccato)
    {
        // Se il bottone è grigio (Locked), blocca l'azione
        if (bottoneCliccato.image.color == colorLocked)
        {
            Debug.Log("Spiacente, questo Quiz è ancora Bloccato!");
            return;
        }

        argomentoScelto = argomento;
        quizScelto = numeroQuiz;
        
        Debug.Log($"Dati Salvati: Argomento {argomento} - Quiz n. {numeroQuiz}");
        
        // Qui sotto potrai aggiungere il comando per aprire il pannello Teoria
        // MenuNavigation.istanza.OpenPanel(Panel_Theory);
    }

    // --- LOGICA RISULTATO FINALE ---
    public void MostraRisultato(int puntiOttenuti, int totaleDomande)
    {
        if (txtScore != null)
        {
            txtScore.text = "Punteggio: " + puntiOttenuti + " / " + totaleDomande;
        }
    }

    // --- NAVIGAZIONE SCENA ---
    public void CaricaMenu()
    {
        // Assicurati che nel Build Settings la scena del menu si chiami esattamente "MenuScene"
        SceneManager.LoadScene("MenuScene");
    }
}