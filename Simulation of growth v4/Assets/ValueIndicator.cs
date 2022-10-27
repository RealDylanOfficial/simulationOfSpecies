using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueIndicator : MonoBehaviour
{
    //Initialises slider components
    private Slider slider;
    private Slider slider2;
    private Slider slider3;
    //Initialises text component
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        //Gets the text component of the value indicator that the script is attached to
        text = GetComponent<Text>();
        //Getting the slider components of the various sliders
        slider = GameObject.Find("Hill height slider").GetComponent<Slider>();
        slider2 = GameObject.Find("Hill density slider").GetComponent<Slider>();
        slider3 = GameObject.Find("Water level silder").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HillHeightIndicator()
    {
        //Sets the text displayed on the screen to the value of the slider
        text.text = slider.value.ToString();
    }

    public void HillDensityIndicator()
    {
        text.text = slider2.value.ToString();
    }

    public void WaterLevelIndicator()
    {
        text.text = slider3.value.ToString();
    }
}
