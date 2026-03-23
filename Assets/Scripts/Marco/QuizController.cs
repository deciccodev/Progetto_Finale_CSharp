using UnityEngine;

public class QuizController : MonoBehaviour
{
    private Question domandaCorrente;

    [Header("Pannelli Tipi Quiz")]
    [SerializeField] GameObject pannelloSceltaMultipla;
    [SerializeField] GameObject pannelloInputField;
    [SerializeField] GameObject pannelloDragAndDrop;
    [SerializeField] GameObject pannelloMaze;

    [Header("Pannello Quiz Argomenti")]
    [SerializeField] GameObject pannelloArgomentiQuiz; // menu precedente

    public void AvviaQuiz(Question domanda)
    {
        domandaCorrente = domanda;

        MostraDomanda();
    }

    void MostraDomanda()
    {
        DisattivaTutti();

        switch (domandaCorrente.TypeQuestion)
        {
            case TypeQuestion.DomandaMultipla: pannelloSceltaMultipla.SetActive(true);

                var uiSceltaMultipla = pannelloSceltaMultipla.GetComponent<IstanziaBottoniQuizSceltaMultipla>();

                uiSceltaMultipla.CreaBottoni(domandaCorrente.opzioni, domandaCorrente, this);
                break;

            case TypeQuestion.Input: pannelloInputField.SetActive(true);

                var uiInputField = pannelloInputField.GetComponent<IstanziaInputfield>();

                break;

            case TypeQuestion.Dragger: pannelloDragAndDrop.SetActive(true);

                var uiDragAndDrop = pannelloDragAndDrop.GetComponent<IstanziaDragAndDrop>();

                break;

            case TypeQuestion.Maze: pannelloMaze.SetActive(true);

            var uiMaze = pannelloMaze.GetComponent<IstanziaMaze>();

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

    // Da chiamare quando l’utente risponde
    public void RispostaData(bool corretta)
    {
        Debug.Log(corretta ? "Corretto!" : "Sbagliato!");

        TornaAlMenu();
    }

    void TornaAlMenu()
    {
        DisattivaTutti();
        pannelloArgomentiQuiz.SetActive(true);
    }
}