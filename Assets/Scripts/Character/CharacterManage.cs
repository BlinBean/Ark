using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManage : MonoBehaviour
{
    List<Character> warehouse;//总卡池
    List<Character> ownWarehouse;//拥有的卡
    //6星卡池
    //5星卡池
    //4星卡池
    //3星卡池

    void Start()
    {
              
    }

    void Update()
    {
        
    }
    //根据拥有的卡片new出角色
    //从已有的卡中读取数据
    //现在想的是从数据库中读取数据实例化已有角色，
    //技能等战斗开始后在实例化
    //也就是战斗用的是一个通用类
}
