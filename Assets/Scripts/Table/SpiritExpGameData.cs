namespace Data
{
    public partial class SpiritExpGameData : GameData<SpiritExpGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}