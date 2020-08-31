using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grid_Type
{
    homePoint=-1,          //家
    enemyPoint=-2,         //地面敌人出生
    airplanePoint=-3,      //飞机出生
    obstacle=-4,           //障碍
    forbid=-5,             //不能放置（地上有×的那种）
    hole=-6,               //落穴          
    wind=-7,               //吹风
    barbette=-8,           //防御塔 
    character=-9,           //有人



    ground=1,             //地面           
    terrace=2,            //平台         
    grass=3,              //草丛    
    scout=4,              //侦察塔
    defense=5,            //防御力增强 
    airStrengthen=6,      //对空地形    
    emp=7,                //EMP     
    strengthenAndBleed=8, //攻击增强并持续扣血
    cure=9,               //治疗   
    flame=10,             //地火  
    contendPoint=11,      //防御塔争夺点
}

public struct Pos
{
    public int posX;
    public int posY;
    public  bool Equals(Pos other)
    {
        if(other.posX==posX && other.posY == posY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Grid
{
    Pos pos;
    Grid_Type type;

    public Grid(int x, int y,int type)
    {
        pos = new Pos();
        pos.posX= x;
        pos.posY= y;
        this.type = (Grid_Type)type;
    }
    public int GetX
    {
        get {return pos.posX; }
    }
    public int GetY
    {
        get { return pos.posY; }
    }
    public Grid_Type Type
    {
        get { return type; }
    }
}
