using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentCard : MonoBehaviour
{
    public EPartName partName;

    public Animator anim;

    public void ShowCard()
    {
        anim.SetBool("Show", true);
    }

    public void HideCard()
    {
        anim.SetBool("Show", false);
    }
}
