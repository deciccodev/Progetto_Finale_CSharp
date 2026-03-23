using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Script da assegnare al bottone al posto dello script button
public class ButtonZoomer : Button, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scalaSet = 1.2f;
    [SerializeField] private float _durataPop = 0.8f;

    protected override void Awake()
    {
        base.Awake();
        //Imposto la local scale a 0 per un effetto pop all'avvio
        this.gameObject.transform.localScale = new Vector3(0,0,0);
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(PopIniziale());
    }

    //Riduzione della scala del pulsante all'uscita del puntatore sull'oggetto
    public override void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.transform.localScale = new Vector3(1,1,1);
        base.OnPointerExit(eventData);
    }

    //Aumento della scala del pulsante all'ingresso del puntatore sull'oggetto
    public override void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.transform.localScale = new Vector3(_scalaSet,_scalaSet,_scalaSet);
        

        base.OnPointerEnter(eventData);
    }

    // Animazione del pulsante alla creazione
    private IEnumerator PopIniziale()
    {
        Vector3 initScale = this.gameObject.transform.localScale;
        Vector3 finalScale = new Vector3(1,1,1);
        float tempo = 0f;

        while(tempo < _durataPop)
        {
            // Il Lerp in questo caso prende la scala iniziale come punto di partenza, quella finale di destinazione 
            // e un valore float che va da 0 a 1 dove lo zero indica la scala iniziale e l'uno la finale..
            //Dividendo il tempo iniziale per il totale del tempo desiderato dovrebbe funzionare correttamente

            this.gameObject.transform.localScale = Vector3.Lerp(initScale,finalScale,tempo/_durataPop);
            tempo += Time.deltaTime;

            yield return null;
        }

        this.gameObject.transform.localScale = finalScale;
    }
}

