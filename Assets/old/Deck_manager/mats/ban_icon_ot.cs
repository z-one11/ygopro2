using UnityEngine;
using System.Collections;
using System.IO;

public class ban_icon_ot : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void show(int i)
    {
        UITexture t = gameObject.GetComponent<UITexture>();
        if (t != null)
        {
            //if (i != 1 && i != 2 && i != 4)
            if (i != 2 && i != 4)  //不显示OCG
            {
                Texture2D icon = (Texture2D)Resources.Load("ban_ot_3");
                t.mainTexture = icon;
            }
            else if (File.Exists("textures/ui/ban_ot_" + i.ToString()))
            {
                t.mainTexture = GameTextureManager.get("ban_ot_" + i.ToString());
            }
            else
            {
                Texture2D icon = (Texture2D)Resources.Load("ban_ot_" + i.ToString());
                t.mainTexture = icon;
            }
        }
        else
        {
            Renderer r = GetComponent<Renderer>();
            //if (i != 1 && i != 2 && i != 4)
            if (i != 2 && i != 4)  //不显示OCG
            {
                Texture2D icon = (Texture2D)Resources.Load("ban_ot_3");
                r.material.mainTexture = icon;
            }
            else if (File.Exists("textures/ui/ban_ot_" + i.ToString()))
            {
                r.material.mainTexture = GameTextureManager.get("ban_ot_" + i.ToString());
            }
            else
            {
                Texture2D icon = (Texture2D)Resources.Load("ban_ot_" + i.ToString());
                r.material.mainTexture = icon;
            }
        }
    }
}
