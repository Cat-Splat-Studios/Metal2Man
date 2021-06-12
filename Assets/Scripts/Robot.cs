using UnityEngine;

public class Robot : MonoBehaviour
{
   public Transform weaponTransform;
   public Transform bodyTransform;
   
   private void Update()
   {
      transform.Translate(Vector3.forward * Time.deltaTime);
   }
}
