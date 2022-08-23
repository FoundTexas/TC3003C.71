using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionConTecla : MonoBehaviour
{
    public KeyCode tecla;
    public string Trigger1, Trigger2;
    Animator anim;
    bool state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(tecla))
        {
            if (state)
            {
                anim.SetTrigger(Trigger1);
            }
            else if (!state)
            {
                anim.SetTrigger(Trigger2);
            }

            state = !state;
        }
    }
}
