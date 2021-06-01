namespace OpenTriviaAPICaller.src.DataModels
{
    internal class UserSettings
    {
        public int Category { get; set; }
        public int Difficulty { get; set; }
        public int Type { get; set; }
    }

    internal enum DifficultyChoices
    {
        All,
        Easy,
        Medium,
        Hard
    }

    internal enum TypeChoices
    {
        All,
        Multiple,
        TrueOrFalse
    }

    internal enum CategoryChoices
    {
        All = -1, // All is extra
        GeneralKnowledge,
        Books,
        Film,
        Music,
        MusicalsTheatres,
        Television,
        VideoGames,
        BoardGames,
        ScienceAndNature,
        ScienceComputers,
        ScienceMathematics,
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
        JapaneseAnimeAndManga,
        CartoonAndAnimations
    }
}