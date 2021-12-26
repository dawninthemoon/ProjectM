namespace Data
{
    public partial class CharacterGameData : GameData<CharacterGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}