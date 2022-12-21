using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI_ScrollView
{
    public class UI程序
    {
        public static void SetActive(GameObject obj, bool isActive)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }

        }
    }

    public enum 方向
    {
        Horizontal,
        Vertical
    }

    public class UI_Scroll : MonoBehaviour
    {
        public 方向 卷軸方向 = 方向.Horizontal;

        public int 排數 = 1;

        public float 間距 = 0f;
        public GameObject Cell單位; //指定的cell

        protected Action<GameObject, int> 函數回調;

        protected RectTransform rectTrans;

        protected float Plane寬度;
        protected float Plane高度;

        protected float Content寬度;
        protected float Content高度;

        protected float Cell寬度;
        protected float Cell高度;

        protected GameObject Content;
        protected RectTransform ContentRectTrans;

        private bool 已初始化 = false;

        //記錄 物體的坐標 和 物體 
        protected struct CellInfo
        {
            public Vector3 pos;
            public GameObject obj;
        };
        protected CellInfo[] Cell資訊;

        protected bool 已啟動 = false;

        protected ScrollRect 卷軸;

        protected int 列表數量 = -1; //列表數量

        protected int m_MinIndex = -1;
        protected int m_MaxIndex = -1;

        protected bool 已清空列表 = false;

        public virtual void 初始化(Action<GameObject, int> callBack)
        {
            重製回調();
            函數回調 = callBack;
            if (已初始化)return;


            Content = this.GetComponent<ScrollRect>().content.gameObject;

            if (Cell單位 == null)
            {
                Cell單位 = Content.transform.GetChild(0).gameObject;
            }
            /* Cell 處理 */
            SetPoolsObj(Cell單位);

            RectTransform cellRectTrans = Cell單位.GetComponent<RectTransform>();
            cellRectTrans.pivot = new Vector2(0f, 1f);
            檢查錨點(cellRectTrans);
            cellRectTrans.anchoredPosition = Vector2.zero;

            //記錄 Cell 資訊
            Cell高度 = cellRectTrans.rect.height;
            Cell寬度 = cellRectTrans.rect.width;

            //記錄 Plane 資訊
            rectTrans = GetComponent<RectTransform>();
            Rect planeRect = rectTrans.rect;
            Plane高度 = planeRect.height;
            Plane寬度 = planeRect.width;

            //記錄 Content 資訊
            ContentRectTrans = Content.GetComponent<RectTransform>();
            Rect contentRect = ContentRectTrans.rect;
            Content高度 = contentRect.height;
            Content寬度 = contentRect.width;

            ContentRectTrans.pivot = new Vector2(0f, 1f);
            檢查錨點(ContentRectTrans);

            卷軸 = this.GetComponent<ScrollRect>();

            卷軸.onValueChanged.RemoveAllListeners();
            //添加滑動事件
            卷軸.onValueChanged.AddListener(delegate (Vector2 value) { 卷軸監聽器(value); });

            已初始化 = true;

        }
        private void 檢查錨點(RectTransform rectTrans)
        {
            if (卷軸方向 == 方向.Vertical)
            {
                if (!((rectTrans.anchorMin == new Vector2(0, 1) && rectTrans.anchorMax == new Vector2(0, 1)) || (rectTrans.anchorMin == new Vector2(0, 1) && rectTrans.anchorMax == new Vector2(1, 1))))
                {
                    rectTrans.anchorMin = new Vector2(0, 1);
                    rectTrans.anchorMax = new Vector2(1, 1);
                }
            }
            else
            {
                if (!((rectTrans.anchorMin == new Vector2(0, 1) && rectTrans.anchorMax == new Vector2(0, 1)) || (rectTrans.anchorMin == new Vector2(0, 0) && rectTrans.anchorMax == new Vector2(0, 1))))
                {
                    rectTrans.anchorMin = new Vector2(0, 0);
                    rectTrans.anchorMax = new Vector2(0, 1);
                }
            }
        }

        //實時刷新列表時用
        public virtual void 更新列表()
        {
            for (int i = 0, length = Cell資訊.Length; i < length; i++)
            {
                CellInfo cellInfo = Cell資訊[i];
                if (cellInfo.obj != null)
                {
                    float rangePos = 卷軸方向 == 方向.Vertical ? cellInfo.pos.y : cellInfo.pos.x;
                    if (!已超出範圍(rangePos))
                    {
                        Func(函數回調, cellInfo.obj, true);
                    }
                }
            }
        }

        //刷新某一项
        public void 更新單個Cell(int index)
        {
            CellInfo cellInfo = Cell資訊[index - 1];
            if (cellInfo.obj != null)
            {
                float rangePos = 卷軸方向 == 方向.Vertical ? cellInfo.pos.y : cellInfo.pos.x;
                if (!已超出範圍(rangePos))
                {
                    Func(函數回調, cellInfo.obj);
                }
            }
        }

        public virtual void 顯示列表(int num)
        {
            m_MinIndex = -1;
            m_MaxIndex = -1;

            //-> 計算 Content 尺寸
            if (卷軸方向 == 方向.Vertical)
            {
                float contentSize = (間距 + Cell高度) * Mathf.CeilToInt((float)num / 排數);
                Content高度 = contentSize;
                Content寬度 = ContentRectTrans.sizeDelta.x;
                contentSize = contentSize < rectTrans.rect.height ? rectTrans.rect.height : contentSize;
                ContentRectTrans.sizeDelta = new Vector2(Content寬度, contentSize);
                if (num != 列表數量)
                {
                    ContentRectTrans.anchoredPosition = new Vector2(ContentRectTrans.anchoredPosition.x, 0);
                }
            }
            else
            {
                float contentSize = (間距 + Cell寬度) * Mathf.CeilToInt((float)num / 排數);
                Content寬度 = contentSize;
                Content高度 = ContentRectTrans.sizeDelta.x;
                contentSize = contentSize < rectTrans.rect.width ? rectTrans.rect.width : contentSize;
                ContentRectTrans.sizeDelta = new Vector2(contentSize, Content高度);
                if (num != 列表數量)
                {
                    ContentRectTrans.anchoredPosition = new Vector2(0, ContentRectTrans.anchoredPosition.y);
                }
            }

            //-> 計算 開始索引
            int lastEndIndex = 0;

            if (已啟動)
            {
                lastEndIndex = num - 列表數量 > 0 ? 列表數量 : num;
                lastEndIndex = 已清空列表 ? 0 : lastEndIndex;

                int count = 已清空列表 ? Cell資訊.Length : 列表數量;
                for (int i = lastEndIndex; i < count; i++)
                {
                    if (Cell資訊[i].obj != null)
                    {
                        if (i <= 0) SetPoolsObj(Cell資訊[i].obj);
                        else Destroy(Cell資訊[i].obj);
                        //SetPoolsObj(Cell資訊[i].obj);
                        Cell資訊[i].obj = null;
                    }
                }
            }

            CellInfo[] tempCellInfos = Cell資訊;
            Cell資訊 = new CellInfo[num];

            //-> 1: 計算 每個Cell坐標並存儲
            for (int i = 0; i < num; i++)
            {
                // * -> 存儲 已有的數據 ( 首次顯示列表函數時則無效 )
                if (列表數量 != -1 && i < lastEndIndex)
                {
                    CellInfo tempCellInfo = tempCellInfos[i];
                    //-> 計算是否超出範圍
                    float rPos = 卷軸方向 == 方向.Vertical ? tempCellInfo.pos.y : tempCellInfo.pos.x;
                    if (!已超出範圍(rPos))
                    {
                        //-> 記錄顯示範圍中的 首位index 和 末尾index
                        m_MinIndex = m_MinIndex == -1 ? i : m_MinIndex; //首位index
                        m_MaxIndex = i; // 末尾index

                        if (tempCellInfo.obj == null)
                        {
                            tempCellInfo.obj = GetPoolsObj();
                        }
                        tempCellInfo.obj.transform.GetComponent<RectTransform>().anchoredPosition = tempCellInfo.pos;
                        tempCellInfo.obj.name = i.ToString();
                        tempCellInfo.obj.SetActive(true);

                        Func(函數回調, tempCellInfo.obj);
                    }
                    Cell資訊[i] = tempCellInfo;
                    continue;
                }

                CellInfo cellInfo = new CellInfo();

                float pos = 0;  //坐標( isVertical ? 記錄Y : 記錄X )
                float rowPos = 0; //計算每排裡面的cell 坐標

                // * -> 計算每個Cell坐標
                if (卷軸方向 == 方向.Vertical)
                {
                    pos = Cell高度 * Mathf.FloorToInt(i / 排數) + 間距 * Mathf.FloorToInt(i / 排數);
                    rowPos = Cell寬度 * (i % 排數) + 間距 * (i % 排數);
                    cellInfo.pos = new Vector3(rowPos, -pos, 0);
                }
                else
                {
                    pos = Cell寬度 * Mathf.FloorToInt(i / 排數) + 間距 * Mathf.FloorToInt(i / 排數);
                    rowPos = Cell高度 * (i % 排數) + 間距 * (i % 排數);
                    cellInfo.pos = new Vector3(pos, -rowPos, 0);
                }

                //-> 計算每排裡面的cell 坐標
                m_MinIndex = m_MinIndex == -1 ? i : m_MinIndex; //首位index
                m_MaxIndex = i; // 末尾index

                //-> 取或創建 Cell
                GameObject cell = GetPoolsObj();
                cell.transform.GetComponent<RectTransform>().anchoredPosition = cellInfo.pos;
                cell.gameObject.name = i.ToString();

                //-> 存数据
                cellInfo.obj = cell;
                Cell資訊[i] = cellInfo;

                //-> 回調函數
                Func(函數回調, cell);
            }

            列表數量 = num;
            已啟動 = true;

        }

        // 更新滾動區域的大小
        public void 更新Plane大小()
        {
            Rect rect = GetComponent<RectTransform>().rect;
            Plane高度 = rect.height;
            Plane寬度 = rect.width;
        }

        //滑動事件
        protected virtual void 卷軸監聽器(Vector2 value)
        {
            更新檢查();
        }

        private void 更新檢查()
        {
            if (Cell資訊 == null)
                return;

            //检查超出范围
            for (int i = 0, length = Cell資訊.Length; i < length; i++)
            {
                CellInfo cellInfo = Cell資訊[i];
                GameObject obj = cellInfo.obj;
                Vector3 pos = cellInfo.pos;

                float rangePos = 卷軸方向 == 方向.Vertical ? pos.y : pos.x;
                //判斷是否超出顯示範圍
                if (!已超出範圍(rangePos))
                {
                    if (obj == null)
                    {
                        GameObject cell = GetPoolsObj();
                        cell.transform.localPosition = pos;
                        cell.gameObject.name = i.ToString();
                        Cell資訊[i].obj = cell;

                        Func(函數回調, cell);
                    }
                }
            }
        }

        //判斷是否超出顯示範圍
        protected bool 已超出範圍(float pos)
        {
            Vector3 listP = ContentRectTrans.anchoredPosition;
            if (卷軸方向 == 方向.Vertical)
            {
                if (pos + listP.y > Cell高度 || pos + listP.y < -rectTrans.rect.height)
                {
                    return true;
                }
            }
            else
            {
                if (pos + listP.x < -Cell寬度 || pos + listP.x > rectTrans.rect.width)
                {
                    return true;
                }
            }
            return false;
        }

        //對像池 機制  (存入， 取出) cell
        protected Stack<GameObject> poolsObj = new Stack<GameObject>();
        //取出 cell
        protected virtual GameObject GetPoolsObj()
        {
            GameObject cell = null;
            if (poolsObj.Count > 0)
            {
                cell = poolsObj.Pop();
            }

            if (cell == null)
            {
                cell = Instantiate(Cell單位) as GameObject;
            }
            cell.transform.SetParent(Content.transform);
            cell.transform.localScale = Vector3.one;
            UI程序.SetActive(cell, true);

            return cell;
        }
        //存入 cell
        protected virtual void SetPoolsObj(GameObject cell)
        {
            if (cell != null)
            {
                poolsObj.Push(cell);
                UI程序.SetActive(cell, false);
            }
        }

        //回調
        protected void Func(Action<GameObject, int> func, GameObject selectObject, bool isUpdate = false)
        {
            int num = int.Parse(selectObject.name) + 1;
            if (func != null)
            {
                func(selectObject, num);
            }
        }

        public void 重製回調()
        {
            if (函數回調 != null)
            {
                函數回調 = null;
            }
        }

        protected void OnDestroy()
        {
            重製回調();
        }
    }
}