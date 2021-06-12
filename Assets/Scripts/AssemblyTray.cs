using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AssemblyTray : MonoBehaviour
{
    public GameObject robotPrefab;
    public Transform buildArea;
    
    public Transform weaponArea;
    public Transform bodyArea;
    public Transform circuitArea;

    public BuildLibrary buildLibrary;
    
    public Order currentOrder;

    public UnityAction onOrderComplete;

    private GameObject weaponComp = null;
    private GameObject bodyComp = null;
    private GameObject circuitBoard = null;
    

    public void AddComponent(Component component)
    {
        switch (component.partType)
        {
            case EPartType.Weapon:
                if (weaponComp || NotInOrder(component.partName))
                {
                    BuildError();
                    return;
                }
                GameObject weaponObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    weaponArea.position, Quaternion.identity);
                weaponObj.transform.parent = weaponArea;
                weaponComp = weaponObj;
                break;
            case EPartType.Body:
                if (bodyComp || NotInOrder(component.partName))
                {
                    BuildError();
                    return;
                }
                GameObject bodyObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    bodyArea.position, Quaternion.identity);
                bodyObj.transform.parent = bodyArea;
                bodyComp = bodyObj;
                break;
            case EPartType.CirtcuitBoard:
                if (circuitBoard)
                {
                    BuildError();
                    return;
                }
                GameObject circuitObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    circuitArea.position, Quaternion.identity);
                circuitObj.transform.parent = circuitArea;
                circuitBoard = circuitObj;
                break;
        }
        
        CheckComponents();
    }
    
    public void SetCurrentOrder(Order order)
    {
        currentOrder = order;
    }

    private bool NotInOrder(EPartName partName)
    {
        foreach (var currentOrderComponent in currentOrder.components)
        {
            if (currentOrderComponent.name == partName)
            {
                return false;
            }
        }

        return true;
    }
  
    private void CheckComponents ()
    {
        if (!weaponComp || !bodyComp || !circuitBoard) return;
        BuildRobot();
    }
    
    private void RemoveComponents()
    {
        Destroy(bodyComp);
        bodyComp = null;
        Destroy(weaponComp);
        weaponComp = null;
        Destroy(circuitBoard);
        circuitBoard = null;
        onOrderComplete.Invoke();
    }
    
    private void BuildRobot() //This is more or less, we have finished our order -> start a new one 
    {
        Instantiate(currentOrder.robotPrefab, buildArea.position, Quaternion.identity);
        RemoveComponents();
    }

    private void BuildError()
    {
        // TODO something when invalid component gets entered in
    }
    
    
    
    // public void TestAddWheelBottom()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.WheelBottom);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
    //
    // public void TestAddStaticBottom()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.StaticBottom);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
    //
    // public void TestAddCubeBody()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.CubeBody);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
    //
    // public void TestAddCannonArms()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.CannonArms);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
    //
    // public void TestAddMeleeArms()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.MeleeArms);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }

}
