using System.Collections.Generic;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    private FormQuestion domandaCorrente;
    private List<FormQuestion> domandeDisponibili;
    private int indexDomanda = 0;

    [Header("Pannelli Tipi Quiz")]
    [SerializeField] GameObject pannelloSceltaMultipla;
    [SerializeField] GameObject pannelloInputField;
    [SerializeField] GameObject pannelloDragAndDrop;
    [SerializeField] GameObject pannelloMaze;

    [Header("Pannello Quiz Argomenti")]
    [SerializeField] GameObject pannelloArgomentiQuiz; // menu precedente

    [Header("Loader")]
    [SerializeField] QuestionLoader questionLoader;

    // Chiamato quando l'utente seleziona un argomento
    public void SelezionaArgomento(string topicFileName)
    {
        int difficulty = GameManager.Instance.GetDifficulty(); // recupera difficoltà salvata

        // Carica le domande dal JSON
        domandeDisponibili = questionLoader.LoadQuestions(topicFileName, difficulty);

        if (domandeDisponibili.Count == 0)
        {
            Debug.LogWarning("Nessuna domanda disponibile per questo topic.");
            return;
        }

        indexDomanda = 0;
        domandaCorrente = domandeDisponibili[indexDomanda];

        pannelloArgomentiQuiz.SetActive(false); // nascondi menu argomenti
        MostraDomanda();
    }

    void MostraDomanda()
    {
        DisattivaTutti();

        switch ((TypeQuestion)domandaCorrente.QuestionType)
        {
            case TypeQuestion.DomandaMultipla:
                pannelloSceltaMultipla.SetActive(true);

                var uiSceltaMultipla = pannelloSceltaMultipla.GetComponent<IstanziaBottoniQuizSceltaMultipla>();
                uiSceltaMultipla.CreaBottoni(domandaCorrente, this);
                break;

            case TypeQuestion.Input:
                pannelloInputField.SetActive(true);
                var uiInputField = pannelloInputField.GetComponent<IstanziaInputfield>();
                uiInputField.MostraDomanda(domandaCorrente, this);
                break;

            case TypeQuestion.Dragger:
                pannelloDragAndDrop.SetActive(true);
                var uiDragAndDrop = pannelloDragAndDrop.GetComponent<IstanziaDragAndDrop>();
                uiDragAndDrop.MostraDomanda(domandaCorrente, this);
                break;

            case TypeQuestion.Maze:
                pannelloMaze.SetActive(true);
                var uiMaze = pannelloMaze.GetComponent<IstanziaMaze>();
                uiMaze.MostraDomanda(domandaCorrente, this);
                break;
        }
    }

    void DisattivaTutti()
    {
        pannelloSceltaMultipla.SetActive(false);
        pannelloInputField.SetActive(false);
        pannelloDragAndDrop.SetActive(false);
        pannelloMaze.SetActive(false);
        pannelloArgomentiQuiz.SetActive(false);
    }

    // Chiamato quando l’utente risponde
    public void RispostaData(bool corretta)
    {
        Debug.Log(corretta ? "Corretto!" : "Sbagliato!");

        // Passa alla prossima domanda se disponibile
        indexDomanda++;
        if (indexDomanda < domandeDisponibili.Count)
        {
            domandaCorrente = domandeDisponibili[indexDomanda];
            MostraDomanda();
        }
        else
        {
            TornaAlMenu();
        }
    }

    void TornaAlMenu()
    {
        DisattivaTutti();
        pannelloArgomentiQuiz.SetActive(true);
    }
}