using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenomeSelector : MonoBehaviour
{
    //initialises the integer to represent the genome the user has switched to
    private int CurrentGenome;
    //initialises a dropdown component
    Dropdown dropdown;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method for updating CurrentGenome when a new genome is selected
    public void genomeSelect()
    {
        //Gets the dropdown component of the genome select dropdown
        dropdown = GetComponent<Dropdown>();
        //Sets CurrentGenome to the value of the dropdown
        CurrentGenome = dropdown.value;
    }
    //method for getting the CurrentGenome
    public int GetCurrentGenome()
    {
        return CurrentGenome;
    }



}
