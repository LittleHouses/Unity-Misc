using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateCounter : MonoBehaviour {

    [Tooltip("Ideal Framerate.")]
    [SerializeField] [Min(0.0f)] int targetFramerate = 90;

    [Tooltip("Size of text in GUI overlay.")]
    [SerializeField] [Min(0.0f)] int fontSize = 25;

    [SerializeField] bool displayInGame = true;

    [SerializeField] bool displayInConsole = false;

    [Tooltip("Time (Sec) between updating the GUI.")]
    [SerializeField] [Min(0.0f)] float sampleInterval = 0.25f;

    private Color32 orangeColor = new Color32(255, 165, 0, 255);

    private bool shouldSampleFrame = true;

    private float framerate;

    public float Framerate { get { return framerate; } private set { } }

    public void DisplayInGame(bool bl)
    {
        displayInGame = bl;
    }

    public void DisplayInConsole(bool bl)
    {
        displayInConsole = bl;
    }

    public static int SampleFramerate()
    {
        return (int)(1.0f / Time.smoothDeltaTime);
    }

    public static string ToHexString(Color32 color)
    {
        string red = color.r < 16 ? "0" + Convert.ToString(color.r, 16) : Convert.ToString(color.r, 16);
        string green = color.g < 16 ? "0" + Convert.ToString(color.g, 16) : Convert.ToString(color.g, 16);
        string blue = color.b < 16 ? "0" + Convert.ToString(color.b, 16) : Convert.ToString(color.b, 16);
        string alpha = color.a < 16 ? "0" + Convert.ToString(color.a, 16) : Convert.ToString(color.a, 16);

        return red + green + blue + alpha;
    }

    // We update the framerate with a new value only after the sampleInterval has passed. The FramerateSampleClock coroutine updates the shouldSampleFrame boolean whenever that is the case. 
    private IEnumerator FramerateSampleClock()
    {
        float timer = 0.0f;

        while (true)
        {
            if (timer >= sampleInterval)
            {
                shouldSampleFrame = true;
                timer = 0.0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
            yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(FramerateSampleClock());
    }

    private void OnGUI()
    {
        // Has enough time passed since the last sample.
        if(shouldSampleFrame)
        {
            framerate = SampleFramerate();
            shouldSampleFrame = false;

            if (displayInConsole) Debug.Log(framerate);
        }

        if(displayInGame)
        { 
            Color32 framerateColor;

            //We lerp between colors indicating the quality of our framerate. 
            if (framerate >= targetFramerate)
            {
                framerateColor = Color.green;
            }
            else if (framerate >= targetFramerate / 2)
            {
                float alpha = 2 * (1 - (framerate / targetFramerate));
                framerateColor = Color32.Lerp(Color.green, orangeColor, alpha);
            }
            else
            {
                float alpha = 1 - 2 * (framerate / targetFramerate);
                framerateColor = Color32.Lerp(orangeColor, Color.red, alpha);
            }

            // We need to convert our color to a Hexidecimal string in order to concatenate.  
            string hexColor = ToHexString(framerateColor);

            GUILayout.Label($"<size={fontSize}><color=#{hexColor}>{framerate}</color></size>");
        }

        shouldSampleFrame = false;            
    }
}

