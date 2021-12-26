namespace Data
{
    public partial class SkillGameData : GameData<SkillGameData>
    {
        public static void LoadData(string fileName)
        {
            LoadData(fileName, elem => elem.GetKey().ToString());
        }
    }
}