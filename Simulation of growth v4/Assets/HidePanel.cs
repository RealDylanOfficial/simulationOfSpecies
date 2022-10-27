using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanel : MonoBehaviour
{
    //Initialises an alternator which causes the submit button to alternate between the procedure and the inverse everytime it is pressed
    private bool alternator;
    
    // Start is called before the first frame update
    void Start()
    {
        //Sets alternator to true
        alternator = true;
        //Hides panel
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hidePanel()
    {
        //decides whether to hide the panel or show it, depending on it's current state
        
        if (alternator == true) //if panel is hidden:
        {
            //show panel
            gameObject.SetActive(true);
            //change the value of the alternator so that next time the button is pressed, the panel is hidden
            alternator = false;
        }
        else //if panel isn't hidden
        {
            //hide panel
            gameObject.SetActive(false);
            //change the value of the alternator so that next time the button is pressed, the panel is shown
            alternator = true;
        }
    }



}
