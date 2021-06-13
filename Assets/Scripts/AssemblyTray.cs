using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AssemblyTray : BaseStation
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

    public void AddComponent(Component component, Player player)
    {
        switch (component.partType)
        {
            case EPartType.Weapon:
                if (weaponComp || NotInOrder(component.partName))
                {
                    BuildError();
                    return;
                }
                
                component.transform.position = weaponArea.position;
                component.transform.rotation = weaponArea.rotation;
                component.transform.parent = weaponArea;
                weaponComp = component.gameObject;
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
                component.transform.position = bodyArea.position;
                component.transform.rotation = bodyArea.rotation;
                component.transform.parent = bodyArea;
                bodyComp = component.gameObject;
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
                component.transform.position = circuitArea.position;
                component.transform.rotation = circuitArea.rotation;
                component.transform.parent = circuitArea;
                circuitBoard = component.gameObject;
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

        player.IsHoldingItem = false;
        anim.SetTrigger("Build");
        CheckComponents();
    }

    protected override void StationAction()
    {
        if (_currentPlayer.IsHoldingItem)
        {
            Debug.Log("Holding item");
            Component comp = _currentPlayer.HoldItemPosition.GetChild(0).GetComponent<Component>();

            if (comp)
            {
                Debug.Log("Have Component item");
                AddComponent(comp, _currentPlayer);
            }
        }
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
    
    
    
    // public void TestAddRangeWeapon()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.RangeWeapon);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
    //
    // public void TestAddMeleeWeapon()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.MeleeWeapon);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
    //
    // public void TestAddBody()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.MobileBody);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
    //
    // public void TestAddCircuit()
    // {
    //     GameObject partObj = buildLibrary.GetBuildObject(EPartName.CircuitBoard);
    //
    //     Component comp = partObj.GetComponent<Component>();
    //     
    //     AddComponent(comp);
    // }
}
