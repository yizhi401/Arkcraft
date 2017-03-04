using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSystem
{
    static public GUISkin skin;

    public delegate void OnPressedDelegate();

    static private int cantidadBotones;

    static private int focusedButton = 0;

    static private bool ignoreAxis;
    static private bool ignoreButton;

    static private List<OnPressedDelegate> delegates = new List<OnPressedDelegate>();

    static public float vAxis = 0.0f;
    static public bool actionButtonDown = false;
    static public bool useKeyboard = true;

    static float WidthRatio;
    static float HeightRatio;
    static float virtualHeight = 480.0f;
    static float virtualWidth = 800.0f;

    static GUIStyle fontStyle;

    static public void ResetFocus()
    {
        focusedButton = 0;
    }

    static public void BeginMenu(string text)
    {
        WidthRatio = Screen.width / virtualWidth;
        HeightRatio = Screen.height / virtualHeight;

        fontStyle = new GUIStyle(GUI.skin.button);
        fontStyle.fontSize = (int)(WidthRatio * 15);

        cantidadBotones = 0;
        delegates.Clear();

        //Screen.showCursor = false;
        GUI.skin = skin;
        GUI.BeginGroup(new Rect(Screen.width / 2 - 200 * WidthRatio, Screen.height / 2 - 200 * HeightRatio, 400 * WidthRatio, 400 * HeightRatio));

        GUI.Box(new Rect(0, 0, 400 * WidthRatio, 400 * HeightRatio), text);
    }

    static public void Button(string text, OnPressedDelegate onPressed)
    {
        delegates.Add(onPressed);

        GUI.SetNextControlName("Boton" + cantidadBotones.ToString());

        if (GUI.Button(new Rect(10 * WidthRatio, 40 * HeightRatio + 30 * 2 * cantidadBotones * HeightRatio, 380 * WidthRatio, 30 * HeightRatio), text, fontStyle))
            onPressed();
        cantidadBotones++;
    }

    static public string TextField(string text)
    {
        GUI.SetNextControlName("Boton" + cantidadBotones.ToString());

        text = GUI.TextField(new Rect(10 * WidthRatio, 40 * HeightRatio + 30 * 2 * cantidadBotones * HeightRatio, 380 * WidthRatio, 30 * HeightRatio), text, fontStyle);

        cantidadBotones++;

        return text;
    }

    static public void LastButton(string text, OnPressedDelegate onPressed)
    {
        delegates.Add(onPressed);

        GUI.SetNextControlName("Boton" + cantidadBotones.ToString());

        if (GUI.Button(new Rect(10 * WidthRatio, 400 * HeightRatio - 70 * HeightRatio, 380 * WidthRatio, 30 * HeightRatio), text, fontStyle))
            onPressed();

        cantidadBotones++;
    }

    static public void EndMenu()
    {
        GUI.EndGroup();

        if (useKeyboard)
        {
            if (cantidadBotones > 0 && GUIUtility.hotControl == 0)
            {
                if (!ignoreAxis && vAxis != 0)
                {
                    if (vAxis > 0)
                        focusedButton--;
                    else if (vAxis < 0)
                        focusedButton++;

                    ignoreAxis = true;
                }

                if (vAxis == 0)
                    ignoreAxis = false;

                if (focusedButton < 0)
                    focusedButton = cantidadBotones - 1;

                if (focusedButton >= cantidadBotones)
                    focusedButton = 0;

                GUI.FocusControl("Boton" + focusedButton.ToString());

                if (!ignoreButton && actionButtonDown)
                {
                    ignoreButton = true;
                    delegates[focusedButton]();
                }
                else if (!actionButtonDown)
                {
                    ignoreButton = false;
                }
            }
        }
    }
}
