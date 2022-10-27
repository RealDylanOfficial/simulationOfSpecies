using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelColour : MonoBehaviour
{
    
    //defines the image element of the GameObject
    private Image Object;
    //Initialises an alternator which causes the accessibility button to alternate between the procedure and the inverse everytime it is pressed
    private bool alternator;
    //defines the cream colour using its rgb values, followed by its opacity
    private Color32 cream = new Color32(255, 253, 208, 255);
    //defines the grey colour using its rgb values, followed by its opacity
    private Color32 grey = new Color32(255, 255, 255, 255);
    
    // Start is called before the first frame update
    void Start()
    {
        //Sets alternator to true
        alternator = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ColourChange()
    {
        //Changes the colour of the panel to cream when the accessibility button is pressed, and then back again to grey

        if (alternator == true) //if the panel isn't in the "accessible" state:
        {
            //accesses image component of the GameObject
            Object = GetComponent<Image>();
            //changes the colour of the panel to cream
            Object.color = cream;
            //change the value of the alternator so that next time the button is pressed, the panel changes to grey
            alternator = false;
        }
        else //if the panel is already "accessible":
        {
            //accesses image component of the GameObject
            Object = GetComponent<Image>();
            //changes the colour of the panel to grey
            Object.color = grey;
            //change the value of the alternator so that next time the button is pressed, the panel changes to cream
            alternator = true;
        }
    }
    
}
