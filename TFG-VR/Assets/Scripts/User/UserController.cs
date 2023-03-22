using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    private UserJSON currentUser;
    
    //Basic methods, basic variables
    public UserJSON getCurrentUser(){
        return currentUser;
    }

    public void setCurrentUser(UserJSON user){
        this.currentUser = user;
    }

    public DataLevel getDataLevel(){
        return this.currentUser.dataLevel;
    }

    public AchievementsJSON getAchievements(){
        return this.currentUser.achievements;
    }

    public string getName(){
        return this.currentUser.name;
    }

    public string getPassword(){
        return this.currentUser.password;    
    }

    public int getCurrentDay(){
        return this.currentUser.currentDay;
    }

    public bool isTutorialChefCompleted(){
        return this.currentUser.tutorialChefCompleted;
    }
    public bool isTutorialWaiterCompleted()
    {
        return this.currentUser.tutorialWaiterCompleted;
    }
    public void tutorialChefCompleted(){
        this.currentUser.tutorialChefCompleted = true;
    }
    public void tutorialWaiterCompleted()
    {
        this.currentUser.tutorialWaiterCompleted = true;
    }

    public void setCurrentDay(int day){
        this.currentUser.currentDay = day;
    }


    public int getTotalExp(){
        return this.currentUser.dataLevel.totalExp;
    }
    public void setTotalExp(int exp){
        this.currentUser.dataLevel.totalExp = exp;
    }

    public int getChefExp()
    {
        return this.currentUser.dataLevel.chefExp;
    }
    public void setChefExp(int exp)
    {
        this.currentUser.dataLevel.chefExp = exp;
    }
    public int getWaiterExp()
    {
        return this.currentUser.dataLevel.waiterExp;
    }
    public void setwaiterExp(int exp)
    {
        this.currentUser.dataLevel.waiterExp = exp;
    }

    public void setChefLevel(int level)
    {
        this.currentUser.dataLevel.chefLevel = level;
    }
    public int getWaiterLevel()
    {
        return this.currentUser.dataLevel.waiterLevel;
    }
    public void setwaiterLevel(int level)
    {
        this.currentUser.dataLevel.waiterLevel = level;
    }

    //Gettters total orders by type
    public int getNumOrdersChef()
    {
        return this.currentUser.dataLevel.ordersChef;
    }
    public int getNumBasicOrdersChef(){
        return this.currentUser.dataLevel.basicOrdersChef;
    }
    public int getNumConditionalOrdersChef()
    {
        return this.currentUser.dataLevel.conditionalIfOrdersChef;
    }
    public int getNumConditionalIfOrdersChef(){
        return this.currentUser.dataLevel.conditionalIfOrdersChef;
    }
    public int getNumConditionalIfElseOrdersChef()
    {
        return this.currentUser.dataLevel.conditionalIfElseOrdersChef;
    }
    public int getNumIterativeOrdersChef(){
        return this.currentUser.dataLevel.iterativeOrdersChef;
    }

    //Settters total orders by type  - Chef
    public void setNumOrdersChef(int num)
    {
        this.currentUser.dataLevel.ordersChef = num;
    }
    public void setNumBasicOrdersChef(int num){
        this.currentUser.dataLevel.basicOrdersChef = num;
    }
    public void setNumConditionalOrdersChef(int num)
    {
        this.currentUser.dataLevel.conditionalOrdersChef = num;
    }
    public void setNumConditionalIfOrdersChef(int num){
        this.currentUser.dataLevel.conditionalIfOrdersChef = num;
    }
    public void setNumConditionalIfElseOrdersChef(int num)
    {
        this.currentUser.dataLevel.conditionalIfElseOrdersChef = num;
    }
    public void setNumIterativeOrdersChef(int num){
        this.currentUser.dataLevel.iterativeOrdersChef = num;
    }

    //Getters ALL orders achievements - Chef
    public bool getFirstOrderChefAchievement(){
        return this.currentUser.achievements.firstOrderChef;
    }
    public bool getTenOrdersChefAchievement()
    {
        return this.currentUser.achievements.tenOrdersChef;
    }
    public bool getTwentyfiveOrdersChefAchievement(){
        return this.currentUser.achievements.twentyfiveOrdersChef;
    }
    public bool getFiftyOrdersChefAchievement()
    {
        return this.currentUser.achievements.fiftyOrdersChef;
    }
    public bool getHundredOrdersChefAchievement()
    {
        return this.currentUser.achievements.hundredOrdersChef;
    }

    //Getters BASIC orders achievements - Chef
    public bool getFirstBasicOrderChefAchievement()
    {
        return this.currentUser.achievements.firstBasicOrderChef;
    }
    public bool getTenBasicOrdersChefAchievement()
    {
        return this.currentUser.achievements.tenBasicOrdersChef;
    }
    public bool getTwentyfiveBasicOrdersChefAchievement()
    {
        return this.currentUser.achievements.twentyfiveBasicOrdersChef;
    }
    //Getters ITERATIVE orders achievements - Chef
    public bool getFirstIterativeOrderChefAchievement(){
        return this.currentUser.achievements.firstIterativeOrderChef;
    }
    public bool getTenIterativeOrdersChefAchievement(){
        return this.currentUser.achievements.tenIterativeOrdersChef;
    }
    public bool getThirtyIterativeOrdersChefAchievement(){
        return this.currentUser.achievements.thirtyIterativeOrdersChef;
    }

    //Getters all CONDITIONALS orders achievements - Chef
    public bool getFirstConditionalOrderChefAchievement()
    {
        return this.currentUser.achievements.firstConditionalOrderChef;
    }
    public bool getTenConditionalOrdersChefAchievement()
    {
        return this.currentUser.achievements.tenConditionalOrdersChef;
    }

    public bool getFiftyConditionalOrdersChefAchievement()
    {
        return this.currentUser.achievements.fiftyConditionalOrdersChef;
    }

    //Getters IF orders achievements - Chef
    public bool getFirstConditionalIfOrderChefAchievement(){
        return this.currentUser.achievements.firstConditionalIfOrderChef;
    }
    public bool getTenConditionalIfOrdersChefAchievement(){
        return this.currentUser.achievements.tenConditionalIfOrdersChef;
    }
    public bool getThirtyConditionalIfOrdersChefAchievement(){
        return this.currentUser.achievements.thirtyConditionalIfOrdersChef;
    }

    //Getters ElSE orders achievements - Chef
    public bool getFirstConditionalIfElseOrderChefAchievement()
    {
        return this.currentUser.achievements.firstConditionalIfElseOrderChef;
    }
    public bool getTenConditionalIfElseOrdersChefAchievement()
    {
        return this.currentUser.achievements.tenConditionalIfElseOrdersChef;
    }
    public bool getThirtyConditionalIfElseOrdersChefAchievement()
    {
        return this.currentUser.achievements.thirtyConditionalIfElseOrdersChef;
    }

    //Setters ALL orders achievements  - Chef
    public void setFirstOrderChefAchievement(bool unlock){
        this.currentUser.achievements.firstOrderChef = unlock;
    }
    public void setTenOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.tenOrdersChef = unlock;
    }
    public void setTwentyfiveOrdersChefAchievement(bool unlock){
        this.currentUser.achievements.twentyfiveOrdersChef = unlock;
    }
    public void setFiftyOrderChefAchievement(bool unlock)
    {
        this.currentUser.achievements.fiftyOrdersChef = unlock;
    }

    public void setHundredOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.hundredOrdersChef = unlock;
    }

    //Setters BASIC orders achievements  - Chef
    public void setFirstBasicOrderChefAchievement(bool unlock)
    {
        this.currentUser.achievements.firstBasicOrderChef = unlock;
    }

    public void setTenBasicOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.tenBasicOrdersChef = unlock;
    }
    public void setTwentyfiveBasicOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.twentyfiveBasicOrdersChef = unlock;
    }
    //Setters iterative orders achievements  - Chef
    public void setFirstIterativeOrderChefAchievement(bool unlock){
        this.currentUser.achievements.firstIterativeOrderChef = unlock;
    }
    public void setTenIterativeOrdersChefAchievement(bool unlock){
        this.currentUser.achievements.tenIterativeOrdersChef = unlock;
    }
    public void setThirtyIterativeOrdersChefAchievement(bool unlock){
        this.currentUser.achievements.thirtyIterativeOrdersChef = unlock;
    }

    //Setters all conditional orders achievements  - Chef
    public void setFirstConditionalOrderChefAchievement(bool unlock)
    {
        this.currentUser.achievements.firstConditionalOrderChef = unlock;
    }
    public void setTenConditionalOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.tenConditionalOrdersChef = unlock;
    }
    public void setFiftyConditionalOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.fiftyConditionalOrdersChef = unlock;
    }

    //Setters IF orders achievements  - Chef
    public void setFirstConditionalIfOrderChefAchievement(bool unlock){
        this.currentUser.achievements.firstConditionalIfOrderChef = unlock;
    }
    public void setTenConditionalIfOrdersChefAchievement(bool unlock){
        this.currentUser.achievements.tenConditionalIfOrdersChef = unlock;
    }
    public void setThirtyConditionalIfOrdersChefAchievement(bool unlock){
        this.currentUser.achievements.thirtyConditionalIfOrdersChef = unlock;
    }

    //Setters IF-ELSE orders achievements  - Chef
    public void setFirstConditionalIfElseOrderChefAchievement(bool unlock)
    {
        this.currentUser.achievements.firstConditionalIfElseOrderChef = unlock;
    }
    public void setTenConditionalIfElseOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.tenConditionalIfElseOrdersChef = unlock;
    }
    public void setThirtyConditionalIfElseOrdersChefAchievement(bool unlock)
    {
        this.currentUser.achievements.thirtyConditionalIfElseOrdersChef = unlock;
    }

    //Getters orders done  - Waiter
    public int getNumOrdersWaiter()
    {
        return this.currentUser.dataLevel.ordersWaiter;
    }
    public int getNumBasicOrdersWaiter()
    {
        return this.currentUser.dataLevel.basicOrdersWaiter;
    }
    public int getNumConditionalOrdersWaiter()
    {
        return this.currentUser.dataLevel.conditionalOrdersWaiter;
    }
    public int getNumConditionalIfOrdersWaiter()
    {
        return this.currentUser.dataLevel.conditionalIfOrdersWaiter;
    }
    public int getNumConditionalIfElseOrdersWaiter()
    {
        return this.currentUser.dataLevel.conditionalIfElseOrdersWaiter;
    }
    public int getNumIterativeOrdersWaiter()
    {
        return this.currentUser.dataLevel.iterativeOrdersWaiter;
    }

    //Setters orders done - Waiter
    public void setNumOrdersWaiter(int num)
    {
        this.currentUser.dataLevel.ordersWaiter = num;
    }
    public void setNumBasicOrdersWaiter(int num)
    {
        this.currentUser.dataLevel.basicOrdersWaiter = num;
    }
    public void setNumConditionalOrdersWaiter(int num)
    {
        this.currentUser.dataLevel.conditionalOrdersWaiter = num;
    }
    public void setNumConditionalIfOrdersWaiter(int num)
    {
        this.currentUser.dataLevel.conditionalIfOrdersWaiter = num;
    }
    public void setNumConditionalIfElseOrdersWaiter(int num)
    {
        this.currentUser.dataLevel.conditionalIfElseOrdersWaiter = num;
    }
    public void setNumIterativeOrdersWaiter(int num)
    {
        this.currentUser.dataLevel.iterativeOrdersWaiter = num;
    }

    //Getters all orders achievements - Waiter
    public bool getFirstOrderWaiterAchievement()
    {
        return this.currentUser.achievements.firstOrderWaiter;
    }
    public bool getTenOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.tenOrdersWaiter;
    }
    public bool getTwentyfiveOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.twentyfiveOrdersWaiter;
    }
    public bool getFiftyOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.fiftyOrdersWaiter;
    }
    public bool getHundredOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.hundredOrdersWaiter;
    }

    //Getters basic orders achievements - Waiter 
    public bool getFirstBasicOrderWaiterAchievement()
    {
        return this.currentUser.achievements.firstBasicOrderWaiter;
    }
    public bool getTenBasicOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.tenBasicOrdersWaiter;
    }
    public bool getTwentyfiveBasicOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.twentyfiveBasicOrdersWaiter;
    }

    //Getters ITERATIVE orders achievements - Waiter 
    public bool getFirstIterativeOrderWaiterAchievement()
    {
        return this.currentUser.achievements.firstIterativeOrderWaiter;
    }
    public bool getTenIterativeOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.tenIterativeOrdersWaiter;
    }
    public bool getThirtyIterativeOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.thirtyIterativeOrdersChef;
    }

    //Getters ALL conditional orders achievements - Waiter 
    public bool getFirstConditionalOrderWaiterAchievement()
    {
        return this.currentUser.achievements.firstConditionalOrderWaiter;
    }
    public bool getTenConditionalOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.tenConditionalOrdersWaiter;
    }
    public bool getThirtyConditionalOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.thirtyConditionalOrdersWaiter;
    }

    public bool getFiftyConditionalOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.thirtyConditionalOrdersWaiter;
    }
    //Getters IF orders achievements - Waiter 
    public bool getFirstConditionalIfOrderWaiterAchievement()
    {
        return this.currentUser.achievements.firstConditionalIfOrderWaiter;
    }
    public bool getTenConditionalIfOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.tenConditionalIfOrdersWaiter;
    }
    public bool getThirtyConditionalIfOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.thirtyConditionalIfOrdersWaiter;
    }

    //Getters ELSE orders achievements - Waiter 
    public bool getFirstConditionalIfElseOrderWaiterAchievement()
    {
        return this.currentUser.achievements.firstConditionalIfElseOrderWaiter;
    }
    public bool getTenConditionalIfElseOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.tenConditionalIfElseOrdersWaiter;
    }
    public bool getThirtyConditionalIfElseOrdersWaiterAchievement()
    {
        return this.currentUser.achievements.thirtyConditionalIfElseOrdersWaiter;
    }

    //Setters all orders achievements - Waiter
    public void setFirstOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.firstOrderWaiter = unlock;
    }
    public void setTenOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.tenOrdersWaiter = unlock;
    }
    public void setTwentyfiveOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.twentyfiveOrdersWaiter = unlock;
    }
    public void setFiftyOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.fiftyOrdersWaiter = unlock;
    }
    public void setHundredOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.hundredOrdersWaiter = unlock;
    }

    //Setters basic orders achievements - Waiter
    public void setBasicFirstOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.firstBasicOrderWaiter = unlock;
    }
    public void setTenBasicOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.tenBasicOrdersWaiter = unlock;
    }
    public void setTwentyfiveBasicOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.twentyfiveBasicOrdersWaiter = unlock;
    }

    //Setters iterative orders achievements - Waiter
    public void setFirstIterativeOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.firstIterativeOrderChef = unlock;
    }
    public void setTenIterativeOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.tenIterativeOrdersWaiter = unlock;
    }
    public void setThirtyIterativeOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.thirtyIterativeOrdersWaiter = unlock;
    }

    //Setters all conditional orders achievements - Waiter
    public void setFirstConditionalOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.firstConditionalOrderWaiter = unlock;
    }
    public void setTenConditionalOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.tenConditionalOrdersWaiter = unlock;
    }
    public void setThirtyConditionalOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.thirtyConditionalOrdersWaiter = unlock;
    }
    public void setFiftyConditionalOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.fiftyConditionalOrdersWaiter = unlock;
    }

    //Setters IF orders achievements - Waiter
    public void setFirstConditionalIfOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.firstConditionalIfOrderWaiter = unlock;
    }
    public void setTenConditionalIfOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.tenConditionalIfOrdersWaiter = unlock;
    }
    public void setThirtyConditionalIfOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.thirtyConditionalIfOrdersWaiter = unlock;
    }

    //Setters ELSE orders achievements - Waiter
    public void setFirstConditionalIfElseOrderWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.firstConditionalIfElseOrderWaiter = unlock;
    }
    public void setTenConditionalIfElseOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.tenConditionalIfElseOrdersWaiter = unlock;
    }
    public void setThirtyConditionalIfElseOrdersWaiterAchievement(bool unlock)
    {
        this.currentUser.achievements.thirtyConditionalIfElseOrdersWaiter = unlock;
    }

    public void setVolume(string vol){
        this.currentUser.volume = vol;
    }
    public float getVolume(){
        return float.Parse(this.currentUser.volume,System.Globalization.CultureInfo.InvariantCulture);
    }

    public string getDataCollection()
    {
        return this.currentUser.dataCollection;
    }

    public void setDataCollection(string data)
    {
        this.currentUser.dataCollection = data;
    }



}
