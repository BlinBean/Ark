using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterCardInBattle : MonoBehaviour, IDragHandler,IPointerDownHandler,IEndDragHandler
{

    Camera cameraTemp;  //摄像机位置

    public int offsetY; //向上偏移距离

    GameObject propertyCharacter;//属性面板

    GameObject characterUI;     //拖动的角色UI//应该也有4个方向

    bool cameraState = false;//相机状态

    float deployTime;//再部署时间

    bool clickState = false;//点击次数

    GameObject thisCharacter;//模型//可能要用一个list来存储，毕竟有4个方向

    static List<Pos> CanPlace; //可放置列表

    List<Pos> attackRange;//攻击距离]
    List<Pos> currentAttackRange;//

    GameObject dir;//攻击方向UI

    Sprite hoshigumaUp;

    public CharacterCardInBattle()
    {
        
    }



    void Start()
    {
        //------------测试
        attackRange = new List<Pos>();
        currentAttackRange = new List<Pos>();
        Pos a = new Pos();
        a.posX = 0;
        a.posY = 0;


        Pos b = new Pos();
        b.posX = 0;
        b.posY = -1;
        attackRange.Add(a);
        attackRange.Add(b);

        //----------

        var temp = Resources.LoadAll<Sprite>("char/01_Hoshigunma/hoshigumaUp");
        //Debug.Log(temp.Length);
        hoshigumaUp = temp[34];

        //获取相机
        cameraTemp = GameObject.FindObjectOfType<Camera>();

        //获取属性面板
        propertyCharacter = GameObject.Find("characterProperty");
        characterUI = GameObject.Find("characterUI");

        //初始化可放置列表
        CanPlace = new List<Pos>();

        //攻击方向UI
        dir= GameObject.Find("dir");

        //判断什么角色

        //寻找对应的模型
    }

   
    //点击角色卡
    public void OnPointerDown(PointerEventData eventData)
    {
        //设置点击
        clickState = !clickState;

        //搜索可用格子//原先想放在start里不过想了想干员放置是动态的
        List<Pos> CanPlace = GridManage.Instance.CanPlaceGrids(Character_Type.guard);

        if (clickState == true)
        {
            //设置相机状态
            cameraState = true;

            //选择角色时 时间减缓
            Time.timeScale = 0.5f;

            //显示角色属性面板
            propertyCharacter.GetComponent<CanvasGroup>().alpha = 1;

            //对应的角色卡上移一点
            Vector3 judgePos = transform.position;
            judgePos.y += offsetY;
            transform.position = judgePos;

            //显示可用格子
            BattleShow.Instance.ShowPlaceGirds(CanPlace);

            //角色UI偏转
            characterUI.transform.Rotate(new Vector3(0, 0, 2));

        }
        else
        {
            //设置相机状态
            cameraState = false;

            //时间恢复
            Time.timeScale = 1;

            //关闭角色属性
            propertyCharacter.GetComponent<CanvasGroup>().alpha = 0;

            //角色卡复位
            Vector3 judgePos = transform.position;
            judgePos.y -= offsetY;
            transform.position = judgePos;

            //关闭显示
            BattleShow.Instance.ClosePlaceGirds(CanPlace);

            //角色UI复位
            characterUI.transform.Rotate(new Vector3(0, 0, -2));
        }

        //设置相机偏移
        if (cameraState) {
            cameraTemp.transform.Rotate(new Vector3(0, 0, -2));
        }
        else
        {
            cameraTemp.transform.Rotate(new Vector3(0, 0, 2));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //清空状态
        BattleShow.Instance.ColosShowAttackRange();
        currentAttackRange.Clear();

        //拖动角色的UI,设置UI可显示
        characterUI.GetComponent<CanvasGroup>().alpha = 1;

        //定义射线和被射线照到的信息
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;

        //得到可行走的格子的坐标
        List<Pos> CanPlace = GridManage.Instance.CanPlaceGrids(Character_Type.guard);

        //----------------------------------------------


        //如果照到格子上
        if (Physics.Raycast(ray,out info, 1000f,1<<LayerMask.NameToLayer("Grid")))
        {
            //获得当前格子
            Transform currentGrid = info.transform;
            //设置中心点//因为有点不准
            Vector3 centerPoint = new Vector3(currentGrid.position.x-0.1f , 0, currentGrid.position.z + 0.4f);

            //获得当前格子的坐标
            string[] currentName = currentGrid.transform.parent.name.Split('-');
            int x=int.Parse(currentName[0]);
            int y= int.Parse(currentName[1]);

            //判断是否再可行走的格子上,新建变量

            Pos testpos = new Pos();
            testpos.posX = x;
            testpos.posY = y;

            if (CanPlace.Contains(testpos)){
                //角色按格移动
                Vector3 scrrenPos = Camera.main.WorldToScreenPoint(centerPoint);
                characterUI.transform.position = scrrenPos;

                //在脚下生成距离
                foreach (var item in attackRange)
                {
                    //得到攻击距离的格子坐标
                    int rangeX = testpos.posX+item.posX;
                    int rangeY = testpos.posY + item.posY;
                    //Debug.Log("rangX" + rangeX + "rangeY" + rangeY);
                    Pos rangePos = new Pos();
                    rangePos.posX = rangeX;
                    rangePos.posY = rangeY;
                    currentAttackRange.Add(rangePos);
                }
                BattleShow.Instance.ShowAttackRnage(currentAttackRange);
            }
            else
            {
                characterUI.transform.position = Input.mousePosition;
            }
        }
        else
        {
            characterUI.transform.position = Input.mousePosition;
        }
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        //清空
        BattleShow.Instance.ColosShowAttackRange();
        currentAttackRange.Clear();

        //得到可行走的格子的坐标
        List<Pos> CanPlace = GridManage.Instance.CanPlaceGrids(Character_Type.guard);

        //判断当前点是否可以放置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info, 1000f, 1 << LayerMask.NameToLayer("Grid")))
        {
            //获得当前格子
            Transform currentGrid = info.transform;

            //获得当前格子的坐标
            string[] currentName = currentGrid.transform.parent.name.Split('-');
            int x = int.Parse(currentName[0]);
            int y = int.Parse(currentName[1]);

            //判断是否再可行走的格子上,新建变量

            Pos testpos = new Pos();
            testpos.posX = x;
            testpos.posY = y;

            //可放置
            if (CanPlace.Contains(testpos))
            {

                //方向选项面板显示
                dir.transform.position = characterUI.transform.position;
                dir.GetComponent<CanvasGroup>().alpha = 1;
                dir.GetComponent<CanvasGroup>().interactable = true;
                dir.GetComponent<CanvasGroup>().blocksRaycasts = true;

               

                //隐藏选线卡
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else
            {
                characterUI.transform.position = new Vector3(100,-100,100);
            }


        }
        else
        {
                characterUI.transform.position = new Vector3(100,-100,100);
        }




        //滑动选择方向//中间有个虚拟摇杆判断方向//拖到边界松手确定//拖到边界没送手取消按钮消失//拖回中心取消按钮出现
        //根据方向旋转
        //攻击距离方向也相应的旋转
        //判断无误后将人物位置固定再位置上


        //不可部署消失 


        //放置成功后还要将放置的格子类型设为有人


        //选择完成后UI不显示，再当前位置放置人物


       


        //设置相机状态，相机复位
        cameraState = false;
        cameraTemp.transform.Rotate(new Vector3(0, 0, 2));

        //设置点击
        clickState = !clickState;

       
        //属性面板隐藏
        propertyCharacter.GetComponent<CanvasGroup>().alpha = 0;

        //时间恢复
        Time.timeScale = 1;

        //关闭显示//这边再搜索一次是因为不知道为啥CanPlace没赋值
        BattleShow.Instance.ClosePlaceGirds(CanPlace);


        //角色UI复位
        characterUI.transform.Rotate(new Vector3(0, 0, -2));
    }



    void FindPalceGirds()
    {
        //定义射线和被射线照到的信息
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;

        //得到可行走的格子的坐标
        List<Pos> CanPlace = GridManage.Instance.CanPlaceGrids(Character_Type.guard);

        //----------------------------------------------


        //如果照到格子上
        if (Physics.Raycast(ray, out info, 1000f, 1 << LayerMask.NameToLayer("Grid")))
        {
            //获得当前格子
            Transform currentGrid = info.transform;
            //设置中心点//因为有点不准
            Vector3 centerPoint = new Vector3(currentGrid.position.x - 0.1f, 0, currentGrid.position.z + 0.4f);

            //获得当前格子的坐标
            string[] currentName = currentGrid.transform.parent.name.Split('-');
            int x = int.Parse(currentName[0]);
            int y = int.Parse(currentName[1]);

            //判断是否再可行走的格子上,新建变量

            Pos testpos = new Pos();
            testpos.posX = x;
            testpos.posY = y;

            //-----------
            if (CanPlace.Contains(testpos))
            {

                //在脚下生成距离
                foreach (var item in attackRange)
                {
                    //得到攻击距离的格子坐标
                    int rangeX = testpos.posX + item.posX;
                    int rangeY = testpos.posY + item.posY;
                    //Debug.Log("rangX" + rangeX + "rangeY" + rangeY);
                    Pos rangePos = new Pos();
                    rangePos.posX = rangeX;
                    rangePos.posY = rangeY;
                    currentAttackRange.Add(rangePos);
                }
                BattleShow.Instance.ShowAttackRnage(currentAttackRange);
            }
        }
    }


}
