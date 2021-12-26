namespace Data
{
    public partial class MonsterGameData : GameData<MonsterGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}