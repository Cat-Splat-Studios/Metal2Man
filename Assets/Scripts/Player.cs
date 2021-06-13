using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //public InputAction input;
    public PlayerInput input;

    public float PlayerSpeed;
    public float DashSpeed = 50;

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

    [SerializeField] PlayerController controller;
    [SerializeField] int playerIndex = 0;

    [SerializeField] bool bIsTest = false;
    [SerializeField] PlayerController controllerPrefab;

    private void Awake()
    {
        //input = GetComponent<PlayerInput>(); //make sure we have the component
        //if (!input)
        //    Debug.Log("Input component wasnt found - get guud");

        rb = GetComponent<Rigidbody>();
        if (!rb)
            Debug.Log("rb component wasnt found - get guud");

        if (PlayerSpeed <= 0)
        {
            PlayerSpeed = 5.0f;
            Debug.Log("speed wasnt set defaulting to: " + PlayerSpeed);
        }
        if (DashSpeed <= 0)
        {
            DashSpeed = 5.0f;
            Debug.Log("dash speed wasnt set defaulting to: " + DashSpeed);
        }

        //ToDo - Need to make a Game Manager to create player instances then add them to the cloud with "Player" + index as the DataKey
        DataManager.ToTheCloud(gameObject.tag,this);
    }

    private void Possess()
    {
        input = controller.PlayerInputs;

        if (input == null)
            input = controller.GetComponent<PlayerInput>();

        if (PlayerManager.GetIsSplitKeyboard == true && playerIndex == 1)
        {
            input.actions["Move1"].performed += UpdateMovementInput;
            input.actions["Move1"].canceled += UpdateMovementInput;

        }
        else
        {
            input.actions["Move"].performed += UpdateMovementInput;
            input.actions["Move"].canceled += UpdateMovementInput;

           // input.onActionTriggered += HandleInputs;
        }

        input.onActionTriggered += HandleInputs;

        input.ActivateInput(); //enable the input 
    }

    private void Start()
    {
        if (bIsTest)
        {
            controller = Instantiate(controllerPrefab) as PlayerController;
            Possess();
            return;
        }

        if (PlayerManager.GetIsSplitKeyboard == true && playerIndex == 1)
        {
            controller = PlayerManager.GetController(0);
            if (controller != null)
            {
                Possess();
                return;
            }
        }

        if (PlayerManager.IsPlayerInRange(playerIndex))
        {
            controller = PlayerManager.GetController(playerIndex);
            if (controller != null)
            {
                Possess();
            }
        }
        else
        {
            Debug.Log("Controller not found, should be disabling this");
            gameObject.SetActive(false);
        }


        //no idea what fookin happened to the rb but mass is 50 and i cant find this prefab in proj??? sooo here we go
        rb.mass = 1;
        DashSpeed = 35;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        //HandleRotation();
    }

    private void UpdateMovementInput(InputAction.CallbackContext context)
    {
        inputVector_L = context.ReadValue<Vector2>();
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
    private void PlayerDash()
    {
        Debug.Log("dash boi");
        rb.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
    }
    //we can do it in a switch - but movement should be seperate
    private void HandleInputs(InputAction.CallbackContext context)
    {
        if (PlayerManager.GetIsSplitKeyboard && playerIndex == 1)
        {
            if (context.action.name == "Interact1")
            {
                ToggleInteractFlag();
                return;
            }

            if (context.action.name == "Dash1")
            {
                if (context.action.phase == InputActionPhase.Performed)
                PlayerDash();
                return;
            }
        }

        switch (context.action.name)
        {
            case "Interact":                
                ToggleInteractFlag();
                break;
            case "Dash":
                if (context.action.phase == InputActionPhase.Performed)
                    PlayerDash();
                break;
        }
    }
}
