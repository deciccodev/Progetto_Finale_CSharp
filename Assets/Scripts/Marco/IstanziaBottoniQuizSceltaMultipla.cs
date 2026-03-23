using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IstanziaBottoniQuizSceltaMultipla : MonoBehaviour // Script da inserire sul pannello dove creare i bottoni per i quiz a scelta multipla
{
    [Header("Setup")]
    [SerializeField] GameObject bottonePrefab;
    [SerializeField] Transform content; // Content della Scroll View
    private bool rispostaData = false;
    private Color coloreCorretto = Color.green;
    private Color coloreSbagliato = Color.red;
    private float delayRitornoMenu = 1.5f; 

    void Start() // START PRESENTE SOLO PER TESTARE LO SCRIPT, DA RIMUOVERE QUANDO SI COLLEGA IL JSON
    {
        string[] opzioniTest = new string[]
        {
            "Risposta 1",
            "Risposta 2",
            "Risposta 3",
            "Risposta 4",
        };

        //CreaBottoni(opzioniTest);
    }

    public void CreaBottoni(string[] opzioni, Question domanda, QuizController quizController)
    {
        rispostaData = false;

        // Pulisce bottoni esistenti nel caso fossero rimasti istanziati
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        Button[] bottoni = new Button[opzioni.Length];

        // Crea bottoni
        for (int i = 0; i < opzioni.Length; i++)
        {
            string opzione = opzioni[i];

            GameObject btnObj = Instantiate(bottonePrefab, content);
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

                // Disabilita tutti i bottoni dopo che l'utente da la risposta
                foreach (Button b in bottoni)
                    b.interactable = false;

                bool corretta = (index == domanda.rispostaCorretta);

                // Imposta il colore al bottone dopo la risposta (verde = giusta, rosso = sbagliata)
                for (int j = 0; j < bottoni.Length; j++)
                {
                    Image img = bottoni[j].GetComponent<Image>();

                    if (j == domanda.rispostaCorretta)
                    {
                        img.color = coloreCorretto; // risposta giusta
                    }
                    else if (j == index)
                    {
                        img.color = coloreSbagliato; // risposta sbagliata cliccata
                    }
                }

                // ⏱ Delay prima di tornare al menu
                StartCoroutine(RitornaDopoDelay(quizController, corretta));
            });
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }

    private System.Collections.IEnumerator RitornaDopoDelay(QuizController quizManager, bool risposta)
    {
        yield return new WaitForSeconds(delayRitornoMenu);

        quizManager.RispostaData(risposta);
    }
}