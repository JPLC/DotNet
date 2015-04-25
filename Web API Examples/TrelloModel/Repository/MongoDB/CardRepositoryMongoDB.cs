using System;

namespace TrelloModel.Repository.MongoDB
{
    public class CardRepositoryMongoDB
    {
        #region Variables and Properties
        private static readonly Lazy<CardRepositoryMongoDB> BoardRepo = new Lazy<CardRepositoryMongoDB>(() => new CardRepositoryMongoDB());

        public static CardRepositoryMongoDB Instance { get { return BoardRepo.Value; } }
        #endregion

        #region Methods
        #endregion
    }
}