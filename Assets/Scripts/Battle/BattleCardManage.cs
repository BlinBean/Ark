using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardManage : MonoBehaviour
{
    List<int> characterList;//外部传进来的角色列表
    public float offsetX;
    public float offsetY;
    GameObject card;
    void Start()
    {
        GameObject cards = GameObject.Find("cards");
        Debug.Log(cards==null);
        card = Resources.Load<GameObject>("prefabs/UI/card");
        //生成对应的角色卡
        for(int i = 0; i < 9; i++)
        {
           GameObject obj=Instantiate(card);
            obj.transform.parent = cards.transform;

        }





        //foreach(var item in characterList)
        //{
        //    //new一个卡片//传入角色id
        //    //生成对应的q版小人作为缓冲//脚本功能关闭
        //    CharacterCardInBattle card=new CharacterCardInBattle();
        //    //生成iamge

            
        //}
    }
    //添加卡片
    //移除卡片
  }
