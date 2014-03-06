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

        // Math for determining healthbar color
        float r = (2 - 2 * (pct - 0.1f));
        float g = (2 * (pct - 0.1f));

        if (pct >= 0.5)
            render.material.color = new Color(r, 1, 0, 1); 
        else
            render.material.color = new Color(1, g, 0, 1);
    }

    public void Enable() {
        render.enabled = true;
        GetColor();
    }

    public void Disable() {
        render.enabled = false;
    }
}
