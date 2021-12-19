namespace Data
{
    public partial class ItemInfoGameData : GameData<ItemInfoGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}