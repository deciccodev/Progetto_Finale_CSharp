using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValueToText : MonoBehaviour
{
    [Header("Riferimenti UI")]
    [SerializeField] Slider mioSlider;
    [SerializeField] TextMeshProUGUI testoValore;
    
    [Header("Impostazioni")]
    [SerializeField] string suffisso = "%";
    void Start()
    {
        if (mioSlider == null) mioSlider = GetComponent<Slider>();
        AggiornaTesto(mioSlider.value);

        mioSlider.onValueChanged.AddListener(delegate { AggiornaTesto(mioSlider.value); });
    }
    public void AggiornaTesto(float valore)
    {
        testoValore.text = valore.ToString("F0") + suffisso;
    }
}