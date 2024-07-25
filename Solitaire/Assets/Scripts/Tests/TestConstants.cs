/*
* Author:	Iris Bermudez
* Date:		01/02/2024
*/




namespace Test {
    public class TestConstants {
        #region SpiderGameMode tests constants
        /*
         * General purpose constants
         */
        public const string CARD_PREFAB_PATH = "Assets/Prefabs/Gameplay/Card Prefab.prefab";

        /*
         * Spider constants
         */
        public const string SPIDERGAMEMODEMOCK_PREFAB_PATH = "Assets/Scripts/Tests/Solitaire/"
                                                + "GameModes/Spider/SpiderGameModeMock Prefab"
                                                + ".prefab";
        public const string SPIDERCARDCONTAINER_PREFAB_PATH = "Assets/Prefabs/Gameplay/Spider"
                                                + "/SpiderCardContainer 6Cards.prefab";

        public const string SPIDERCARDCONTAINERFORDISTRIBUTION_PREFAB_PATH = "Assets/Prefabs/" 
                                                + "Gameplay/Spider/CardDistributor.prefab";


        /*
         * Klondike constants
         */
        public const string KLONDIKEGAMEMODEMOCK_PREFAB_PATH = "Assets/Scripts/Tests/Solitaire/"
                                                + "GameModes/Klondike/KlondikeGameModeMock " 
                                                + "Prefab.prefab";
        public const string KLONDIKECARDCONTAINER_PREFAB_PATH = "Assets/Prefabs/Gameplay/Klondike"
                                                + "/KlondikeCardContainer.prefab";
        public const string KLONDIKECOMPLETEDCOLUMNCONTAINER_PREFAB_PATH = "Assets/Prefabs/Gameplay/Klondike"
                                                + "/Klondike CompletedColumnContainer.prefab";
        public const string KLONDIKELOOPABLECONTAINER_PREFAB_PATH = "Assets/Prefabs/Gameplay/Klondike"
                                                + "/KlondikeLoopableCardDistributor.prefab";
        #endregion
    }
}