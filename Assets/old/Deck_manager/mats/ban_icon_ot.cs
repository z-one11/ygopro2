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
            if (i == 2 || i == 4)  //[1: OCG]、[2: TCG]、[3: OCG&TCG]、[4: Anime]
            {
                if (File.Exists("texture/ui/ban_ot_" + i.ToString() + ".png"))
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
                Texture2D icon = (Texture2D)Resources.Load("ban_ot_3");
                t.mainTexture = icon;
            }
        }
        else
        {
            Renderer r = GetComponent<Renderer>();
            if (i == 2 || i == 4)  //[1: OCG]、[2: TCG]、[3: OCG&TCG]、[4: Anime]
            {
                if (File.Exists("texture/ui/ban_ot_" + i.ToString() + ".png"))
                {
                    r.material.mainTexture = GameTextureManager.get("ban_ot_" + i.ToString());
                }
                else
                {
                    Texture2D icon = (Texture2D)Resources.Load("ban_ot_" + i.ToString());
                    r.material.mainTexture = icon;
                }
            }
            else
            {
                Texture2D icon = (Texture2D)Resources.Load("ban_ot_3");
                r.material.mainTexture = icon;
            }
        }
    }
}
