using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
/*
 * more notes weeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
 * being told to take notes in scripts makes me feel valid for making giant comment blocks in years past
 * "why are we getting an error?"
 * "Maybe you're the error." 
 * maybe you're the problem
 * what is =>? Called a lambda or arrow expression 
 * "a more concise and shorter way of writing out functions" lol the irony here
 * an expression is anything that will return a value
 * ? : -> an if/then abbreviation 
 * FUCK I REMEMBER THIS TOO 
 * it's the ternary conditional operator
 * Events
 * think about all the different interactions we have
 * all these things happening in the game
 * if we define them we can order them however we want
 * Listeners
 * what you use for calling those events
 * when we script listeners in, we are basically tracking events
 * 
 * */


//inherits from CoreFeatures
public class DoorFeatures : CoreFeatures
{
    //door config features: pivot info, open door state, max angle, reverse, speed
    [Header("Door Configs")]
    [SerializeField]
    private Transform doorPivot; //controls pivot
     
    [SerializeField]
    private float maxAngle = 90.0f; //probably less than 90 will work

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float doorSpeed = 1.0f;

    [SerializeField]
    private bool open = false; //default to being shut

    [SerializeField]
    private bool makeKinematicOnOpen = false;

    //interaction features for socket interactor, simple interactor: 
    [Header("Interaction Configs")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractor;


    private void Start()
    {
        socketInteractor?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
            PlayOnStart();
        });
        //"ARE you the socket interactor?" - john, with his pitch rising when he says "interactor"

        socketInteractor?.selectExited.AddListener((s) =>
        {
            PlayOnEnd();
            //when we are done exiting, we don't want to reuse the socket
            socketInteractor.socketActive = featureUsage == FeatureUsage.Once ? false : true;
            //if set to once, it will be set to false, else set to true
        });

        simpleInteractor?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });
    }

    public void OpenDoor()
    {
        if (!open)
        {
            PlayOnStart();
            open = true;
            //StartCoroutine(ProcessMotion());
        }

        //open ? false : true;
        //rowan quit screen-peeking that's cheating 
        //REMOVE THIS
        OpenDoor();
    }

    private IEnumerator ProcessMotion()
    {
        var angle = doorPivot.localEulerAngles.y < 180 ? doorPivot.localEulerAngles.y : doorPivot.localEulerAngles.y - 360;

        angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;

        if (angle <= maxAngle)
        {
            doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            
        }
        else
        {
            open = false;
            var featureRigidBody = GetComponent<Rigidbody>();
            if (featureRigidBody != null && makeKinematicOnOpen) featureRigidBody.isKinematic = true;
            
        }

        yield return null;
    }
}
