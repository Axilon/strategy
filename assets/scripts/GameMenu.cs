
using UnityEngine;

public class GameMenu : MonoBehaviour {
    public GUISkin mainUI;
    public int numDepth = 1;
    public bool pause  = false;


    // Update is called once per frame
    private void Update()
    {
        
    }


    void onGUI () {
        if (pause)
        {
            GUI.depth = numDepth;
            GUI.skin = mainUI;

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", GUI.skin.GetStyle("BackGround"));

            GUI.BeginGroup(new Rect((Screen.width - 150) / 2, (Screen.height - 150) / 2, 150, 150));

            GUI.Label(new Rect(25, 30, 100, 30), "Пауза", GUI.skin.label);
            if(GUI.Button(new Rect(0, 50, 150, 30), "Продолжить"))
            {
                pause = false;
            }
            if(GUI.Button(new Rect(0, 90, 15, 30), "Выход"))
             {
                Application.Quit();
                    Debug.Log("exit");
                
            }
            GUI.EndGroup();
        }
	}
}
