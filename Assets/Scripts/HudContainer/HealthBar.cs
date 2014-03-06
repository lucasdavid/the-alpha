using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    Renderer render;
    Mob mob;

    public Color[] colors;



    void Start() {
        mob = transform.parent.GetComponent<Mob>();
        render = GetComponent<Renderer>();
        render.enabled = false;
    }

    void Update() {
        GetColor();
    }

    void GetColor() {
        float pct = ((float)(mob.Health) / (float)(mob.MaxHealth));

        float r = (2 - 2 * (pct - 0.1f));
        float g = (2 * (pct - 0.1f));

        Debug.Log (pct + ":" + r + "," + g);

        if (pct >= 0.5)
            render.material.color = new Color(r, 1, 0, 1); 
        else
            render.material.color = new Color(1, g, 0, 1);
        /*

else if (pct >= 0.8 && pct < 0.9f)
            render.material.color = colors[0];
        else if (pct >= 0.7 && pct < 0.8f)
            render.material.color = colors[0];
        else if (pct >= 0.6 && pct < 0.7f)
            render.material.color = colors[0];
        else if (pct >= 0.5 && pct < 0.6f)
            render.material.color = colors[0];
        else if (pct >= 0.4 && pct < 0.5f)
            render.material.color = colors[0];
        else if (pct >= 0.3 && pct < 0.4f)
            render.material.color = colors[0];
        else if (pct >= 0.2 && pct < 0.3f)
            render.material.color = colors[0];
        else if (pct >= 0.1 && pct < 0.2f)
            render.material.color = colors[0];
*/
    }

    public void Enable() {
        render.enabled = true;
        GetColor();
    }

    public void Disable() {
        render.enabled = false;
    }
}
