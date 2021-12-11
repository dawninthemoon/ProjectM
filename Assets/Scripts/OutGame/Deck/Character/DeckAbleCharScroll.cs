namespace OutGame
{
    public class DeckAbleCharScroll : DeckScrollBase
    {
        private Data.Character[] characters;

        public void Start()
        {
            characters = Data.CharacterDataParser.Instance.Data;
            Init();
        }

        public override void Init()
        {
            for (int i = 0; i < base.deckScrollButtonBases.Length; ++i)
            {
                if (i >= characters.Length)
                    deckScrollButtonBases[i].gameObject.SetActive(false);
                else
                {
                    deckScrollButtonBases[i].gameObject.SetActive(true);
                    deckScrollButtonBases[i].Init(i, characters[i].Key, OnSelectCallback);
                }
            }
        }
    }
}