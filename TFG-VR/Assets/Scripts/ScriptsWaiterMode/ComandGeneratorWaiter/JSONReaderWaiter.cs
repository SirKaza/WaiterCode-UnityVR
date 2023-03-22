using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class JSONReaderWaiter : MonoBehaviour
{

    public TextAsset jsonFileWaiter;
    private OrderSetWaiter orderInJsonWaiter;
    private ArrayList orderJSONWaiter;
    void Start(){
        this.orderInJsonWaiter = JsonUtility.FromJson<OrderSetWaiter>(jsonFileWaiter.text);
        var ordersToShow = orderInJsonWaiter.orders.Where((value) => value.level == "1").ToList();
        this.orderJSONWaiter = new ArrayList(ordersToShow.ToList());
    }


    public ArrayList getOrderInJSON(){

        return this.orderJSONWaiter;
        

    }

    public void unlockLevel(int level){


        var newList = new ArrayList(this.orderInJsonWaiter.orders.Where((value) => value.level == "" + level).ToList());
        this.orderJSONWaiter.AddRange(newList);
        
    }



    public OrderJSONWaiter randomOrder(){

        var random = new System.Random();
        var index = random.Next(this.orderJSONWaiter.Count);
        return this.orderJSONWaiter[index] as OrderJSONWaiter;

    }

    public OrderJSONWaiter orderTutorial(int i)
    {
        return this.orderJSONWaiter[i] as OrderJSONWaiter;

    }









}
