using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] float distanza = 10f;   // quanto si sposta sull'asse X
    [SerializeField] float velocita = 2f;     // velocità del movimento

    private Vector3 posizioneIniziale;

    void Start()
    {
        posizioneIniziale = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * velocita) * distanza;
        transform.position = new Vector3(posizioneIniziale.x + offset, posizioneIniziale.y, posizioneIniziale.z);
    }

}
