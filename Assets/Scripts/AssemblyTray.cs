using UnityEngine;
using Random = UnityEngine.Random;

public class AssemblyTray : MonoBehaviour
{
    public GameObject robotPrefab;
    public Transform buildArea;
    
    public Transform armArea;
    public Transform bodyArea;
    public Transform bottomArea;

    public BuildLibrary buildLibrary;

    public Order[] orderTemplate;

    private Order currentOrder;

    private GameObject armComp = null;
    private GameObject bodyComp = null;
    private GameObject bottomComp = null;

    // TODO check order
    private void Start()
    {
        GenerateOrder();
    }

    public void AddComponent(Component component)
    {
        switch (component.partType)
        {
            case EPartType.Arms:
                if (armComp || component.partName != currentOrder.armPart)
                {
                    BuildError();
                    return;
                }
                GameObject armObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    armArea.position, Quaternion.identity);
                armObj.transform.parent = armArea;
                armComp = armObj;
                break;
            case EPartType.Body:
                if (bodyComp || component.partName != currentOrder.bodyPart)
                {
                    BuildError();
                    return;
                }
                GameObject bodyObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    bodyArea.position, Quaternion.identity);
                bodyObj.transform.parent = bodyArea;
                bodyComp = bodyObj;
                break;
            case EPartType.Bottom:
                if (bottomComp || component.partName != currentOrder.bottomPart)
                {
                    BuildError();
                    return;
                }
                GameObject bottomObj = Instantiate(buildLibrary.GetBuildObject(component.partName),
                    bottomArea.position, Quaternion.identity);
                bottomObj.transform.parent = bottomArea;
                bottomComp = bottomObj;
                break;
        }
        
        CheckComponents();
    }

    public void GenerateOrder()
    {
        int idx = Random.Range(0, orderTemplate.Length);

        currentOrder = orderTemplate[idx];
    }
  
    private void CheckComponents ()
    {
        if (!armComp || !bodyComp || !bodyComp) return;
        BuildRobot();
    }
    
    private void RemoveComponents()
    {
        bodyComp = null;
        armComp = null;
        bottomComp = null;
        
        GenerateOrder();
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

    private void BuildError()
    {
        // TODO something when invalid component gets entered in
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
