namespace Data
{
    public partial class SpiritGameData : GameData<SpiritGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}