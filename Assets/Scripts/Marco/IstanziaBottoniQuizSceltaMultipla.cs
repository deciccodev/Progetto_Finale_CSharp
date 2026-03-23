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
    private float delayRitornoMenu = 1.5f;

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

        // Crea bottoni
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

                // Colora bottoni
                for (int j = 0; j < bottoni.Length; j++)
                {
                    Image img = bottoni[j].GetComponent<Image>();
                    if (j == domanda.rightAnswer)
                        img.color = coloreCorretto; // corretto
                    else if (j == index)
                        img.color = coloreSbagliato; // sbagliato selezionato
                }

                // Delay prima di notificare QuizController
                StartCoroutine(RitornaDopoDelay(quizController, corretta));
            });
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }

    private System.Collections.IEnumerator RitornaDopoDelay(QuizController quizController, bool risposta)
    {
        yield return new WaitForSeconds(delayRitornoMenu);
        quizController.RispostaData(risposta);
    }
}