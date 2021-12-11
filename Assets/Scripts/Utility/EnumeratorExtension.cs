using System.Collections.Generic;
using UnityEngine;

public interface IHasData<Class_Data>
{
    Class_Data p_pData { get; }

    void IHas_SetData(Class_Data pData);
}

public interface IDictionaryItem<TKeyType>
{
    TKeyType IDictionaryItem_GetKey();
}

public interface IDictionaryItem_ContainData<TKeyType, TDataType> : IDictionaryItem<TKeyType>
{
    TKeyType IDictionaryItem_ContainData_GetKey();

    void IDictionaryItem_ContainData_SetData(TDataType sData);
}

public interface IListItem_HasField<TFieldType>
{
    TFieldType IListItem_HasField_GetField();
}

public static class SCEnumeratorHelper
{
    private static System.Text.StringBuilder pStringBuilder = new System.Text.StringBuilder();

    #region Enumerable

    /// <summary>
    /// 디버그용 출력
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pIter"></param>
    /// <returns></returns>
    public static string ToString_DataList<T>(this IEnumerable<T> pIter)
        where T : MonoBehaviour
    {
        pStringBuilder.Length = 0;
        foreach (T t in pIter)
        {
            pStringBuilder.Append(t.name);
            pStringBuilder.Append(", ");
        }

        return pStringBuilder.ToString();
    }

    public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
    {
        int iCapacity = 0;
        IEnumerator<TSource> pIter = source.GetEnumerator();
        while (pIter.MoveNext())
        {
            iCapacity++;
        }

        pIter = source.GetEnumerator();
        TSource[] arrReturn = new TSource[iCapacity];
        int iIndex = 0;
        while (pIter.MoveNext())
        {
            arrReturn[iIndex++] = pIter.Current;
        }

        return arrReturn;
    }

    public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
    {
        return new List<TSource>(source);
    }

    public static void ToList<TSource>(this IEnumerable<TSource> source, List<TSource> listOut, bool bIsClear_And_Add = true)
    {
        if (bIsClear_And_Add)
            listOut.Clear();

        listOut.AddRange(source);
    }

    public static Dictionary<TKey, TSource> ToDictionary<TKey, TSource>(this IEnumerable<TSource> source)
        where TSource : IDictionaryItem<TKey>
    {
        Dictionary<TKey, TSource> mapReturn = new Dictionary<TKey, TSource>();
        mapReturn.DoAddItem(source);

        return mapReturn;
    }

    private static List<int> listRandomIndexTemp = new List<int>();

    /// 테스트 코드 링크
    /// <see cref="SCEnumeratorHelper_Test.GetRandom_Test"/>
    /// <summary>
    /// 예외를 제외한 랜덤을 리턴합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="arrFilter"></param>
    /// <returns></returns>
    public static T GetRandom_Filter<T>(this IEnumerable<T> source, params T[] arrFilter)
    {
        listRandomIndexTemp.Clear();
        IEnumerator<T> pIter = source.GetEnumerator();
        int iLoopIndex = 0;
        while (pIter.MoveNext())
        {
            bool bIsException = false;
            for (int i = 0; i < arrFilter.Length; i++)
            {
                if (pIter.Current.Equals(arrFilter[i]))
                {
                    bIsException = true;
                    break;
                }
            }

            if (bIsException == false)
                listRandomIndexTemp.Add(iLoopIndex);

            iLoopIndex++;
        }

        if (iLoopIndex == 0)
            return default(T);

        int iRandomIndex = listRandomIndexTemp.GetRandom() + 1;
        pIter = source.GetEnumerator();
        T pDataReturn = default(T);

        while (pIter.MoveNext() && iRandomIndex-- > 0)
        {
            pDataReturn = pIter.Current;
        }

        return pDataReturn;
    }

    #endregion Enumerable

    public static void DoExtractItem<T, TFieldType>(this List<T> list, List<TFieldType> listOut)
        where T : IListItem_HasField<TFieldType>
    {
        listOut.Clear();
        for (int i = 0; i < list.Count; i++)
            listOut.Add(list[i].IListItem_HasField_GetField());
    }

    public static void DoExtractItemList<T, TFieldType>(this List<T> list, List<TFieldType> listOut)
        where T : IListItem_HasField<List<TFieldType>>
    {
        listOut.Clear();
        for (int i = 0; i < list.Count; i++)
            listOut.AddRange(list[i].IListItem_HasField_GetField());
    }

    public static bool Contains_PrintOnError<T>(this List<T> list, T CheckData, bool bContains = false)
    {
        bool bIsContain = list.Contains(CheckData);
        if (bIsContain == bContains)
            Debug.Log("Failed to List.Contains - " + CheckData, null);

        return bIsContain;
    }

    public static void ConvertTo_BaseItemList<TChild, TBase>(this List<TChild> listChild, ref List<TBase> listBase)
        where TChild : TBase
    {
        listBase.Clear();
        for (int i = 0; i < listChild.Count; i++)
            listBase.Add(listChild[i]);
    }

    public static bool TryGetValue<T>(this List<T> list, int iIndex, out T outData)
        where T : new()
    {
        bool bIsContain = iIndex < list.Count;
        if (bIsContain == false)
        {
            outData = new T();
            Debug.LogWarning("Failed to List.TryGetValue - Index :  " + iIndex, null);
        }
        else
            outData = list[iIndex];

        return bIsContain;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        if (list.Count == 0)
            return default(T);

        int iRandomIndex = Random.Range(0, list.Count);
        return list[iRandomIndex];
    }

