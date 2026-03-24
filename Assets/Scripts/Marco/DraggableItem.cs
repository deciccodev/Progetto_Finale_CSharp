using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

// Script prefab DraggableItem.
// Il prefab deve avere: Image, CanvasGroup, TextMeshProUGUI (figlio).
// Il Canvas root deve avere il componente GraphicRaycaster.

[RequireComponent(typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Stato pubblico (letto da DropSlot)
    public int AnswerIndex { get; private set; }   // 0=A 1=B 2=C 3=D

    // Riferimenti
    private CanvasGroup _canvasGroup;
    private RectTransform  _rectTransform;
    private Canvas _canvas;

    private Transform _originalParent;
    private Vector2 _originalPosition;
    private int _originalSiblingIndex;

    // Init chiamato da DragDropQuestion
    public void Setup(string labelText, int answerIndex)
    {
        AnswerIndex = answerIndex;
        GetComponentInChildren<TextMeshProUGUI>().text = labelText;
    }

    private void Awake()
    {
        _canvasGroup   = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _canvas        = GetComponentInParent<Canvas>();
    }

    // Metodo interfaccia IBeginDragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Salva posizione di partenza per poter tornare indietro
        _originalParent = transform.parent;
        _originalPosition = _rectTransform.anchoredPosition;
        _originalSiblingIndex = transform.GetSiblingIndex();

        // Sposta sul Canvas root cosi' non viene clippato dal panel
        transform.SetParent(_canvas.transform, true);
        transform.SetAsLastSibling();

        // Disabilita il raycast sull'oggetto draggable cosi' l'EventSystem puo' vedere lo slot sotto di esso
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.8f;
    }

    // Metodo interfaccia IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    // Metodo interfaccia IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;

        // Se OnDrop dello slot NON e' stato chiamato (drop in zona vuota) torna al panel originale nella posizione di partenza
        if (transform.parent == _canvas.transform)
            ReturnToOrigin();
    }

    // Chiamato da DropSlot se il drop viene rifiutato o resettato
    public void ReturnToOrigin()
    {
        transform.SetParent(_originalParent, true);
        transform.SetSiblingIndex(_originalSiblingIndex);
        _rectTransform.anchoredPosition = _originalPosition;
    }
}
