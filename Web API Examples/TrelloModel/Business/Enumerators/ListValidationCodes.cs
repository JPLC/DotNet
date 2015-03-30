namespace TrelloModel.Business.Enumerators
{
    public enum ListValidationCodes
    {
        ListNameIsNull,
        ListNameIsEmpty,
        ListNameBiggerThanMaxValue,
        ListNameSpecialChars,
        ListIndexNegative
    }
}