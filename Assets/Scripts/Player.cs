using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //public InputAction input;
    public PlayerInput input;

    public float PlayerSpeed;
    public Transform HoldItemPosition;
    Vector2 inputVector_L; //left stick
    Vector2 inputVector_R; //left stick
    Vector3 MovementVector;
    Vector3 RotationVector;
    float axisDeadZone = 0.15f;
    private bool _isInteracting;
    private bool _isHoldingItem;
    Rigidbody rb;
    public bool IsInteracting
    {
        get => _isInteracting;
        set => _isInteracting = value;
    }

    public bool IsHoldingItem
    {
        get => _isHoldingItem;
        set => _isHoldingItem = value;
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>(); //make sure we have the component
        if (!input)
            Debug.Log("Input component wasnt found - get guud");

        rb = GetComponent<Rigidbody>();
        if (!rb)
            Debug.Log("rb component wasnt found - get guud");

        if (PlayerSpeed <= 0)
        {
            PlayerSpeed = 5.0f;
            Debug.Log("speed wasnt set defaulting to: " + PlayerSpeed);
        }
            
        input.ActivateInput(); //enable the input 
        
        input.onActionTriggered += HandleInputs;
        //ToDo - Need to make a Game Manager to create player instances then add them to the cloud with "Player" + index as the DataKey
        DataManager.ToTheCloud(gameObject.tag,this);
    }

    public void ToggleInteractFlag()
    {
        _isInteracting = !_isInteracting;
    }

    //private void FixedUpdate()
    //{
    //    //HandleMovement(); //handle movement is gunna handle rotation as well - unless we want twin stick mobility
    //    //HandleRotation();
    //}

    private void HandleRotation()
    {
        inputVector_R = input.actions["Look"].ReadValue<Vector2>();

        RotationVector = new Vector3(inputVector_R.x, 0, inputVector_R.y);

        transform.rotation = Quaternion.LookRotation(RotationVector.normalized);
    }

    public void HandleMovement()
    {
        inputVector_L = input.actions["Move"].ReadValue<Vector2>();

        MovementVector = new Vector3(inputVector_L.x, 0, inputVector_L.y);   

       
        //only update rotation if the vector is greater than zero (if there was any input)
        //this is more or less a 'deadzone' on a joystick
        if (MovementVector.magnitude >= axisDeadZone)
        {
            //set our velocity to our input direction
            rb.velocity = MovementVector.normalized * PlayerSpeed;

            transform.rotation = Quaternion.LookRotation(MovementVector.normalized);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    //we can do it in a switch - but movement should be seperate
    private void HandleInputs(InputAction.CallbackContext context)
    {
        Debug.Log($"Context: {context.action.name}");
        switch (context.action.name)
        {
            case "Interact":
                Debug.Log("bang banggg yourrrr dead");
                
                break;
        }
    }
}
