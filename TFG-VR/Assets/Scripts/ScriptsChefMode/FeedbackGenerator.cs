using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackGenerator : MonoBehaviour
{
    private ComandaJSONChef actualOrder;

    private void Start()
    {
    }
    public (string,float) getFeedback(ComandaChef order,float time)
    {

               
        ArrayList ingredients = order.getIngredients();
        ArrayList ingredients_screen = new ArrayList(actualOrder.ingredients);

        string result_str;

        bool result = true;
        int index = 0;
        int exp = 0;
        

        if (actualOrder.type == "basic" || actualOrder.type == "iter")
        {
            if (ingredients.Count > ingredients_screen.Count)
            {
                result_str = "You put too many ingredients!";
            
                return (result_str,0f);
            }
            else if (ingredients.Count < ingredients_screen.Count && ingredients.Count > 0)
            {
                return ("You still have ingredients to put. Don't give up!" ,0);
            }
            foreach (GameObject ingredient in ingredients)
            {

                if (result)
                {

                    if (ingredient.gameObject.name == "Hamburguer")
                    {
                        return ("Oh no ! You have to coock the hamburguer !",0);
                    }
                    result = ingredient.gameObject.name.Equals(ingredients_screen[index]);
                    index++;
                }
                else
                {
                    return ("The ingredients are unordered, or you don't have to put " + ingredient.name + " in the hamburguer", 0);
                }
            }

        }
        else if (actualOrder.type == "conditionalIf")
        {
            var conditionalIngredient = actualOrder.offset[0];
            var signo = actualOrder.offset[1];
            var minimumCardinality = System.Convert.ToInt32(actualOrder.offset[2]);
            var posicionCondicional = System.Convert.ToInt32(actualOrder.offset[3]);
            var isConditionTrue = false;
            var ingCond = conditionalIngredient;

            if (conditionalIngredient == "CoockedHamburguer")
            {
                ingCond = "Hamburguer";
            }
            if (controladorPartidaChef.current.getCardinalityController().getCardinality(ingCond) > minimumCardinality)
            {
                isConditionTrue = true;
            }

            foreach (GameObject ingredient in ingredients)
            {
                if (ingredients.Count > ingredients_screen.Count)
                {
                    result_str = "You put too many ingredients!";
                    return (result_str, 0f);
                }

                else if (ingredients.Count < ingredients_screen.Count-1 && ingredients.Count > 0)
                {
                    return ("You still have ingredients to put. Don't give up!", 0f);

                }
                else if (ingredients.Count == 0)
                {
                    return ("You have to put ingredients!", 0);

                }
                if (result)
                {

                    if (ingredient.gameObject.name == "Hamburguer")
                    {
                        return ("Oh no! You have to coock the hamburguer !", 0);
                    }
                    if (index == posicionCondicional)
                    {

                        if (ingredient.gameObject.name.Equals(ingredients_screen[index]) && !isConditionTrue)
                        {
                            return ("You dont't have enough " + conditionalIngredient + " to put in!" , 0);
                        }

                        else if (ingredient.gameObject.name.Equals(ingredients_screen[index+1]) && isConditionTrue)
                        {
                            return ("You have enough " + conditionalIngredient + " to put in!", 0);
                        }
                        else if(ingredient.gameObject.name.Equals(ingredients_screen[index+1]) && !isConditionTrue)
                        {
                            index ++;
                        }
                        result = ingredient.gameObject.name.Equals(ingredients_screen[index]);
                    }
                    else
                    {
                        result = ingredient.gameObject.name.Equals(ingredients_screen[index]);

                    }
                    index++;
                }
                else
                {
                    return ("The ingredients are unordered, or you don't have to put " + ingredient.name + " in the hamburguer", 0);
                }
            }

        }
        else if (actualOrder.type == "conditionalIfElse")
        {
            var ingredientConditional = actualOrder.offset[0];
            var minimumCardinality = System.Convert.ToInt32(actualOrder.offset[3]);
            var posicionCondicional = System.Convert.ToInt32(actualOrder.offset[4]);
            var isConditionTrue = false;
            var ingCond = ingredientConditional;
            if (ingredientConditional == "CoockedHamburguer")
            {
                ingCond = "Hamburguer";
            }


            if (controladorPartidaChef.current.getCardinalityController().getCardinality(ingCond) >= minimumCardinality)
            {
                isConditionTrue = true;
            }


            foreach (GameObject ingredient in ingredients)
            {
                if (ingredients.Count > ingredients_screen.Count-1)
                {
                    result_str = "You put too many ingredients!";               
                    return (result_str,0f);
                }

                else if (ingredients.Count < ingredients_screen.Count-1 && ingredients.Count > 0)
                {
                    return ("You still have ingredients to put. Don't give up!", 0f);

                }else if(ingredients.Count == 0){
                    return ("You have to put ingredients!",0);

                }
                if (result)
                {

                    if (ingredient.gameObject.name == "Hamburguer")
                    {
                        return ("Oh no! You have to coock the hamburguer !",0);
                    }
                    if (index == posicionCondicional){

                        if (ingredient.gameObject.name.Equals(ingredients_screen[index]) && !isConditionTrue){

                            return ("You dont't have enough " + ingredientConditional + " to put in!", 0);

                        }
                        else if (ingredient.gameObject.name.Equals(ingredients_screen[index+1]) && isConditionTrue)
                        {

                            return ("You have enough " + ingredientConditional + " to put in!", 0);

                        }
                        else if (ingredient.gameObject.name.Equals(ingredients_screen[index + 1]) && !isConditionTrue)
                        {
                            index++;
                        }

                        result = ingredient.gameObject.name.Equals(ingredients_screen[index]);
                        

                    }
                    else{

                        result = ingredient.gameObject.name.Equals(ingredients_screen[index]);

                    }
                    index++;
                }
                else
                {
                    return ("The ingredients are unordered, or you don't have to put " + ingredient.name + " in the hamburguer", 0);
                }
            }

        
        }

       
        if (result && index > 0)
        {
            if (time > 10f)
            {
                exp = 25;
            }
            else if (time < 10f)
            {
                exp = 100;
            }
            return ("Succes!",exp);
        }
        return ("You have to put ingredients!",0);
    }

    public void setActualOrder(ComandaJSONChef order)
    {
        this.actualOrder = order;
    }
}
