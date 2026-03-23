using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IstanziaBottoniQuizSceltaMultipla : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] GameObject bottonePrefab;
    [SerializeField] Transform content; // Content della Scroll View

    private bool rispostaData = false;
    private Color coloreCorretto = Color.green;
    private Color coloreSbagliato = Color.red;
    private float delayProssimaDomanda = 1.5f; // Delay prima di passare alla prossima domanda

    /// <summary>
    /// Crea i bottoni per una domanda a scelta multipla
    /// </summary>
    public void CreaBottoni(FormQuestion domanda, QuizController quizController)
    {
        rispostaData = false;

        // Pulisce bottoni esistenti
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        string[] opzioni = new string[]
        {
            domanda.answerA,
            domanda.answerB,
            domanda.answerC,
            domanda.answerD
        };

        Button[] bottoni = new Button[opzioni.Length];

        for (int i = 0; i < opzioni.Length; i++)
        {
            string opzione = opzioni[i];

            GameObject btnObj = Instantiate(bottonePrefab, content);
            btnObj.GetComponent<LayoutElement>().preferredWidth = 500f;

            Button btn = btnObj.GetComponent<Button>();
            bottoni[i] = btn;

            TextMeshProUGUI testo = btnObj.GetComponentInChildren<TextMeshProUGUI>();
            if (testo != null)
                testo.text = opzione;

            int index = i;

            btn.onClick.AddListener(() =>
            {
                if (rispostaData) return;

                rispostaData = true;

                // Blocca tutti i bottoni
                foreach (Button b in bottoni)
                    b.interactable = false;

                bool corretta = (index == domanda.rightAnswer);

                // Colora i bottoni
                for (int j = 0; j < bottoni.Length; j++)
                {
                    Image img = bottoni[j].GetComponent<Image>();
                    if (j == domanda.rightAnswer)
                        img.color = coloreCorretto;
                    else if (j == index)
                        img.color = coloreSbagliato;
                }

                // Avvia la prossima domanda dopo il delay
                StartCoroutine(AvanzaDopoDelay(quizController, corretta));
            });
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }

    private System.Collections.IEnumerator AvanzaDopoDelay(QuizController quizController, bool risposta)
    {
        yield return new WaitForSeconds(delayProssimaDomanda);

        // Notifica il QuizController e lascia a lui la gestione della prossima domanda
        quizController.RispostaData(risposta);
    }
}