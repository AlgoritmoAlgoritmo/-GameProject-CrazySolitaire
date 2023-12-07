using Solitaire.Common;

namespace Solitaire.Cards {

    [System.Serializable]
    public class CardData {

        #region Variables
        public short number;
        public CardType cardType;
        public CardColour cardColour;
        #endregion


        #region Constructor
        public CardData( short _number, CardType _cardType,
                                    CardColour _cardColour ) {
            number = _number;
            cardType = _cardType;
            cardColour = _cardColour;
        }
        #endregion
    }
}