using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Access : MonoBehaviour
{
    //defines the text element of the GameObject
    private Text m_Text;
    //Initialises an alternator which causes the accessibility button to alternate between the text reformatting procedure and the inverse everytime it is pressed
    private bool alternator;
    //Initialises an alternator which causes the accessibility button to alternate between the object transformation procedure and the inverse everytime it is pressed
    private bool alternator2;

    // Start is called before the first frame update
    void Start()
    {
        //Sets alternator to true
        alternator = true;
        //Sets alternator2 to true
        alternator2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccessibilityChange()
    {
        //Increases font size and emboldens text when the accessibility button is pressed and back again when pressed again

        if (alternator == true) //if the text isn't in the "accessible" state:
        {
            //accesses the text component of the text box
            m_Text = GetComponent<Text>();
            //increases the font size
            m_Text.fontSize = 50;
            //emboldens the text
            m_Text.fontStyle = FontStyle.Bold;
            //change the value of the alternator so that next time the button is pressed, the text becomes normal
            alternator = false;
        }
        else //if the text is already in the "accessible" state
        {
            //accesses the text component of the text box
            m_Text = GetComponent<Text>();
            //sets the font size back to normal
            m_Text.fontSize = 30;
            //sets the boldness of the text back to normal
            m_Text.fontStyle = FontStyle.Normal;
            //change the value of the alternator so that next time the button is pressed, the text becomes more accessible
            alternator = true;
        }
    }

    public void text9Transform()
    {
        //Reformats some of the text so that it fits on the screen after it's been enlarged

        if (alternator2 == true) //if the text isn't in the "accessible" state:
        {
            //accesses the text component of the text box
            m_Text = GetComponent<Text>();
            //reformats text to be on one line
            m_Text.text = "Number of cycles";
            //moves text to fit on screen
            m_Text.transform.position = new Vector2((m_Text.transform.position.x - 65), (m_Text.transform.position.y - 20));
            //change the value of the alternator so that next time the button is pressed, the text returns to the original position
            alternator2 = false;
        }
        else //if the text is already in the "accessible" state
        {
            //accesses the text component of the text box
            m_Text = GetComponent<Text>();
            //formats text to take up two lines
            m_Text.text = "Number of\r\ncycles";
            //moves text back to its original position
            m_Text.transform.position = new Vector2((m_Text.transform.position.x + 65), (m_Text.transform.position.y + 20));
            //change the value of the alternator so that next time the button is pressed, the text reformats
            alternator2 = true;
        }

    }
    
    public void text17Transform()
        
    {
        //Essentially identical to text9Transform() but for textbox17: "Number of organisms"

        if (alternator2 == true)
        {
            m_Text = GetComponent<Text>();
            m_Text.text = "Number of organisms";
            m_Text.transform.position = new Vector2((m_Text.transform.position.x - 215), (m_Text.transform.position.y - 10));
            alternator2 = false;
        }
        else
        {
            m_Text = GetComponent<Text>();
            m_Text.text = "Number of\r\norganisms";
            m_Text.transform.position = new Vector2((m_Text.transform.position.x + 215), (m_Text.transform.position.y + 10));
            alternator2 = true;
        }
    }
    
    public void textTransform(int type)
    {
        if (type == 0)
        {
            if (alternator2 == true)
            {
                m_Text = GetComponent<Text>();
                m_Text.text = "Movement speed";
                m_Text.transform.position = new Vector2((m_Text.transform.position.x - 215), (m_Text.transform.position.y - 10));
                alternator2 = false;
            }
            else
            {
                m_Text = GetComponent<Text>();
                m_Text.text = "Movement\r\nspeed";
                m_Text.transform.position = new Vector2((m_Text.transform.position.x + 215), (m_Text.transform.position.y + 10));
                alternator2 = true;
            }
        }
        else if (type == 1)
        {
            if (alternator2 == true)
            {
                m_Text = GetComponent<Text>();
                m_Text.text = "Detection range";
                m_Text.transform.position = new Vector2((m_Text.transform.position.x - 215), (m_Text.transform.position.y - 10));
                alternator2 = false;
            }
            else
            {
                m_Text = GetComponent<Text>();
                m_Text.text = "Detection\r\nrange";
                m_Text.transform.position = new Vector2((m_Text.transform.position.x + 215), (m_Text.transform.position.y + 10));
                alternator2 = true;
            }
        }
        
    }
    
}
