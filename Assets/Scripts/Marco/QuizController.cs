using UnityEngine;
using TMPro;

public class QuizController : MonoBehaviour
{
    private FormQuestion domandaCorrente;
    private string topicCorrente;

    [Header("Pannelli Tipi Quiz")]
    [SerializeField] GameObject pannelloSceltaMultipla;
    /*[SerializeField] GameObject pannelloInputField;
    [SerializeField] GameObject pannelloDragAndDrop;
    [SerializeField] GameObject pannelloMaze;*/

    [Header("Pannello Argomenti Quiz (Fondamenta, ecc...)")]
    [SerializeField] GameObject pannelloArgomentiQuiz;

    [Header("Pannello Dove Visualizzare La Domanda")]
    [SerializeField] TextMeshProUGUI testoDomanda;

    [Header("Riferimenti")]
    [SerializeField] QuestionLoader questionLoader;
    [SerializeField] ProgressMenu menuQuiz;

    // SELEZIONE ARGOMENTO
    public void SelezionaArgomento(string topicFileName)
    {
        topicCorrente = topicFileName;

        int difficulty = GameManager.Instance.GetDifficulty();

        int numeroDomande = 3;
        if (difficulty == 1) numeroDomande = 5;
        else if (difficulty == 2) numeroDomande = 10;

        // Mostra il pannello con i bottoni quiz
        DisattivaTutti();
        pannelloArgomentiQuiz.SetActive(true);

        // Crea i bottoni (quiz)
        menuQuiz.CreaMenu(numeroDomande, this);
    }

    // AVVIO QUIZ (UNA DOMANDA)
    public void AvviaQuiz(int index)
    {
        domandaCorrente = questionLoader.GetRandomQuestion(topicCorrente);

        if (domandaCorrente == null)
        {
            Debug.LogError("Domanda non caricata!");
            return;
        }

        DisattivaTutti();
        MostraDomanda();
    }

    // MOSTRA DOMANDA
    void MostraDomanda()
    {
        if (testoDomanda != null)
            testoDomanda.text = domandaCorrente.question;

        switch (domandaCorrente.QuestionType)
        {
            case TypeQuestion.DomandaMultipla:
                pannelloSceltaMultipla.SetActive(true);

                var uiSceltaMultipla = pannelloSceltaMultipla.GetComponent<IstanziaBottoniQuizSceltaMultipla>();
                uiSceltaMultipla.CreaBottoni(domandaCorrente, this);
                break;

            //TODO CREARE CLASSI CHE INIZIALIZZANO GLI ALTRI QUIZ
            /*case TypeQuestion.Input: pannelloInputField.SetActive(true);

                var uiInput = pannelloInputField.GetComponent<IstanziaInputfield>();
                uiInput.MostraDomanda(domandaCorrente, this);
                break;*/

            /*case TypeQuestion.Dragger: pannelloDragAndDrop.SetActive(true);

                var uiDrag = pannelloDragAndDrop.GetComponent<IstanziaDragAndDrop>();
                uiDrag.MostraDomanda(domandaCorrente, this);
                break;*/

            /*case TypeQuestion.Maze: pannelloMaze.SetActive(true);

                var uiMaze = pannelloMaze.GetComponent<IstanziaMaze>();
                uiMaze.MostraDomanda(domandaCorrente, this);
                break;*/
        }
    }

    // RISPOSTA DATA
    public void RispostaData(bool corretta)
    {
        Debug.Log(corretta ? "Corretto!" : "Sbagliato!");

        // aggiorna stato bottoni nel menu
        menuQuiz.SegnaCompletata(corretta);

        TornaAlMenuQuiz();
    }

    // TORNA AL MENU QUIZ
    void TornaAlMenuQuiz()
    {
        DisattivaTutti();
        pannelloArgomentiQuiz.SetActive(true);
    }

    // UTILITY
    void DisattivaTutti()
    {
        pannelloSceltaMultipla.SetActive(false);
        /*pannelloInputField.SetActive(false);
        pannelloDragAndDrop.SetActive(false);
        pannelloMaze.SetActive(false);*/

        pannelloArgomentiQuiz.SetActive(false);
    }
}