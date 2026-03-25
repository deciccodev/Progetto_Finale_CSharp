using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Slider _sliderVol;

   void Awake()
   {
      _sliderVol.onValueChanged.AddListener(SetVolume);
   }

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetVolume(float vol)
    {
        _audio.volume = vol * 0.01f;
        GameManager.Instance.VolumeToSave(vol);
    }
}
