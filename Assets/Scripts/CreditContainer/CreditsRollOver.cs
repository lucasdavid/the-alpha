using UnityEngine;
using System.Collections;

public class CreditsRollOver : MonoBehaviour {

	void Start ()
    {
        string ale, luc, mic, mat, vic, newSection;
        

        ale = "Alex Siegelman\n";
        luc = "Lucas David\n";
        mic = "Michael Resh\n";
        mat = "Matthew Bucher\n";
        vic = "Victor Khou\n";
        newSection = "\n\n\n\n\n";

        GetComponent<GUIText>().text =
            "The Alpha" +
            
            newSection + newSection + newSection + newSection + "Design and Animation\n\n" +
            ale + luc + mic + mat + vic +
            
            newSection + "Enviornment Artist\n\n" +
            mat +
            
            newSection + "Programming\n\n" +
            luc + vic +
            
            newSection + "Programming support\n\n" +
            luc + vic +
            
            newSection + "3D Artists\n\n" +
            ale + mic + mat +

            newSection + "Sound Design\n\n" +
            ale +

            newSection + "External sources\n\n" +
            "Unit Asset Store\n" +
            "FreeSound.org\n" +

            newSection + "Special thanks\n\n" +
            "JesseEtzler0 (youtube channel)\n" +
            "Metalstache (youtube channel)\n" +
            "The wpfmedical project\n" +
            "Who helped testing the game\n" +
            
            newSection + newSection + newSection + "Thank you!"; 
	}
	
}
