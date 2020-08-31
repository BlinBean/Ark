using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character_Type
{
    guard,              //近卫
    sniper,             //狙击
    medic,              //治疗
    supporter,          //辅助
    vanguard,           //先锋
    caster,             //术士
    specialist,         //特种
    defender,           //重装
}

public class CharacterBase
{
    public float hp;
    public float atk;
    public float def;
    public float spellDef;                  //法抗
    public int energy;                      //能量
    public float energySpeed;               //能量回复速度
    public Character_Type type;             //角色类型
    public int cost;                        //费用
    public float revocaTime;                //撤回再部署时间
    public float killedTime;                //被杀再部署时间
    public float attackRange;               //攻击范围，要改
    public float attackSpeed;               //攻击速度
    public int trust;                       //信赖值
    public int rank;                        //等级
    public int rarit;                       //稀有度
    public int resist;                      //阻挡数量
    public Vector2 direction;               //攻击方向
    public int state;                       //各类状态
    public int attackNum;

    string characterName;
    public CharacterBase(string id)
    {
        characterName=id;
    }
    //攻击方法
    //能量满时显示技能
    //被攻击
}
