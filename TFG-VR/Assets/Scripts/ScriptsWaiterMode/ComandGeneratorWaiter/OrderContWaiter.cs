using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderContWaiter:MonoBehaviour
{
    private OrderJSONWaiter actualOrder;
    private JSONReaderWaiter ordersJson;
    public OrderContWaiter()
    {
        ordersJson = GameObject.Find("ControllerPartidaWaiter").GetComponent<JSONReaderWaiter>();      
   }

    public void generateOrder()
    {
        actualOrder = ordersJson.randomOrder();
    }

    public void unlocklevels(int level){
        ordersJson.unlockLevel(level); 
    }

    public OrderJSONWaiter getActualOrder()
    {
        return this.actualOrder;
    }

    public void setOrder(OrderJSONWaiter order)
    {
        actualOrder  = order;
    }

    
}