    public static T GetRandom<T>(this T[] arr)
    {
        if (arr.Length == 0)
            return default(T);

        int iRandomIndex = Random.Range(0, arr.Length);
        return arr[iRandomIndex];
    }

    public static string ToStringList<T>(this List<T> list)
    {
        string strString = "Count [";
        strString += list.Count + "] - ";
        for (int i = 0; i < list.Count; i++)
        {
            strString += list[i];
            if (i != list.Count - 1)
                strString += ",";
        }

        return strString;
    }

    // =====================================================================================

    public static void AddRange_First<T>(this LinkedList<T> source, IEnumerable<T> arrItem)
    {
        IEnumerator<T> pEnumerator = arrItem.GetEnumerator();
        while (pEnumerator.MoveNext())
        {
            source.AddFirst(pEnumerator.Current);
        }
    }

    public static void AddRange_Last<T>(this LinkedList<T> source, IEnumerable<T> arrItem)
    {
        IEnumerator<T> pEnumerator = arrItem.GetEnumerator();
        while (pEnumerator.MoveNext())
        {
            source.AddLast(pEnumerator.Current);
        }
    }

    #region Dictionary

    public static bool ContainsKey_PrintOnError<TKey, TValue>(this Dictionary<TKey, TValue> map, TKey CheckKey, Object pObjectForDebug = null)
    {
        bool bIsContain = CheckKey != null;
        if (bIsContain)
            bIsContain = map.ContainsKey(CheckKey);

        if (bIsContain == false)
        {
            string strKeyName = typeof(TKey).Name;
            string strValueName = typeof(TValue).Name;
            Debug.LogWarning(string.Format("Failed to Dictionary<{0}, {1}>.ContainsKey - ({2})", strKeyName, strValueName, CheckKey), pObjectForDebug);
        }

        return bIsContain;
    }

    public static void DoAddItem<TKey, TSource>(this Dictionary<TKey, TSource> mapDataTable, IEnumerable<TSource> source, bool bIsClear = true)
    where TSource : IDictionaryItem<TKey>
    {
        if (bIsClear)
            mapDataTable.Clear();

        IEnumerator<TSource> pIter = source.GetEnumerator();
        while (pIter.MoveNext())
        {
            TSource pCurrent = pIter.Current;
            if (pCurrent == null)
                continue;

            TKey hDataID = pCurrent.IDictionaryItem_GetKey();
            if (mapDataTable.ContainsKey(hDataID))
                Debug.LogWarning("Error, Data table exists with overlapped key values!!" + typeof(TSource) + " : " + hDataID);
            else
                mapDataTable.Add(hDataID, pCurrent);
        }
    }

    public static bool Remove<TKey, TSource>(this Dictionary<TKey, TSource> mapDataTable, TSource pValue)
    where TSource : IDictionaryItem<TKey>
    {
        return mapDataTable.Remove(pValue.IDictionaryItem_GetKey());
    }

    public static void Add<TKey, TSource>(this Dictionary<TKey, TSource> mapDataTable, TSource pAddSource)
        where TSource : IDictionaryItem<TKey>
    {
        TKey hDataID = pAddSource.IDictionaryItem_GetKey();
        mapDataTable.Add(hDataID, pAddSource);
    }

    public static void Add<TKey, TSource>(this Dictionary<TKey, List<TSource>> mapDataTable, TSource pAddSource)
        where TSource : IDictionaryItem<TKey>
    {
        TKey hDataID = pAddSource.IDictionaryItem_GetKey();
        if (mapDataTable.ContainsKey(hDataID) == false)
            mapDataTable.Add(hDataID, new List<TSource>());

        mapDataTable[hDataID].Add(pAddSource);
    }

    public static void DoClear_And_AddItem<TSource, TKey>(this Dictionary<TKey, TSource> mapDataTable, IEnumerable<TSource> arrDataTable, UnityEngine.Object pObjectForDebug = null)
        where TSource : IDictionaryItem<TKey>
    {
        mapDataTable.Clear();
        if (arrDataTable == null) return;

        IEnumerator<TSource> pIter = arrDataTable.GetEnumerator();
        while (pIter.MoveNext())
        {
            TSource pCurrent = pIter.Current;
            TKey hDataID = pCurrent.IDictionaryItem_GetKey();
            if (mapDataTable.ContainsKey(hDataID))
                Debug.LogWarning("Error, Data table exists with overlapped key values!!" + typeof(TSource) + " : " + hDataID, pObjectForDebug);
            else
                mapDataTable.Add(hDataID, pCurrent);
        }
    }

    public static void DoClear_And_AddItem<TSource, TKey>(this Dictionary<TKey, List<TSource>> mapDataTable, IEnumerable<TSource> source)
        where TSource : IDictionaryItem<TKey>
    {
        mapDataTable.Clear();
        IEnumerator<TSource> pIter = source.GetEnumerator();
        while (pIter.MoveNext())
        {
            TSource pCurrent = pIter.Current;
            TKey hDataID = pCurrent.IDictionaryItem_GetKey();
            if (mapDataTable.ContainsKey(hDataID) == false)
                mapDataTable.Add(hDataID, new List<TSource>());

            mapDataTable[hDataID].Add(pCurrent);
        }
    }

    public static bool ContainsKey<TKeyData, TData>(this Dictionary<TKeyData, TData> mapData, IDictionaryItem<TKeyData> pItem)
    {
        return mapData.ContainsKey(pItem.IDictionaryItem_GetKey());
    }

    #endregion Dictionary
}