using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class PlayPauseButton : MonoBehaviour
{
    bool alternator;
    Image image;

    public Sprite pause;
    public Sprite play;

    public Text speedText;

    public ContainInitialVariables Container;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        alternator = true;
        speedText.text = "x1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSprite()
    {
        if (alternator == true)
        {
            image.sprite = play;
            transform.eulerAngles = new Vector3(0, 0, 270);
            transform.localScale = new Vector3((float)0.6, (float)0.8, 1);

            Container.paused = true;

            alternator = false;
        }
        else
        {
            image.sprite = pause;
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);

            Container.paused = false;

            alternator = true;
            
        }
    }

    public void fastForward()
    {
        if ((Container.currentSpeed <= 4) && (Container.currentSpeed > 1))
        {
            Container.currentSpeed /= 2;

            speedText.text = "x" + (1 / ((float)Container.currentSpeed / 4)).ToString();
        }

        
    }

    public void slowDown()
    {
        if ((Container.currentSpeed < 4) && (Container.currentSpeed >= 1))
        {
            Container.currentSpeed *= 2;

            speedText.text = "x" + (1/((float)Container.currentSpeed/4)).ToString();
            
        }
    }
}
