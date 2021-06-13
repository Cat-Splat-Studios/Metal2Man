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

    public HudComponents hudComponents;
    public ScoreWave scoreWave;

    public Animator anim;
    
    public UnityAction onOrderComplete;

    private GameObject weaponComp = null;
    private GameObject bodyComp = null;
    private GameObject circuitBoard = null;
    
    
    public bool isLeft = false;

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
                if (isLeft)
                {
                    hudComponents.removeCardLeftSection(component.partName);
                }
                else
                {
                    hudComponents.removeCardRightSection(component.partName);
                }
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
                if (isLeft)
                {
                    hudComponents.removeCardLeftSection(component.partName);
                }
                else
                {
                    hudComponents.removeCardRightSection(component.partName);
                }
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
                if (isLeft)
                {
                    hudComponents.removeCardLeftSection(component.partName);
                }
                else
                {
                    hudComponents.removeCardRightSection(component.partName);
                }
                break;
        }
        
        anim.SetTrigger("Build");
        CheckComponents();
    }
    
    public void SetCurrentOrder(Order order)
    {
        currentOrder = order;
        
        if (isLeft)
        {
            hudComponents.populateLeftSecion(currentOrder.components);
        }
        else
        {
            hudComponents.populateRightSection(currentOrder.components);
        }
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
        if (currentOrder.components.Length == 1)
        {
            if (!weaponComp || !circuitBoard) return;
            BuildRobot();
        }
        else
        {
            if (!weaponComp || !bodyComp || !circuitBoard) return;
            BuildRobot();
        }
    }
    
    public void RemoveComponents()
    {
        Destroy(bodyComp);
        bodyComp = null;
        Destroy(weaponComp);
        weaponComp = null;
        Destroy(circuitBoard);
        circuitBoard = null;
    }

    public void ClearComponentCards()
    {
        if (isLeft)
        {
            hudComponents.removeallLeftCards();
        }
        else
        {
            hudComponents.removeallRightCards();
        }
    }
    
    private void BuildRobot() //This is more or less, we have finished our order -> start a new one 
    {
        //  Instantiate(currentOrder.robotPrefab, buildArea.position, Quaternion.identity);
        RemoveComponents();
        scoreWave.ReduceWave(5);
        onOrderComplete.Invoke();
    }

    private void BuildError()
    {
        // TODO something when invalid component gets entered in
    }
    
    
    
    public void TestAddRangeWeapon()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.RangeWeapon);
    
        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
    
    public void TestAddMeleeWeapon()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.MeleeWeapon);
    
        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
    
    public void TestAddBody()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.MobileBody);
    
        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
    
    public void TestAddCircuit()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.CircuitBoard);
    
        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
}
