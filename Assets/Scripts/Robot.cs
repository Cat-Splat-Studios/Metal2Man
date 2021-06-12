using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
   public Transform bottomTransform;
   public Transform bodyTransform;
   public Transform armTransform;

   private bool startMove = false;
   
   public void ContrucutRobot(GameObject bottom, GameObject body, GameObject arms)
   {
      Debug.Log("CONSTRUCT");
      bottom.transform.position = bottomTransform.position;
      bottom.transform.parent = bottomTransform;
      body.transform.position = bodyTransform.position;
      body.transform.parent = bodyTransform;
      arms.transform.position = armTransform.position;
      arms.transform.parent = armTransform;

      startMove = true;
   }

   private void Update()
   {
      if (!startMove) return;
      transform.Translate(Vector3.forward * Time.deltaTime);
   }
   
}
