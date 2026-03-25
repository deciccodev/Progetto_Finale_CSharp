using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Slider _sliderVol;
    [SerializeField] TextMeshProUGUI testoValore;

   void Awake()
   {
      _sliderVol.onValueChanged.AddListener(SetVolume);
   }

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        if (_sliderVol == null) _sliderVol = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetVolume(float vol)
    {
        _audio.volume = vol * 0.01f;
        testoValore.text = vol.ToString("F0") + " %";
        GameManager.Instance.VolumeToSave(vol);
    }
}
