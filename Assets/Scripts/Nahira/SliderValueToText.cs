using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValueToText : MonoBehaviour
{
    [Header("Riferimenti UI")]
    public Slider mioSlider;
    public TextMeshProUGUI testoValore;
    [Header("Impostazioni")]
    public string prefisso = "Valore: ";
    public string suffisso = "%";
    void Start()
    {
        if (mioSlider == null) mioSlider = GetComponent<Slider>();
        // imposto il testo iniziale all'avvio
        AggiornaTesto(mioSlider.value);
        // se non esiste già aggiungo un "ascoltatore" che chiama la funzione ogni volta che muovi lo slider
        // mioSlider.onValueChanged.AddListener(delegate { AggiornaTesto(mioSlider.value); });
    }
    public void AggiornaTesto(float valore)
    {
        testoValore.text = prefisso + valore.ToString("F0") + suffisso;
    }
}