using UnityEngine;
using UnityEngine.InputSystem;

public class ProvaMov : MonoBehaviour
{
    // Inizializziamo direttamente qui per sicurezza
    
    [SerializeField] private InputAction _inputActions;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Vector2 movDir;

    [SerializeField] private Vector3 _posIniz;

    public float speed = 5f;

   void Awake()
   {
      _posIniz = gameObject.transform.position;
      gameObject.SetActive(true);
   }

   private void OnEnable()
    {
        _inputActions.Enable();
        //Subscribe all'azione da intraprendere per l'evento
        PlayerTrigger.EventTriggered += EventTrig;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        //Rimuoviamo l'azione per l'evento
        PlayerTrigger.EventTriggered -= EventTrig;
    }

   void Update()
   {
      movDir = _inputActions.ReadValue<Vector2>();
   }

   private void FixedUpdate()
   {
      rb.linearVelocity = new Vector2(movDir.x* speed, movDir.y * speed );
   }

    //Azione per l'evento, l'argomento id ci serve perchè è richiesto dall'evento
    private void EventTrig(int id)
    {
        transform.position = _posIniz;

        rb.linearVelocity = Vector2.zero;
        Debug.Log(("Posizione player resettata"));

        gameObject.SetActive(false);
        Debug.Log(("Player disattivato"));
    }

}