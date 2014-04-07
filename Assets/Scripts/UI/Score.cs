using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public int score = 0;
    public string text = "Score:";

    void Update() 
    {
        guiText.text = text + " " + score;        
    }
}