namespace Data
{
    public partial class GachaGameData : GameData<GachaGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}