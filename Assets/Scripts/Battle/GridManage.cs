using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GridManage {
    public int hight;
    public int width;
    public Grid[,] levelMap;

    static GridManage instance;

    //-------------------------------------------

    public static GridManage Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GridManage();
            }
            return instance;
        }
    }

    public void Init()
    {
        //读取地图
        TextAsset textAsset = (TextAsset)Resources.Load("levelMap/0-1");

        //解析为字符串数组
        string[] map_row = textAsset.text.Trim().Split('\n');

        //地图长宽赋值
        hight = map_row.Length;
        string tempDate = map_row[0];
        width=tempDate.Split(',').Length;

        //初始化地图
        levelMap = new Grid[hight, width];

        for (int i = 0; i < hight; i++)
        {
            var temp= map_row[i].Split(',');
            for (int j = 0; j < width; j++)
            {
                if (temp[j] != ","&& temp[j] != "")
                {
                    levelMap[i, j] = new Grid(i, j,int.Parse(temp[j]) );
                }
            }
        }
    }


    #region 寻找可以放置的格子
    public List<Pos> CanPlaceGrids(Character_Type type)
    {
        //近卫 地面
        //先锋 地面
        //重装 地面
        //辅助 高台
        //狙击 高台
        //治疗 高台
        //术士 高台
        //特种 跳过

        //定义一个位置列表
        List<Pos> place=new List<Pos>();

        switch (type)
        {
            case Character_Type.guard:
                place = FindPlaceGrid(Grid_Type.terrace);
                break;
            case Character_Type.vanguard:
                place = FindPlaceGrid(Grid_Type.terrace);
                break;
            case Character_Type.defender:
                place = FindPlaceGrid(Grid_Type.terrace);
                break;
            case Character_Type.supporter:
                place = FindPlaceGrid(Grid_Type.ground);
                break;
            case Character_Type.sniper:
                place = FindPlaceGrid(Grid_Type.ground);
                break;
            case Character_Type.medic:
                place = FindPlaceGrid(Grid_Type.ground);
                break;
            case Character_Type.caster:
                place = FindPlaceGrid(Grid_Type.ground);
                break;
            case Character_Type.specialist:
                place = FindPlaceGrid(Grid_Type.ground);
                break;
        }

        return place;
    }
    List<Pos> FindPlaceGrid(Grid_Type exclude)
    {
        List<Pos> grids = new List<Pos>();
        foreach (var item in levelMap)
        {
            if (item.Type > 0 && item.Type != exclude)
            {
                Pos temp = new Pos();
                //Debug.Log("item.x:"+item.GetX+","+"item.y:"+item.GetY);
                temp.posX = item.GetX;
                temp.posY = item.GetY;
                //Debug.Log(temp.posX+","+temp.posY);
                grids.Add(temp);
            }
        }
        return grids;
    }

    #endregion
}
