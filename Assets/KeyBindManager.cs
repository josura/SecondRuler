using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour {

    

    public Dictionary<string, KeyCode> keyBindings { get; set; }

    private static KeyBindManager _instance;
    public static KeyBindManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);   //to keep the singleton between scenes
        bindName = string.Empty;
        keyBindings = new Dictionary<string, KeyCode>();
        keyBindings.Add("UP", KeyCode.W);
        keyBindings.Add("DOWN", KeyCode.S);
        keyBindings.Add("LEFT", KeyCode.A);
        keyBindings.Add("RIGHT", KeyCode.D);
        keyBindings.Add("FIRELASER", KeyCode.Mouse0);
        keyBindings.Add("FIREBULLET", KeyCode.Mouse0);
        keyBindings.Add("FIREROCKET", KeyCode.Mouse1);
    }

    private void Start()
    {
        

    }

    private string bindName;

    public void bindKey(string key, KeyCode bind)
    {
        Dictionary<string, KeyCode> currentBind = keyBindings;
        if ( !currentBind.ContainsValue(bind) || key.Contains("FIRE"))
        {
            currentBind[key]= bind;

        }
        else
        {
            string mykey = currentBind.FirstOrDefault(x => x.Value == bind).Key;

            currentBind[mykey] = KeyCode.None;
        }

        currentBind[key] = bind;
        bindName = string.Empty;
    }

    public void keyBindOnClick(string key)
    {
        this.bindName = key;
    }

    private void OnGUI()
    {
        if (bindName!= string.Empty)
        {
            Event ev = Event.current;

            if (ev.isKey)
            {
                bindKey(bindName, ev.keyCode);
            }
            else if(ev.isMouse)
            {
                Debug.Log(bindName);
                if (bindName.Contains("FIRE"))
                {
                    KeyCode mouseButton;
                    switch (ev.button)
                    {
                        case 0:
                            mouseButton = KeyCode.Mouse0;
                            break;
                        case 1:
                            mouseButton = KeyCode.Mouse1;
                            break;
                        default:
                            mouseButton = KeyCode.Mouse2;
                            break;
                    }
                    bindKey(bindName, mouseButton);
                    
                }
            }

        }
    }
}
