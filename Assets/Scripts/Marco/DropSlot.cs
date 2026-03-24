using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Script GameObject DropSlot nel PanelSinistroDomanda.
// Il GameObject deve avere: Image (usata per il feedback visivo bordo/colore).

[RequireComponent(typeof(Image))]
public class DropSlot : MonoBehaviour, IDropHandler
{
    [Header("Riferimenti")]
    [SerializeField] private QuizController quizController;

    [Header("Colori feedback")]
    [SerializeField] private Color colorNeutral = Color.gray;
    [SerializeField] private Color colorCorrect = Color.green;
    [SerializeField] private Color colorWrong   = Color.red;

    // Impostato da DragDropQuestion prima di mostrare la domanda
    public int RightAnswer { get; set; }

    private Image _image;
    private DraggableItem _currentItem;   // item attualmente nello slot
    private bool _answered;      // blocca ulteriori drop dopo la risposta

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = colorNeutral;
    }

    // IDropHandler: Metodo chiamato quando un DraggableItem viene rilasciato
    public void OnDrop(PointerEventData eventData)
    {
        if (_answered) return;

        DraggableItem dragged = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (dragged == null) return;

        // Se c'era gia' un item nello slot, rimandalo al panel originale
        if (_currentItem != null && _currentItem != dragged)
            _currentItem.ReturnToOrigin();

        // Aggancia l'item allo slot
        _currentItem = dragged;
        dragged.transform.SetParent(transform, false);

        RectTransform itemRect = dragged.GetComponent<RectTransform>();
        itemRect.anchorMin = new Vector2(0.5f, 0.5f);
        itemRect.anchorMax = new Vector2(0.5f, 0.5f);
        itemRect.anchoredPosition = Vector2.zero;

        // Valida e notifica
        bool isCorrect = (dragged.AnswerIndex == RightAnswer);
        _answered = true;

        _image.color = isCorrect ? colorCorrect : colorWrong;

        if (quizController != null)
            quizController.RispostaData(isCorrect);
        else
            Debug.LogWarning("[DropSlot] QuizController non assegnato in Inspector.");
    }

    // Chiamato da DragDropQuestion per resettare lo slot
    public void ResetSlot()
    {
        if (_currentItem != null)
        {
            _currentItem.ReturnToOrigin();
            _currentItem = null;
        }

        _answered = false;
        _image.color = colorNeutral;
    }
}
