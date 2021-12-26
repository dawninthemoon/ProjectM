namespace Data
{
    public partial class CharacterStatGameData : GameData<CharacterStatGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}