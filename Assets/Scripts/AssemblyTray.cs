using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyTray : MonoBehaviour
{
    public GameObject robotPrefab;
    public Transform buildArea;
    
    public Transform armArea;
    public Transform bodyArea;
    public Transform bottomArea;

    public BuildLibrary buildLibrary;

    private GameObject armComp = null;
    private GameObject bodyComp = null;
    private GameObject bottomComp = null;


    // TODO check order

    public void AddComponent(Component component)
    {
        Debug.Log("ADD");
        switch (component.partType)
        {
            case EPartType.Arms:
                if (armComp) return;
                GameObject armObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    armArea.position, Quaternion.identity);
                armObj.transform.parent = armArea;
                armComp = armObj;
                break;
            case EPartType.Body:
                if (bodyComp) return;
                GameObject bodyObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    bodyArea.position, Quaternion.identity);
                bodyObj.transform.parent = bodyArea;
                bodyComp = bodyObj;
                break;
            case EPartType.Bottom:
                Debug.Log("BOTTOM");
                if (bottomComp) return;
                Debug.Log("CREATE");
                GameObject bottomObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    bottomArea.position, Quaternion.identity);
                bottomObj.transform.parent = bottomArea;
                bottomComp = bottomObj;
                break;
        }
        
        CheckComponents();
    }

  
    private void CheckComponents ()
    {
        if (!armComp || !bodyComp || !bodyComp) return;
        
        Debug.Log("BUILD");
        BuildRobot();
    }

    private void RemoveComponents()
    {
        bodyComp = null;
        armComp = null;
        bottomComp = null;
    }
    
    private void BuildRobot()
    {
        GameObject robotObj = Instantiate(robotPrefab, buildArea.position, Quaternion.identity);

        Robot robot = robotObj.GetComponent<Robot>();
        
        if (robot)
        {
            robot.ContrucutRobot(bottomComp, bodyComp, armComp);
            
            RemoveComponents();
        }
    }
    
    
    public void TestAddWheelBottom()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.WheelBottom);

        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
    
    public void TestAddStaticBottom()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.StaticBottom);

        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
    
    public void TestAddCubeBody()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.CubeBody);

        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
    
    public void TestAddCannonArms()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.CannonArms);

        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }
    
    public void TestAddMeleeArms()
    {
        GameObject partObj = buildLibrary.GetBuildObject(EPartName.MeleeArms);

        Component comp = partObj.GetComponent<Component>();
        
        AddComponent(comp);
    }

}
