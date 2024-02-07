/*
* Author:	Iris Bermudez
* Date:		07/02/2024
*/



using Solitaire.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Solitaire.Gameplay {
    public class BasicCardContainer : AbstractCardContainer {
        #region Variables

        #endregion


        #region MonoBehaviour methods

        #endregion


        #region Public methods
        public override void AddCard( CardFacade _card ) {
            throw new System.NotImplementedException();
        }

        public override bool AddCards( List<CardFacade> _cards ) {
            foreach( CardFacade auxCard in _cards ) {
                _cards.Add( auxCard );

                auxCard.transform.position = new Vector3(
                                        transform.position.x + cardsOffset.x,
                                        transform.position.y + cardsOffset.y,
                                        transform.position.z
                                    );
            }

            return true;
        }

        public override bool CandAddCards() {
            throw new System.NotImplementedException();
        }

        public override List<CardFacade> Initialize( List<CardFacade> _cards ) {
            throw new System.NotImplementedException();
        }

        public override void RemoveCard( CardFacade _card ) {
            throw new System.NotImplementedException();
        }

        public override void RemoveCards( List<CardFacade> _cards ) {
            throw new System.NotImplementedException();
        }
        #endregion


        #region Protected
        protected override void SetUpStarterCards() {
            throw new System.NotImplementedException();
        }
        #endregion


        #region Private methods

        #endregion
    }
}