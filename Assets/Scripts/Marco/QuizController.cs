using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizController : MonoBehaviour
{
    private List<FormQuestion> domandeCorrenti;
    private int indexDomanda = 0;
    private int risposteCorrette = 0;
    private string topicCorrente;

    [Header("Pannelli")]
    [SerializeField] GameObject pannelloArgomenti;
    [SerializeField] GameObject pannelloQuiz;

    [Header("Panel Tipi Quiz")]
    [SerializeField] GameObject pannelloSceltaMultipla;
    /*[SerializeField] GameObject pannelloInputField;
    [SerializeField] GameObject pannelloDragAndDrop;
    [SerializeField] GameObject pannelloMaze;*/

    [Header("Panel Visualizzazione Domanda")]
    [SerializeField] TextMeshProUGUI testoDomanda;

    [Header("Loader")]
    [SerializeField] QuestionLoader questionLoader;

    // SELEZIONE ARGOMENTO
    public void SelezionaArgomento(string topicFileName)
    {
        topicCorrente = topicFileName;

        int difficulty = GameManager.Instance.GetDifficulty();

        // Carica lista domande random in base alla difficoltà
        domandeCorrenti = questionLoader.LoadQuestions(topicFileName, difficulty);

        if (domandeCorrenti == null || domandeCorrenti.Count == 0)
        {
            Debug.LogError("Nessuna domanda trovata!");
            return;
        }

        indexDomanda = 0;
        risposteCorrette = 0;

        pannelloArgomenti.SetActive(false);
        pannelloQuiz.SetActive(true);

        MostraDomanda();
    }

    // MOSTRA DOMANDA
    void MostraDomanda()
    {
        DisattivaTipi();

        FormQuestion domanda = domandeCorrenti[indexDomanda];

        // Mostra il testo della domanda
        testoDomanda.text = domanda.question;

        switch (domanda.QuestionType)
        {
            case TypeQuestion.DomandaMultipla:
                pannelloSceltaMultipla.SetActive(true);
                var uiSceltaMultipla = pannelloSceltaMultipla.GetComponent<IstanziaBottoniQuizSceltaMultipla>();
                uiSceltaMultipla.CreaBottoni(domanda, this);
                break;

            //TODO CREARE SCRIPT PER ISTANZIARE GLI ALTRI PANEL
            /*case TypeQuestion.Input: pannelloInputField.SetActive(true);
                var uiInput = pannelloInputField.GetComponent<IstanziaInputField>();
                break;

            case TypeQuestion.Dragger: pannelloDragAndDrop.SetActive(true);
                var uiDragger = pannelloDragAndDrop.GetComponent<IstanziaDragger>();
                break;

            case TypeQuestion.Maze: pannelloMaze.SetActive(true);
                var uiMaze = pannelloMaze.GetComponent<IstanziaMaze>();
                break;*/
        }
    }

    // RISPOSTA DATA
    public void RispostaData(bool corretta)
    {
        if (corretta)
            risposteCorrette++;

        indexDomanda++;

        if (indexDomanda < domandeCorrenti.Count)
        {
            MostraDomanda();
        }
        else
        {
            FineQuiz();
        }
    }

    // FINE QUIZ
    void FineQuiz()
    {
        int totale = domandeCorrenti.Count;
        bool superato = risposteCorrette >= (totale - 1);

        Debug.Log("Corrette: " + risposteCorrette + "/" + totale);

        if (superato)
        {
            Debug.Log("TOPIC SUPERATO");

            // TODO: gestire sblocco topic successivo
            GameManager.Instance.ProgressToSave(GameManager.Instance.GetDifficulty() + 1);
            GameManager.Instance.SaveData();
        }
        else
        {
            Debug.Log("TOPIC FALLITO - riprova");
        }

        TornaAlMenu();
    }

    // TORNA AL MENU
    void TornaAlMenu()
    {
        pannelloQuiz.SetActive(false);
        pannelloArgomenti.SetActive(true);
    }

    // UTILITY - disattiva tutti i tipi panel
    void DisattivaTipi()
    {
        pannelloSceltaMultipla.SetActive(false);
        /*pannelloInputField.SetActive(false);
        pannelloDragAndDrop.SetActive(false);
        pannelloMaze.SetActive(false);*/
    }
}