using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Legge i dati della domanda, spezza il testo sul token "___",
// popola i due TMP del panel sinistro e spawna i DraggableItem nel panel destro.

public class IstanziaDragger : MonoBehaviour
{
    [Header("Panel sinistro")]
    [SerializeField] private TextMeshProUGUI tmpPrima;
    [SerializeField] private TextMeshProUGUI tmpDopo;
    [SerializeField] private DropSlot dropSlot;

    [Header("Panel destro")]
    [SerializeField] private Transform draggableContainer;
    [SerializeField] private GameObject draggablePrefab;

    // Token usato nel JSON per indicare la posizione dello slot
    private const string SLOT_TOKEN = "___";

    public void CreaDomandaDragger(FormQuestion data)
    {
        ResetUI();

        // Imposta la risposta corretta sullo slot
        dropSlot.RightAnswer = data.rightAnswer;

        // Spezza il testo della domanda sul token ___
        string[] parti = data.question.Split(new string[] { SLOT_TOKEN }, System.StringSplitOptions.None);

        tmpPrima.text = parti.Length > 0 ? parti[0].Trim() : string.Empty;
        tmpDopo.text  = parti.Length > 1 ? parti[1].Trim() : string.Empty;

        // Costruisce la lista delle risposte e le shuffla
        List<(string label, int index)> risposte = new()
        {
            (data.answerA, 0),
            (data.answerB, 1),
            (data.answerC, 2),
            (data.answerD, 3),
        };

        Shuffle(risposte); // randomizzo la posizione delle risposte

        // Spawna i DraggableItem nel panel destro
        foreach (var (label, index) in risposte)
        {
            GameObject oggettoDraggabile  = Instantiate(draggablePrefab, draggableContainer);
            DraggableItem oD = oggettoDraggabile.GetComponent<DraggableItem>();

            if (oD == null)
            {
                Debug.LogError("[DragDropQuestion] Il prefab non ha DraggableItem.");
                continue;
            }

            oD.Setup(label, index);
        }
    }

    // Utility
    private void ResetUI()
    {
        // Resetta lo slot (rimanda eventuale item rimasto e toglie colore)
        dropSlot.ResetSlot();
        
        // Distruggi i draggable della domanda precedente
        foreach (Transform child in draggableContainer)
            Destroy(child.gameObject);

        tmpPrima.text = string.Empty;
        tmpDopo.text  = string.Empty;
    }

    /// Metodo per randomizzare l'istanzazione delle risposte
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
