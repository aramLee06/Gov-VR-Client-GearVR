using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    GUIStyle healthStyle;
    GUIStyle backStyle;
    Combat combat;
    public Camera cam;
    int temp=0;

    void Awake()
    {
        combat = GetComponent<Combat>();
    }

    void OnGUI() 
    {
        InitStyles();
        
        // Draw a Health Bar
            Vector3 pos = cam.WorldToScreenPoint(transform.position);
            
            // draw health bar background
            GUI.color = Color.grey;
            GUI.backgroundColor = Color.grey;
        
            //GUI.Label(new Rect(Screen.width - 120, (10*i)+(10*i), 100, 30), "Player" + (i+1));
       
            GUI.Box(new Rect(pos.x - 26, Screen.height - pos.y - 50,Combat.maxHealth / 2, 15), ".", backStyle);

            // draw health bar amount
            GUI.color = Color.green;
            GUI.backgroundColor = Color.green;
            GUI.Box(new Rect(pos.x - 25, Screen.height - pos.y - 50, combat.health / 2, 15), ".", healthStyle);

        
    }

    void InitStyles()
    {
        
        if (healthStyle == null)
        {
            healthStyle = new GUIStyle(GUI.skin.box);
            healthStyle.normal.background = MakeTex(2, 2, new Color(0f, 1f, 0f, 1.0f));
        }

        if (backStyle == null)
        {
            backStyle = new GUIStyle(GUI.skin.box);
            backStyle.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 1.0f));
        }
    }

    Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
