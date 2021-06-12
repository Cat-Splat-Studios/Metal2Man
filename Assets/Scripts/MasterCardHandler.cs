using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MasterCardHandler : MonoBehaviour
{
    //this is our build order card - it holds 3 checks boxes (images) to see if the player has retreived a component
    //Once a player brings 1 of 3 desired components back to the card, the image of that component
    //should show a check mark to indicate that component has been fetched
    //when 3 are done we advance to our next build

    //big picture of final product

    public AssemblyTray assemblyTray;

    public Image robotImage;
    public Image WeaponImage;
    public Image BodyImage;
    public Image CircuitboardImage;

    public BuildLibrary buildLibrary;
    
    public Order[] orderTemplates;

    public Slider timeSlider;

    public Animator anim;

    private float startTime;
    private float orderTime;
    private bool onTheClock = false;
    
    public float yOffset = 0.25f;
    private void Start()
    {
        assemblyTray.onOrderComplete = () =>
        {
            StartCoroutine(startNextOrder(3.0f));
        };
        
        StartCoroutine(startNextOrder(3.0f));
    }

    private void Update()
    {
        if (!onTheClock) return;
        
        orderTime -= Time.deltaTime;
        DisplaySlider();

        if (orderTime <= 0.0f)
        {
            // order incomplete
            // Remove all info from assembly
            // generate new order
            assemblyTray.RemoveComponents();
            assemblyTray.ClearComponentCards();
            StartCoroutine(startNextOrder(3.0f));
        }
    }

    public void GenerateOrder()
    {
        int index = Random.Range(0, orderTemplates.Length);
        Order currentOrder = orderTemplates[index];
        PopulateCard(currentOrder);
        assemblyTray.SetCurrentOrder(currentOrder);
    
        orderTime = currentOrder.buildTime;
        startTime = currentOrder.buildTime;
        onTheClock = true;
    }

    private void PopulateCard(Order order)
    {
        BodyImage.gameObject.SetActive(false);
        WeaponImage.gameObject.SetActive(false);
        
        robotImage.sprite = order.robotImage;
        CircuitboardImage.sprite = buildLibrary.GetBuildImage(EPartName.CircuitBoard);
        foreach (var orderComponent in order.components)
        {
            switch (orderComponent.type)
            {
                case EPartType.Body:
                    BodyImage.gameObject.SetActive(true);
                    BodyImage.sprite = buildLibrary.GetBuildImage(orderComponent.name);
                    break;
                case EPartType.Weapon:
                    WeaponImage.gameObject.SetActive(true);
                    WeaponImage.sprite = buildLibrary.GetBuildImage(orderComponent.name);
                    break;
            }
        }
    }

    private IEnumerator startNextOrder(float seconds)
    {
        onTheClock = false;
        RemoveCard();
        yield return new WaitForSeconds(seconds);
        ShowCard();
        GenerateOrder();
    }

    public void ShowCard()
    {
        anim.SetBool("Show", true);
    }

    public void RemoveCard()
    {
        anim.SetBool("Show", false);
    }
    
    private void DisplaySlider()
    {
        float percent = orderTime / startTime;

        timeSlider.value = percent;
    }
}
