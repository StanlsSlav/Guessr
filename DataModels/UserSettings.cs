namespace OpenTrivia
{
	class UserSettings
	{
		public int Category { get; set; }
		public int Difficulty { get; set; }
		public int Type { get; set; }
	}

	enum DifficultyChoices
	{
		All,
		Easy,
		Medium,
		Hard
	}

	enum TypeChoices
	{
		All,
		Multiple,
		Boolean
	}

	enum CategoryChoices
	{
		All = -1, //All is extra
		General_Knowledge,
		Books,
		Film,
		Music,
		Musicals_Theatres,
		Television,
		Video_Games,
		Board_Games,
		Science_and_Nature,
		Science_Computers,
		Science_Mathematics,
		Mythology,
		Sports,
		Geography,
		History,
		Politics,
		Art,
		Celebrities,
		Animals,
		Vehicles,
		Comics,
		Gadgets,
		Japanese_Anime_and_Manga,
		Cartoon_and_Animations
	}
}
