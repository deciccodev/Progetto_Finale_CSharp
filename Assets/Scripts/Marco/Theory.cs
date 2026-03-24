using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Theory : MonoBehaviour
{
    [Header("Tab Menu")]
    [SerializeField] private List<Button> tabs;

    [Header("Scroll View")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform contentContainer;
    [SerializeField] private GameObject textPrefab;

    [Header("Dati")]
    [SerializeField] private string jsonFileName = "Topics.json"; // Per ora lasciamo visibile il nome stringa per debug
    [SerializeField] private Sprite spriteTabAttiva;
    [SerializeField] private Sprite spriteTabDisattiva;
    [SerializeField] private Color coloreTestoAttivo;
    [SerializeField] private Color coloreTestoDisattivato;

    // Strutture dati per il parsing del JSON
    [System.Serializable]
    private class Topic
    {
        public string title;
        public string content;
    }

    [System.Serializable]
    private class TopicList
    {
        public List<Topic> topics;
    }

    // Stato interno
    private TopicList _data;
    private int _currentIndex = -1;
    private bool _ready = false;

    // Lifecycle
    private void Start()
    {
        StartCoroutine(LoadJSON());
    }

    // Caricamento JSON da StreamingAssets
    private IEnumerator LoadJSON()
    {
        string path = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"[Theory] File non trovato: {path}");
            yield break;
        }

        string json = File.ReadAllText(path);
        ParseAndInit(json);
        yield return null;
    }

    private void ParseAndInit(string json)
    {
        _data = JsonUtility.FromJson<TopicList>(json);

        if (_data == null || _data.topics == null || _data.topics.Count == 0)
        {
            Debug.LogError("[Theory] Parsing JSON fallito o lista topics vuota.");
            return;
        }

        _ready = true;
        RegisterTabCallbacks();
        ShowTopic(0);
    }

    // Registrazione callbacks tab
    private void RegisterTabCallbacks()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            int index = i; // closure locale necessaria
            tabs[i].onClick.AddListener(() => ShowTopic(index));
        }
    }

    // Logica principale
    public void ShowTopic(int index)
    {
        if (!_ready)
        {
            Debug.LogWarning("[Theory] Dati non ancora caricati.");
            return;
        }

        if (index < 0 || index >= _data.topics.Count)
        {
            Debug.LogWarning($"[Theory] Indice topic non valido: {index}");
            return;
        }

        if (index == _currentIndex) return;
        _currentIndex = index;

        ClearContent();
        SpawnText(_data.topics[index].content);
        ScrollToTop();
        UpdateTabVisuals(index);
    }

    // Utility
    private void ClearContent()
    {
        foreach (Transform child in contentContainer)
            Destroy(child.gameObject);
    }

    private void SpawnText(string text)
    {
        if (textPrefab == null)
        {
            Debug.LogError("[Theory] textPrefab non assegnato in Inspector.");
            return;
        }

        GameObject instance = Instantiate(textPrefab, contentContainer);
        TextMeshProUGUI tmp = instance.GetComponent<TextMeshProUGUI>();

        if (tmp == null)
        {
            Debug.LogError("[Theory] Il prefab non contiene un componente TextMeshProUGUI.");
            return;
        }

        tmp.text = text;
    }

    private void ScrollToTop()
    {
        StartCoroutine(ResetScrollNextFrame());
    }

    private IEnumerator ResetScrollNextFrame()
    {
        // Attende un frame cosi' ContentSizeFitter aggiorna le dimensioni prima di reimpostare la posizione dello scroll, non volevo forzare il refresh del canvas
        yield return null;
        scrollRect.verticalNormalizedPosition = 1f;
    }

    private void UpdateTabVisuals(int activeIndex)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            bool isActive = (i == activeIndex);

            tabs[i].GetComponent<Image>().sprite = isActive ? spriteTabAttiva : spriteTabDisattiva;

            TextMeshProUGUI label = tabs[i].GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
                label.color = isActive ? coloreTestoAttivo : coloreTestoDisattivato;

            tabs[i].interactable = !isActive;
        }
    }
}