using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveRight : MonoBehaviour
{
    //defines a GameObject
    public GameObject Object;
    //Initialises an alternator which causes the accessibility button to alternate between the moveRight procedure and the inverse everytime it is pressed
    private bool alternator;

    // Start is called before the first frame update
    void Start()
    {
        alternator = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveRight()
    {
        
        //Moves objects to the right when the accessibility button is pressed so that it fits on the screen and back again when pressed again

        if (alternator == true) //if the object isn't in the "accessible" state:
        {
            //moves object to the right
            Object.transform.position = new Vector2((Object.transform.position.x + 175), (Object.transform.position.y - 10));
            //change the value of the alternator so that next time the button is pressed, the object returns to its original position
            alternator = false;
        }
        else //if the object is already in the "accessible" state
        {
            //moves object back to its original position
            Object.transform.position = new Vector2((Object.transform.position.x - 175), (Object.transform.position.y + 10));
            //change the value of the alternator so that next time the button is pressed, the object moves to the right
            alternator = true;
        }
        
    }


}
