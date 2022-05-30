namespace Guessr.UserSettings
{
    internal static class UserOptions
    {
        public static int Category { get; set; }

        public static int Difficulty { get; set; }

        public static int Type { get; set; }
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
        Boolean
    }

    // The categories start with 9 and there's an offset in 
    internal enum CategoryChoices
    {
        All = -1,
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