using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IstanziaBottoniQuizSceltaMultipla : MonoBehaviour
{
    [Header("Setup")]
    public GameObject bottonePrefab;
    public Transform content; // Content della Scroll View

    void Start() // START PRESENTE SOLO PER TESTARE LO SCRIPT, DA RIMUOVERE QUANDO SI COLLEGA IL JSON
    {
        string[] opzioniTest = new string[]
        {
            "Risposta 1",
            "Risposta 2",
            "Risposta 3",
            "Risposta 4",
        };

        CreaBottoni(opzioniTest);
    }

    public void CreaBottoni(string[] opzioni) // Metodo da chiamare passando le opzioni della domanda
    {
        // Pulisce i bottoni esistenti
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        // Istanzia i nuovi bottoni
        foreach (string opzione in opzioni)
        {
            GameObject btn = Instantiate(bottonePrefab, content);

            TMP_Text testo = btn.GetComponentInChildren<TMP_Text>();
            if (testo != null)
            {
                testo.text = opzione;
            }
            else
            {
                Text testoUI = btn.GetComponentInChildren<Text>();
                if (testoUI != null)
                    testoUI.text = opzione;
            }
        }

        // Forza aggiornamento layout (IMPORTANTE per Scroll View)
        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }
}