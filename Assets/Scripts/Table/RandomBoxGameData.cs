namespace Data
{
    public partial class RandomBoxGameData : GameData<RandomBoxGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}