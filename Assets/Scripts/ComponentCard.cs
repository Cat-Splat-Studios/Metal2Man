using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentCard : MonoBehaviour
{
    public EPartName partName;

    public Animator anim;
    
    public void HideCard()
    {
        anim.SetTrigger("Hide");
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
