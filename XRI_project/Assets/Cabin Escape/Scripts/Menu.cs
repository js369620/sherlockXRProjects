using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //Public variable for Menu to quick ref
    //Creating PROPERTY VALUES -> encapsulation vars
    //get accessor (read)
    //set accessor (write)
    //if you want more UI stuff than what's here it should be declared here

    [field: SerializeField]
    public Button ResumeButton {  get; set; }

    [field: SerializeField]
    public Button RestartButton { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI SolvedText { get; set; }

    

}
