using Data;
using FBControl;

public class GachaLogic
{
    public RandomBoxData Gacha(int index)
    {
        GachaData gachaData = GachaDataParser.Instance.Data[index];

        RandomBoxData data = RandomBoxDataParser.Instance.GetRandomBoxResult(gachaData.RandomBoxKey);

        return data;
    }

    public RandomBoxData Gacha(GachaData gachaData)
    {
        RandomBoxData data = RandomBoxDataParser.Instance.GetRandomBoxResult(gachaData.RandomBoxKey);

        return data;
    }

    public RandomBoxData[] Gacha(GachaData gachaData, int count)
    {
        RandomBoxData[] data = RandomBoxDataParser.Instance.GetRandomBoxResultArray(gachaData.RandomBoxKey, count);

        return data;
    }

    public void GetItem(RandomBoxData data)
    {
        FirebaseManager.Instance.UserData.UserItemDataContainer.AddItem(data.RewardKey, data.Count);
    }
}